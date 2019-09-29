using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController_TimeLimit : MonoBehaviour
{
//    [SerializeField, Tooltip("GameのTimeLimitを表示するUI.Textをセットしてください。")]
//    private UnityEngine.UI.Text m_TextTime = null;

    [SerializeField, Tooltip("初期の残りプレイ時間をセットしてください。")]
    private float m_InitTimeLimit = 0;
    
    [SerializeField, Tooltip("現在の残りプレイ時間")]
    private float m_RemaingTime = 0;

    [SerializeField, Tooltip("初期の残りプレイ時間を任意に設定する場合チェック")]
    private bool m_IsSetInitRemainitTime = false;

    private const float PlayingTimeScale = 1;
    private const float StoppingTimeScale = 0;

    /// <summary>
    /// ポーズ中はfalse
    /// </summary>
    private bool m_IsPausing = false;

    // Use this for initialization
    void Start()
    {
        if(!m_IsSetInitRemainitTime)
        {
            m_RemaingTime = m_InitTimeLimit;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalRemainingTime();
    }

    /// <summary>
    /// 引数に応じてゲームの一時停止、再開をする。ゲームの一時停止はUnityEngine.Time.timeScaleで制御する
    /// </summary>
    /// <param name="isMakePause">true : ゲームを一時停止する、false : ゲームを再開する</param>
    /// <returns>引数isMakePauseが適切な場合はtrue、不適切な場合はfalse、例えば一時停止中に引数trueを与えると「すでに停止中」なのでfalseを返す</returns>
    public bool StartStopGame(bool isMakePause)
    {
        bool formerIsPausing = m_IsPausing;

        //ゲーム一時停止中(m_IsPausing==true)にゲームを停止(isStop==true)するとfalseを返す
        if(formerIsPausing == isMakePause) { return false; }

        if(isMakePause)
        {
            UnityEngine.Time.timeScale = StoppingTimeScale;
        }
        else
        {
            UnityEngine.Time.timeScale = PlayingTimeScale;
        }
        return true;
    }

    /// <summary>
    /// 引数に応じてゲームの一時停止、再開をする。ゲームの一時停止はUnityEngine.Time.timeScaleで制御する
    /// </summary>
    public void StartStopGame()
    {
        if (!m_IsPausing)
        {
            UnityEngine.Time.timeScale = StoppingTimeScale;
        }
        else
        {
            UnityEngine.Time.timeScale = PlayingTimeScale;
        }

        m_IsPausing = !m_IsPausing;

    }

    /// <summary>
    /// 
    /// </summary>
    private float CalRemainingTime()
    {
        if(!m_IsPausing)
        {
            m_RemaingTime -= UnityEngine.Time.deltaTime;                //前のフレームとの差分だけプレイ時間を減らす
        }
        return m_RemaingTime;
    }


    /// <summary>
    /// 外部に残りプレイ時間を渡すメソッド 
    /// </summary>
    /// <returns>残りプレイ時間</returns>
    public float GetRemainingTime()
    {
        return this.m_RemaingTime;
    }

    /// <summary>
    /// ゲームシーンがプレイ中かポーズ中か返す。
    /// </summary>
    /// <returns>true : ポーズ中</returns>
    public bool GetIsPlaying()
    {
        return !m_IsPausing;
    }

}
