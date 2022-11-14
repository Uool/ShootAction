using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationData
{
    public static float AttackDuration(EnemyType enemyType, int attackNumber)
    {
        float duration = 1f;

        switch (enemyType)
        {
            case EnemyType.MonsterOneHand:
                switch (attackNumber)
                {
                    case 0:
                        duration = 2.267f;
                        break;
                }
                break;
            case EnemyType.MonsterTwoHand:
                break;
            case EnemyType.HumanOneHand:
                break;
            default:
                break;
        }

        return duration;
    }

    public static float DeadDuration(EnemyType enemyType)
    {
        float duration = 1f;

        switch (enemyType)
        {
            case EnemyType.MonsterOneHand:
                duration = 3.2f;
                break;
            case EnemyType.MonsterTwoHand:
                break;
            case EnemyType.HumanOneHand:
                break;
            default:
                break;
        }

        return duration;
    }
}
