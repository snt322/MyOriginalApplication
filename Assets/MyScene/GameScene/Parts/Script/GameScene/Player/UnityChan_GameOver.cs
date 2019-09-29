using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// PlayerのLifeが0になった場合の処理
/// Game Overにする
/// </summary>
public class UnityChan_GameOver : MonoBehaviour
{

    private void GameOverAction()
    {
        Debug.Log("Game Over!!!!");

        GameObject obj = GameObject.FindGameObjectWithTag("");
    }
}
