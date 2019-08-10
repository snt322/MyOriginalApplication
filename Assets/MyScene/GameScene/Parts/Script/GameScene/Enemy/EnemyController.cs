using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using TargetFindState;


namespace TargetFindState
{
    /// <summary>
    /// レイキャストにより探索するターゲットの捕捉、消失等の状態を表す
    /// </summary>
    public enum enumFindTargetState
    {                                       //ターゲットを
        NotFind,                            //捜索中
        Find,                               //捕捉
        Lost,                               //追跡中に消失
    }
}




/// <summary>
/// EnemyのPlayer探索スクリプト
/// 子オブジェクトのRaycast_Searchにアタッチする
/// Raycast_SearchオブジェクトのZ軸正方向に円錐上にRayを飛ばしてPlayerを探索する
/// </summary>
public class EnemyController : MonoBehaviour
{

    private enumFindTargetState m_TargetFindState = enumFindTargetState.NotFind;                    //このEnemyオブジェクトがPlayerを捕捉中、消失中フラグ

    private GameObject m_TargetPlayer = null;                                                       //ターゲットとなるPlayerゲームオブジェクト

    [SerializeField]
    private GameObject m_RaycastLauncher = null;

    [SerializeField]
    private Animator m_EnemyAnimator = null;

    [SerializeField]
    private Enemy_Attack_Damege_Controller m_Enemy_Attack_Damege_Controller = null;                 //このスクリプト内でEnemyオブジェクトの健康状態を保持

    private readonly float m_AttacRange = 1f;                                                     //このオブジェクトの攻撃レンジ

    private readonly float m_MoveSpeed = 3.0f;                                                      //このオブジェクトの移動速度

    private float m_SearchLength2 = 10.0f * 10.0f;                                                  //Enemyオブジェクトの索敵距離

    //重力
    private float m_Gravity = -9.8f;

    // Use this for initialization
    void Start()
    {

        string tagStr = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Player);
        m_TargetPlayer = GameObject.FindGameObjectWithTag(tagStr);


