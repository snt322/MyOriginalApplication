using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class Button_StarScene : MonoBehaviour {

    public void Button_StartMenu()
    {
        SceneManager.LoadScene("OpeningScene", LoadSceneMode.Single);
    }

}
