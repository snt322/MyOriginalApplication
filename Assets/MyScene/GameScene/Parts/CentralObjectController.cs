using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CentralObjectController : MonoBehaviour {

    /*
     * Playerキャラ、Enemyキャラの管理を行うスクリプト
     * 
     * 
     */

    private GameObject m_Player = null;
    private List<GameObject> m_ListEnemyObj = null;                       //フィールドに表示している敵の一覧リスト


    private void Awake()
    {
        m_ListEnemyObj = new List<GameObject>();                        //敵リストの初期化
    }





    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    
    public GameObject Player
    {
        set { m_Player = value; }
        get { return m_Player; }
    }
    public List<GameObject> Enemy
    {
        private set { m_ListEnemyObj = value; }
        get { return m_ListEnemyObj; }
    }



}