        StartCoroutine(SearchTargetCoroutine());
    }

    // Update is called once per frame
    void Update()
    {
        float rotDeg = 0;
        Vector3 moveDir = new Vector3();


        //
        GetMoveRotDirection(ref moveDir, ref rotDeg);
        Move(moveDir);

    }

    private int counter = 0;


    /// <summary>
    /// コルーチン内でPlayerを探索する
    /// </summary>
    /// <returns></returns>
    private IEnumerator SearchTargetCoroutine()
    {
        for (; ; )
        {
            switch (m_TargetFindState)
            {
                case enumFindTargetState.NotFind:                                   //ターゲットを捕捉していない場合
                    if(FindTargetWithBoxRay())
                    {
                        m_TargetFindState = enumFindTargetState.Find;               //捕捉中へ設定を変更
                    }
                    break;
                case enumFindTargetState.Find:                                      //ターゲットを捕捉中の場合
                    if (!KeepFindingTarget())
                    {
                        m_TargetFindState = enumFindTargetState.NotFind;            //未捕捉へ設定を変更
                    }
                    else
                    {
                        Attack();                                                   //捕捉中の場合攻撃範囲内か判断して攻撃する
                    }
                    break;
            }
            yield return new WaitForSeconds(0.1f);
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    [Obsolete("Use FindeTargetWithBoxRay instead.")]
    private bool FindTarget()
    {

        //ターゲット(Player)とこのオブジェクト間の距離を計算
        Vector3 thisPosVect = m_RaycastLauncher.transform.position;
        Vector3 targetPosVect = m_TargetPlayer.transform.position;

        Vector3 diffVect = targetPosVect - thisPosVect;
        float distanceBtwnThisAndTarget = Vector3.SqrMagnitude(diffVect);

        //        Debug.Log("Distance = " + distanceBtwnThisAndTarget + "  :  threshold =" + m_SearchLength2);

        if (distanceBtwnThisAndTarget > m_SearchLength2)
        {
            return false;                                                           //距離が遠いいので発見できず
        }

        //以下、発見される可能性がある距離まで接近した場合の処理
        //2段階の処理でEnemyがPlayerを発見したか判定する


        //①このEnemyオブジェクトが向いている方向にPlayerオブジェクトが居るかRaycastで調査する

        Vector3 originPosVect = m_RaycastLauncher.transform.position;                 //ワールド座標でのレイの開始地点、Enemyオブジェクトのワールド座標位置
        Vector3 directionVect = m_RaycastLauncher.transform.forward;                  //レイの方向、ゲームオブジェクトの前方向へ

        float maxDistance = 10.0f;                                                  //レイが衝突を検知する最大距離
        int layerMask;                                                              //レイヤーマスク、レイキャストするときに選択的に衝突を無視するために使用する。
        layerMask = 1 << 14;                                                        //Playerレイヤーのみ衝突判定を行う

        bool isRayCastFind = false;
        isRayCastFind = UnityEngine.Physics.Raycast(originPosVect, directionVect, maxDistance, layerMask);     //レイキャスト
        if (!isRayCastFind)
        {
            //EnemyオブジェクトがPlayerの方向を向いていないので処理を抜ける
            return false;                                                           //発見できず
        }

        //②このEnemyオブジェクトとPlayerオブジェクトの間に障害物がない場合にEnemyがPlayerを発見したこととする

        layerMask = 0xfbfff;//10321911;                                                       //int layerMaskの1をセットしたビットのみレイが衝突したか判定する
        maxDistance = Vector3.Magnitude(diffVect);                                            //PlayerオブジェクトとこのEnemyオブジェクト間の距離

        isRayCastFind = UnityEngine.Physics.Raycast(originPosVect, directionVect, maxDistance, layerMask);     //レイキャスト、trueの場合はPlayer／Enemy間に障害物がある
        
        return !isRayCastFind;

    }

    /// <summary>
    /// BoxRayでPlayerを探索する
    /// </summary>
    /// <returns></returns>
    private bool FindTargetWithBoxRay()
    {
        //ターゲット(Player)とこのオブジェクト間の距離を計算
        Vector3 thisPosVect = m_RaycastLauncher.transform.position;
        Vector3 targetPosVect = m_TargetPlayer.transform.position;

        Vector3 diffVect = targetPosVect - thisPosVect;
        float distanceBtwnThisAndTarget = Vector3.SqrMagnitude(diffVect);

        if (distanceBtwnThisAndTarget > m_SearchLength2)
        {
            return false;                                                           //距離が遠いいので発見できず
        }

        //以下、発見される可能性がある距離まで接近した場合の処理
        //2段階の処理でEnemyがPlayerを発見したか判定する


        //①このEnemyオブジェクトが向いている方向にPlayerオブジェクトが居るかRaycastで調査する

        Vector3 originPosVect = m_RaycastLauncher.transform.position;                 //ワールド座標でのレイの開始地点、Enemyオブジェクトのワールド座標位置
        Vector3 directionVect = m_RaycastLauncher.transform.forward;                  //レイの方向、ゲームオブジェクトの前方向へ

        Vector3 halfExtentVect = new Vector3(0.25f, 0.25f, 0.25f);

        float maxDistance = 10.0f;                                                  //レイが衝突を検知する最大距離
        int layerMask;                                                              //レイヤーマスク、レイキャストするときに選択的に衝突を無視するために使用する。
        layerMask = 1 << 14;                                                        //Playerレイヤーのみ衝突判定を行う

        bool isRayCastFind = false;

        isRayCastFind = Physics.BoxCast(originPosVect, halfExtentVect, directionVect, Quaternion.identity, maxDistance, layerMask);
//        isRayCastFind = UnityEngine.Physics.Raycast(originPosVect, directionVect, maxDistance, layerMask);     //レイキャスト
        if (!isRayCastFind)
        {
            //EnemyオブジェクトがPlayerの方向を向いていないので処理を抜ける
            return false;                                                           //発見できず
        }

        //②このEnemyオブジェクトとPlayerオブジェクトの間に障害物がない場合にEnemyがPlayerを発見したこととする

        layerMask = 0xfbfff;//10321911;                                                       //int layerMaskの1をセットしたビットのみレイが衝突したか判定する
        maxDistance = Vector3.Magnitude(diffVect);                                            //PlayerオブジェクトとこのEnemyオブジェクト間の距離

        isRayCastFind = UnityEngine.Physics.Raycast(originPosVect, directionVect, maxDistance, layerMask);     //レイキャスト、trueの場合はPlayer／Enemy間に障害物がある

        return !isRayCastFind;


        return false;
    }

    /// <summary>
    /// Playerキャラの攻撃可能範囲に居るか判定する。
    /// 戻り値：true = 攻撃可能範囲内、false = 攻撃範囲外
    /// </summary>
    private bool canAttackLeng()
    {
        Vector3 thisPos = this.gameObject.transform.position;               //このEnemyの座標
        Vector3 targetPos = m_TargetPlayer.transform.position;              //Playerの座標

        Vector3 tmpVect = thisPos - targetPos;

        float distanceSqr = Vector3.Dot(tmpVect, tmpVect);

        return (distanceSqr <= m_AttacRange) ? true : false;
    }

    /// <summary>
    /// Ienumerator SearchTargetCoroutine()内で呼び出す
    /// </summary>
    private void Attack()
    {
        if (canAttackLeng())
        {
            m_Enemy_Attack_Damege_Controller.Attack();
        }
    }

    private void OnDrawGizmos()
    {
        Vector3 originPosVect = m_RaycastLauncher.transform.position;                 //ワールド座標でのレイの開始地点、Enemyオブジェクトのワールド座標位置
        Vector3 directionVect = m_RaycastLauncher.transform.forward;                  //レイの方向、ゲームオブジェクトの前方向へ

        float maxDistance = 10.0f;                                                  //レイが衝突を検知する最大距離

        Gizmos.DrawLine(originPosVect, originPosVect + maxDistance * directionVect);
        Gizmos.DrawWireCube(originPosVect + directionVect * maxDistance, new Vector3(0.25f, 0.25f, 0.25f));
    }


    /// <summary>
    /// Playerオブジェクトを発見後に距離が離れすぎた場合(逃げられた場合)を判定する
    /// 戻り値 true : 捕捉中、 false : 逃げられた
    /// </summary>
    /// <returns></returns>
    private bool KeepFindingTarget()
    {
        //ターゲット(Player)とこのオブジェクト間の距離を計算
        Vector3 thisPosVect = m_RaycastLauncher.transform.position;
        Vector3 targetPosVect = m_TargetPlayer.transform.position;

        Vector3 diffVect = targetPosVect - thisPosVect;
        float distanceBtwnThisAndTarget = Vector3.SqrMagnitude(diffVect);

        if (distanceBtwnThisAndTarget > m_SearchLength2)
        {
            return false;                                                           //距離が遠いいので発見できず
        }
        return true;
    }


    /// <summary>
    /// enumFindTargetState m_TargetFindStateの値に応じて移動方向を決定する
    /// </summary>
    /// <returns></returns>
    private void GetMoveRotDirection(ref Vector3 moveDistPerFrame, ref float rotDegPerFrame)
    {
        //Enemyの状態enumHealthStateによって移動の可否を判定
        if(m_Enemy_Attack_Damege_Controller.EnemyHealthState == MyClasses.enumHealthState.DEAD)
        {
            moveDistPerFrame = new Vector3();                                               //移動無し
            rotDegPerFrame = 0;                                                             //回転無し
            return;                                                                         //処理を抜ける
        }

        switch (m_TargetFindState)
        {
            case enumFindTargetState.Find:                  //Playerを見つけている場合、最短距離でPlayer方向に向かってくる
                {
                    Vector3 thisPos = gameObject.transform.position;                         //このオブジェクトの座標
                    Vector3 targetPos = m_TargetPlayer.transform.position;                   //ターゲットPlayerの座標

                    moveDistPerFrame = targetPos - thisPos;                                 //ターゲットオブジェクトへ向かう単位ベクトル
                    moveDistPerFrame.y = 0.0f;                                              //Y方向成分を0にする
                    moveDistPerFrame = Vector3.Normalize(moveDistPerFrame);

                    this.gameObject.transform.forward = moveDistPerFrame;                              //向きを変える

                    moveDistPerFrame *= m_MoveSpeed;

                    moveDistPerFrame.y = m_Gravity;                                                   //重力加速度を付与

                    if(canAttackLeng())                                                               //攻撃範囲内に以上に近寄らない
                    {
                        moveDistPerFrame.x = 0.0f;
                        moveDistPerFrame.z = 0.0f;
                    }



                }
                break;
            default:
                moveDistPerFrame.x = 0;
                moveDistPerFrame.y = m_Gravity;
                moveDistPerFrame.z = 0;

                rotDegPerFrame = 0;
                break;
        }

    }

    private void Move(Vector3 moveDistPerFrame)
    {
        moveDistPerFrame *= Time.deltaTime;

        //移動
        (this.gameObject.GetComponent<CharacterController>() as CharacterController).Move(moveDistPerFrame);

        moveDistPerFrame.y = 0;
    }

}
