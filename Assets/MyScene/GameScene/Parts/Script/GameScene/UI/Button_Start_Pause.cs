using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのPause、Startを行うUI.Imageにアタッチしてください。
/// </summary>
public class Button_Start_Pause : MonoBehaviour
{
    [SerializeField, Tooltip("残りプレイ時間を管理するGameController_TimeLimit.csをアタッチしてください。")]
    private GameController_TimeLimit m_GCTimeLimit = null;

    private const string mStrPlay = "Playing";
    private const string mStrPause = "Pausing";

    private void Start()
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Playing";
    }

    private void Update()
    {
        Button_StartPause();
    }

    public void Button_StartPause()
    {
        bool isPlaying = m_GCTimeLimit.GetIsPlaying();

        if(isPlaying)                                //ポーズ中だったのを解除中にする
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = mStrPlay;
        }
        else
        {                                           //ポーズ解除中だったのをポーズにする。
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = mStrPause;
        }
    }
}
