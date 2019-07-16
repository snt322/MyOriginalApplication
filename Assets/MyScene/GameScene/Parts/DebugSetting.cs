using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  各スクリプトに分散しているゲームパラメータのセッティングを一元管理するスクリプト
 *  
 *  
 */

public class DebugSetting : MonoBehaviour {






	// Use this for initialization
	void Start () {
//        StartCoroutine("Coroutine1");
	}
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator Coroutine1()
    {
        for(; ;)
        {
//            Debug.Log(System.DateTime.Now);
            yield return new WaitForSeconds(1f);
        }
    }

}
