using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのPause、Startを行う
/// </summary>
public class Button_Start_Pause : MonoBehaviour
{
    [SerializeField, Tooltip("GameTimerスクリプトをアタッチしてください")]
    private GameTimer m_GameTimer = null;
    

    private void Start()
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Playing";
    }

    public void Button_StartPause()
    {
        bool isPaused = m_GameTimer.StartPause();

        if(isPaused)                                //ポーズ中だったのを解除中にする
        {
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Playing";
        }
        else
        {                                           //ポーズ解除中だったのをポーズにする。
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Pause";
        }
    }
}
