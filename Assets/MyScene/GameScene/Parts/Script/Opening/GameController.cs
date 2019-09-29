using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// シーンがロードされる度にtimeScale=1をセットする。
/// </summary>
public class GameController : MonoBehaviour
{
    private void Update()
    {
        if (UnityEngine.Time.timeScale < 1.0f)
        {
            UnityEngine.Time.timeScale = 1.0f;
        }

        Debug.Log("GameController.cs");
    }

}
