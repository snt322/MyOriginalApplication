using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Button_StartMenu : MonoBehaviour {

    public void Button_ToStartMenu()
    {
        Debug.Log("Button_ToStartMenu()");
        if(Time.timeScale < 1.0f)
        {
            Time.timeScale = 1.0f;
            Debug.Log("");
        }                //TImeScaleを1.0fに戻す
        Debug.Log("Button_ToStartMenu() after if");
        SceneManager.LoadScene("OpeningScene", LoadSceneMode.Single);
    }

}
