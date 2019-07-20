using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

/*
 * シーンの制御の制御をまとめて実行するスクリプト
 * シーンの制御とは
 * ①UIの表示(テキストHP、HP表示バー、メニューボタンなど)
 * 
 */ 






public class CentralUIController : MonoBehaviour {

    [SerializeField]
    private GameObject m_Player = null;



    [SerializeField]
    private Text m_HitPoint = null;

    [SerializeField]
    private Image m_HitPointProgressBar = null;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        int playerHitPoint = GetPlayerHitPoint();                   //プレイヤーの現在のヒットポイントを取得
        int playerMaxHitPoint = GetPlayerHitPoint(true);            //プレイヤーの最大ヒットポイントを取得

        float ratio = (float)playerHitPoint / (float)playerMaxHitPoint;

        UpdatePlayerHitPoint(playerHitPoint);
        UpdatePlayerHitPointProgressBar(ratio);

	}

    private int GetPlayerHitPoint(bool isMaxHitPoint = false)
    {
        Attack_Damage_Controller controller = m_Player.GetComponent<Attack_Damage_Controller>() as Attack_Damage_Controller;
        if(controller != null)
        {
            if(isMaxHitPoint)
            {
                return controller.MaxHitPoint;
            }
            else
            {
                return controller.HitPoint;
            }
        }

        return -1;

    }


    private void UpdatePlayerHitPoint(int hitpoint)
    {
        if(m_HitPoint != null)
        {
            m_HitPoint.text = string.Format("HP:{0}", hitpoint);
        }
    }

    //ImageTypeはfillにしていること
    private void UpdatePlayerHitPointProgressBar(float ratio)
    {

        float setValue = Mathf.Clamp01(ratio);

        if(m_HitPointProgressBar != null)
        {
 //           Debug.Log("UpdatePlayerHitPointProgressBar : " + ratio);
            m_HitPointProgressBar.fillAmount = setValue;
        }
    }
}
