using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyEnumerator
{

    /// <summary>
    /// タグを管理するEnumerator
    /// OnTriggerEnter()等で衝突相手を判定する際に使用する
    /// 列挙子名とタグ名を一致させること
    /// </summary>
    public enum EnumeratorTag
    {
        Untagged,
        Respawn,
        Finish,
        EditorOnly,
        MainCamera,
        Player,
        GameController,
        FirstPersonPerspective,
        ThirdPersonPerspective, 
        Enemy,
        EnemyWeapon,
        UnityChanResurectionTag,
        UnityChanWeapon,
        EnemyHitPointTag,

    }

}