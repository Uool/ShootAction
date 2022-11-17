using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonGenerator : MonoBehaviour
{
    public class Cell
    {
        public bool visited = false;
        public bool[] status = new bool[4];
    }

    [System.Serializable]
    public class Rule
    {
        public GameObject room;
        public Vector2Int minPosition;
        public Vector2Int maxPosition;

        public bool obligtory;  // 해당 방이 반드시 있어야 하는지에 대한 여부
        public bool isExisted;
        public int ProbabilityOfSpawning(int x, int y)
        {
            // 0 - cannot Spawn, 1 - can spawn, 2 - has to spawn

            if (x >= minPosition.x && x <= maxPosition.x && y >= minPosition.y && y <= maxPosition.y)
            {
                return obligtory ? 2 : 1;
            }

            return 0;
        }
    }    

    public Vector2Int size;
    public int startPos = 0;
    public Rule[] rooms;
    public Vector2 offset;  // 방 사이간의 거리

    List<Cell> board;


    // Start is called before the first frame update
    void Start()
    {
        MazeGenerator();
    }

    void GenerateDungeon()
    {
        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                Cell currentCell = board[(i + j * size.x)];
                if (currentCell.visited)
                {
                    int randomRoom = -1;
                    List<int> availableRooms = new List<int>();

                    for (int k = 0; k < rooms.Length; k++)
                    {
                        int p = rooms[k].ProbabilityOfSpawning(i, j);
                        if (p == 2 && false == rooms[k].isExisted)
                        {
                            randomRoom = k;
                            rooms[k].isExisted = true;
                            break;
                        }
                        else if (p == 1)
                            availableRooms.Add(k);
                    }

                    if (randomRoom == -1)
                    {
                        if (availableRooms.Count > 0)
                            randomRoom = availableRooms[Random.Range(0, availableRooms.Count)];
                        else
                            randomRoom = 0;
                    }

                    var newRoom = Instantiate(rooms[randomRoom].room, new Vector3(i * offset.x, 0f, -j * offset.y), Quaternion.identity, transform).GetComponent<RoomBehaviour>();
                    newRoom.UpdateRoom(currentCell.status);

                    newRoom.name += " " + i + "-" + j;
                }
            }
        }
    }

    void MazeGenerator()
    {
        board = new List<Cell>();

        for (int i = 0; i < size.x; i++)
        {
            for (int j = 0; j < size.y; j++)
            {
                board.Add(new Cell());
            }
        }

        int currentCell = startPos;
        Stack<int> path = new Stack<int>();

        int k = 0;
        while (k < 1000)
        {
            k++;

            board[currentCell].visited = true;

            if (currentCell == board.Count - 1)
                break;

            // check the cell's neighbors
            List<int> neighbors = CheckNeighbors(currentCell);

            if (neighbors.Count == 0)
            {
                if (path.Count == 0)
                    break;
                else
                    currentCell = path.Pop();
            }
            else
            {
                path.Push(currentCell);

                int newCell = neighbors[Random.Range(0, neighbors.Count)];
                if (newCell > currentCell)
                {
                    // right or down
                    if (newCell - 1 == currentCell)
                    {
                        board[currentCell].status[(int)RoomDir.Right] = true;
                        currentCell = newCell;
                        board[currentCell].status[(int)RoomDir.Left] = true;
                    }
                    else
                    {
                        board[currentCell].status[(int)RoomDir.Down] = true;
                        currentCell = newCell;
                        board[currentCell].status[(int)RoomDir.Up] = true;
                    }
                }
                else
                {
                    // left or up
                    if (newCell + 1 == currentCell)
                    {
                        board[currentCell].status[(int)RoomDir.Left] = true;
                        currentCell = newCell;
                        board[currentCell].status[(int)RoomDir.Right] = true;
                    }
                    else
                    {
                        board[currentCell].status[(int)RoomDir.Up] = true;
                        currentCell = newCell;
                        board[currentCell].status[(int)RoomDir.Down] = true;
                    }
                }
            }     
        }
        GenerateDungeon();
    }

    // TODO: 맵을 체크한 후, 방문하지 않은 곳에서 몬스터가 발생해야됨.
    List<int> CheckNeighbors(int cell)
    {
        List<int> neighbors = new List<int>();

        // check up neighbor
        if (cell - size.x >= 0 && false == board[(cell-size.x)].visited)
        {
            neighbors.Add(cell - size.x);
        }
        // check down neighbor
        if (cell + size.x < board.Count && false == board[(cell + size.x)].visited)
        {
            neighbors.Add(cell + size.x);
        }
        // check right neighbor
        if ((cell + 1) % size.x != 0 && false == board[(cell + 1)].visited)
        {
            neighbors.Add(cell + 1);
        }
        // check left neighbor
        if (cell % size.x != 0 && false == board[(cell - 1)].visited)
        {
            neighbors.Add(cell - 1);
        }

        return neighbors;
    }
}
