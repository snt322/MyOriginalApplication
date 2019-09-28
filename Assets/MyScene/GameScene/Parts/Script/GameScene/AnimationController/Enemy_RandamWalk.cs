using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Enemyオブジェクトをランダムに移動させるスクリプト
/// </summary>
public class Enemy_RandamWalk : MonoBehaviour
{
    enum enumTmpStates
    {
        walk,
        wait,
        turn,
        none,
    }

    public struct MyRange
    {
        public float Max;
        public float Min;
    }

    /*
     * ランダムに移動するスクリプト
     * コルーチンにてランダム動作を作成する
     */

    //方向変換のスピード
    [SerializeField, Tooltip("ランダムに向きを変えるスピード")]
    private MyRange m_RotSpeedRange;
    private float m_RotSpeed;


    //方向変換の時間
    [SerializeField, Tooltip("ランダムに向きを変える時間")]
    private MyRange m_RotTimeRange;


    //歩くスピード
    [SerializeField, Tooltip("ランダムに歩くスピード")]
    private MyRange m_WalkSpeedRange;
    private float m_WalkSpeed;


    //歩く時間
    [SerializeField, Tooltip("ランダムに歩く時間")]
    private MyRange m_WalkTimeRange;


    //立ち止まる時間
    [SerializeField, Tooltip("ランダムに立ち止まる時間")]
    private MyRange m_WaitTimeRange;


    //モーションのオンオフを制御するフラグ
    private bool m_IsBreak = false;

    private enumTmpStates m_State = enumTmpStates.none;

    // Use this for initialization
    void Start()
    {
        //(向きを変える角度) = (回転スピード) × (回転時間)

        //向きを変えるスピード範囲の初期化
        m_RotSpeedRange.Max = 1;
        m_RotSpeedRange.Min = -1;
        //向きを変える時間範囲の初期化
        m_RotTimeRange.Max = 3;
        m_RotTimeRange.Min = 0;
        //-----------------------

        //(歩く距離) = (歩くスピード) × (歩く時間)

        //歩くスピード範囲の初期化
        m_WalkSpeedRange.Max = 3;
        m_WalkSpeedRange.Min = 0;
        //歩く時間範囲の初期化
        m_WalkTimeRange.Max = 5;
        m_WalkTimeRange.Min = 0;

        //-----------------------
        //立ち止まる時間の初期化
        m_WaitTimeRange.Max = 5;
        m_WaitTimeRange.Min = 0;
        

        StartCoroutine("MotionRoutine");
    }

    // Update is called once per frame
    void Update()
    {

        switch (m_State)
        {
            case enumTmpStates.turn:
                {
                    Vector3 vect = gameObject.transform.forward;
                    vect = Quaternion.Euler(0, m_RotSpeed, 0) * vect;
                    vect *= Time.deltaTime;
                    gameObject.transform.forward = vect;
                }
                break;
            case enumTmpStates.wait:
                {
                    ;
                }
                break;
            case enumTmpStates.walk:
                {
                    Vector3 vect = gameObject.transform.forward;
                    vect = Vector3.Normalize(vect) * m_WalkSpeed;
                    vect *= Time.deltaTime;
                    gameObject.transform.position += vect;
                }
                break;
            default:
                {
                    ;
                }
                break;
        }





    }


    IEnumerator MotionRoutine()
    {
        bool isLoop = true;
        float duration = 0;
        while (isLoop)
        {
            if (m_IsBreak) break; ;

            //向きを変える
            duration = Random.Range(m_RotTimeRange.Min, m_RotTimeRange.Max);
            m_RotSpeed   = Random.Range(m_RotSpeedRange.Min, m_RotSpeedRange.Max);
            m_State      = enumTmpStates.turn;
            yield return new WaitForSeconds(duration);

            //歩く
            duration = Random.Range(m_WalkTimeRange.Min, m_WalkTimeRange.Max);
            m_WalkSpeed = Random.Range(m_WalkSpeedRange.Min, m_WalkSpeedRange.Max);
            m_State = enumTmpStates.walk;
            yield return new WaitForSeconds(duration);

            //立ち止まる
            duration = Random.Range(m_WaitTimeRange.Min, m_WaitTimeRange.Max);
            m_State = enumTmpStates.wait;
            yield return new WaitForSeconds(duration);

        }

        m_State = enumTmpStates.none;

        yield break; ;
    }




}
