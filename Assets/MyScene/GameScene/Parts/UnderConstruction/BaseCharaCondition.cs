using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 *  キャラクタ毎にアタッチする
 *  
 *  
 *
 */







public class BaseCharaCondition : MonoBehaviour {

    HealthState m_Health;


    private enum enumInput
    {

    }



	// Use this for initialization
	void Start () {
        var health = m_Health.State;
	}
	
	// Update is called once per frame
	void Update () {
		
	}







}

/*
 * Playerキャラの
 * BaseCharaConditionを継承、キーボード入力処理を追加
 */
public class PlayerCondition : BaseCharaCondition
{

}


