using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// スコアを保持するスクリプト
/// アプリケーション内に1つのみ作成し、
/// シーン遷移で破棄されないオブジェクトにアタッチする
/// </summary>
public class DontDestroyMe : MonoBehaviour
{
    private void Awake()
    {
        GameObject[] gObj = GameObject.FindGameObjectsWithTag("OnlyOneObjInApp");
        if (gObj.Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }
}
