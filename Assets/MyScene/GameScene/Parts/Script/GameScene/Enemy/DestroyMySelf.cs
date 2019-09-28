using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// このscriptはEnemyオブジェクトにアタッチしてください。
/// Inspector上で初期はenabledをfalseにしてください。
/// Enemyオブジェクトの体力が0になりenumHealthStateがDEADになると、
/// Animation[DIE_Delete]内で本scriptがEnable.trueになり、オブジェクトの削除、スコア加点が実行されます。
/// </summary>
public class DestroyMySelf : MonoBehaviour
{
    [SerializeField]
    private int m_ThisScore = 0;

    ScoreManager m_ScoreManager = null;



    // Use this for initialization
    void Start()
    {
        string tagScore = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Score);
        GameObject gObj = GameObject.FindGameObjectWithTag(tagScore);
        m_ScoreManager = gObj.GetComponent<ScoreManager>() as ScoreManager;


        m_ScoreManager.AddScore = m_ThisScore;      //スコアに加点
        Destroy(gameObject);                        //オブジェクトを削除
    }

}
