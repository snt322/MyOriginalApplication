using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_Attack_Damege_Controller : MonoBehaviour , IDamage
{
    [SerializeField]
    private Attack_Damage_Controller m_Player = null;

    [SerializeField]
    private Animator m_ThisAnimator = null;

    private MyClasses.BaseCharacter m_Condition = new MyClasses.BaseCharacter(100, 100, MyClasses.enumState.NORMAL);

	// Use this for initialization
	void Start () {
        m_ThisAnimator = this.gameObject.GetComponent<Animator>() as Animator;            //このスクリプトがアタッチされているゲームオブジェクトのアニメータを取得する
	}
	
	// Update is called once per frame
	void Update () {
//        Debug.Log("Enemy : " + ((ITestInterface)m_Player).OverrideMe(""));
    }


    int IDamage.Damage(MyClasses.enumAttackMeans means)
    {
        return m_Condition.AttackPoint(means);
    }
}
