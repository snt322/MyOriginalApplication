using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 実行中のアニメーションを判定するために使用
/// </summary>
namespace MyAnimationStateNames
{
    public enum StateNames
    {
        DIE,
        WALK_RUN,
        ATTACK,
        RECOVER,
        DAMAGE,
        MAGIC,
    }

    public enum EquipmentStateName
    {
        Equipment_Canvas_Close_Ended,
        Equipment_Canvas_Expand_Ended,
    }

}
