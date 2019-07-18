using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Enemy_Attack_Damege_Controller : MonoBehaviour , IDamage
{
    [SerializeField]
    private Attack_Damage_Controller m_Player = null;

    [SerializeField]
    private Animator m_ThisAnimator = null;

    private EnemyHitPointBillBoard m_ThisHPBar = null;

    private MyClasses.BaseCharacter m_Condition = new MyClasses.BaseCharacter(100, 100, MyClasses.enumState.NORMAL);

	// Use this for initialization
	void Start () {
        m_ThisHPBar = this.gameObject.GetComponentInChildren<EnemyHitPointBillBoard>() as EnemyHitPointBillBoard;
        m_ThisAnimator = this.gameObject.GetComponent<Animator>() as Animator;            //このスクリプトがアタッチされているゲームオブジェクトのアニメータを取得する
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Attack_Damage_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
//        Debug.Log("Enemy : " + ((ITestInterface)m_Player).OverrideMe(""));
    }


    int IDamage.Damage(MyClasses.enumAttackMeans means)
    {
        return m_Condition.AttackPoint(means);
    }

    void OnTriggerEnter(Collider colider)
    {
        string tagStr;
        tagStr = colider.tag;

        Debug.Log("enemage OnTriggerEnter");

        switch (tagStr)
        {
            case "UnityChanWeapon":                     //UnityChanの武器に接触した場合
                {
                    if (colider.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))       //UnityChanが攻撃モーション中であれば
                    {
                        int damage = ((IDamage)m_Player).Damage(MyClasses.enumAttackMeans.WEAPON);
                        int confirmedDamage = m_Condition.DamagedPoint(damage);
                        m_ThisHPBar.HitPointBarValue = (float)m_Condition.Life / m_Condition.MaxLife;
                        Debug.Log(this.name + " : 被ダメージ " + confirmedDamage + " : " + (float)m_Condition.Life / m_Condition.MaxLife);
                    }
                    else
                    {
                        Debug.Log("攻撃中でないのでダメージはありません。");
                    }
                }
                break;
            default:
                //武器以外に接触した場合はダメージを受けない
                break;
        }

    }


    public MyClasses.enumState EnemyState
    {
        get { return m_Condition.State; }
    }
    public MyClasses.enumAction EnemyAction
    {
        get { return m_Condition.Action; }
        private set { m_Condition.Action = value; }
    }

    public void ResetAction()
    {
        EnemyAction = MyClasses.enumAction.NONE;
    }

    //キャラクタの状態を正常(enumState.Normal)にする。
    //MyClasses.BaseCharacter m_Condition.m_State = MyClasses.enumState.NORMAL;を実行。
    public void EnemyStateNormal()
    {
        m_Condition.State = MyClasses.enumState.NORMAL;
    }
}
