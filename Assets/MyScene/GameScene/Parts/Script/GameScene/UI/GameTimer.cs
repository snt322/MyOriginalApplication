using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中の残り時間を表示するスクリプト
/// テキストにアタッチ
/// </summary>
//[RequireComponent(typeof(UnityEngine.UI.Text))]
public class GameTimer : MonoBehaviour
{

    UnityEngine.UI.Text m_TextTime = null;

    [SerializeField]
    private int m_TimeLimit = 0;                        //ゲームの残り時間

    private int m_CurrentTime = 0;

    private System.TimeSpan m_Span;
    private System.DateTime m_FormerTime;


    // Use this for initialization
    void Start()
    {
        m_TextTime = gameObject.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;     //ゲームの残り時間を表示するテキストを取得
        m_FormerTime = System.DateTime.Now;
    }

    // Update is called once per frame
    void Update()
    {
        System.DateTime tmpTime = System.DateTime.Now;

        m_Span = tmpTime - m_FormerTime;
        m_CurrentTime = m_TimeLimit - (int)m_Span.TotalSeconds;

        if(m_CurrentTime < 0)
        {
            UnityEngine.Time.timeScale = 0;
        }

    }

    private void FixedUpdate()
    {
        string timeLimit = "Time Limit : " + string.Format("{0:D3} sec", m_CurrentTime);
        m_TextTime.text = timeLimit;
    }

}


