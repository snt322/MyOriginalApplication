using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームのPause、Startを行う
/// </summary>
public class Button_Start_Pause : MonoBehaviour
{
    
    bool isPaused = false;

    private void Start()
    {
        gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Playing";
    }

    public void Button_StartPause()
    {
        if(isPaused)
        {
            Time.timeScale = 1;
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Playing";
        }
        else
        {
            Time.timeScale = 0;
            gameObject.GetComponentInChildren<UnityEngine.UI.Text>().text = "Pause";
        }
        isPaused = !isPaused;
    }
}
