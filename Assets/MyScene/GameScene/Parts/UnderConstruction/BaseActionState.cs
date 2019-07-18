using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * キャラクタの行動の状態を管理する
 */

    public enum enumActions
    {
        None,
        Attack,
        Defence,
        Move,
        HIT,                        //取り合えず何かにぶつかった
    }

public class BaseActionState : MonoBehaviour {


    private enumActions m_Action;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        
	}



    public enumActions Action
    {
        get { return this.m_Action; }
    }

}
