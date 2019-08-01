using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Button_StartMenu : MonoBehaviour {

    public void Button_ToStartMenu()
    {
        if(Time.timeScale < 1.0f) { Time.timeScale = 1.0f; }                //TImeScaleを1.0fに戻す
        SceneManager.LoadScene("OpeningScene", LoadSceneMode.Single);
    }

}
