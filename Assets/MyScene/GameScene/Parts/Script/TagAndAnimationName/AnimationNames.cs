using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 実行中のアニメーションを判定するために使用
/// </summary>
namespace MyAnimationStateNames
{ 
    public static class StateNames
    {
        public static readonly string DIE = "DIE";
        public static readonly string WALK_RUN = "WALK_RUN";
        public static readonly string ATTACK = "ATTACK";
        public static readonly string RECOVER = "RECOVER";
        public static readonly string DAMAGE = "DAMAGE";
    }
}
