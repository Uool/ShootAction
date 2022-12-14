public enum AnimatorTrigger
{
    NoTrigger = 0,
    IdleTrigger = 1,
    ActionTrigger = 2,
    AttackTrigger = 3,
    DeathTrigger = 4,
    ReviveTrigger = 5,
}

public enum CharacterState
{
    Idle = 0,
    Move = 1,
}

public enum EnemyAttackType
{
    Building = 0,
    Player = 1,
}

public enum EnemyType
{
    MonsterOneHand,
    MonsterTwoHand,
    HumanOneHand,
}

public enum RoomDir
{
    Up,
    Down,
    Right,
    Left,
}

public enum RoomType
{
    Start,
    End,
}