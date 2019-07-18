using System.Collections;
using System.Collections.Generic;
using MyClasses;
using UnityEngine;



public class Attack_Damage_Controller : MonoBehaviour , IDamage
{

    [SerializeField]
    private Enemy_Attack_Damege_Controller m_Enemy = null;

    private MyClasses.BaseCharacter m_Condition = new MyClasses.BaseCharacter();


    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update ()
    {

    }



    int IDamage.Damage(enumAttackMeans means)
    {
        return m_Condition.AttackPoint(means);
    }

    //Restartボタンを押した場合
    public void Restart_ButtonClick()
    {
        try
        {
            GameObject obj = GameObject.FindGameObjectWithTag("Player");
            obj.GetComponent<ParticleSystem>().Play();
        }
        catch(UnityEngine.UnityException e)
        {
            Debug.Log(e.Message);
        }
        m_Condition.Resurrection = m_Condition.MaxLife;
    }


    //相手に接触した場合にどこに接触したか判定してダメージ計算を行う。
    //
    void OnTriggerEnter(Collider collider)
    {
        string tagStr;
        tagStr = collider.gameObject.tag;


        Enemy_Attack_Damege_Controller enemyDamageController = collider.GetComponentInParent<Enemy_Attack_Damege_Controller>();
        if(enemyDamageController == null)
        {
            Debug.Log("warning nullreference!" + "colider name : " + collider.name);
        }


        switch (tagStr)
        {
            case "EnemyWeapon":
                {
                    if (collider.transform.root.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
                    {
                        //                    int damage = ((IDamage)m_Enemy).Damage(enumAttackMeans.WEAPON);
                        Debug.Log(collider.name + " : WEAPON : 参照 = " + enemyDamageController);
                        int damage = ((IDamage)enemyDamageController).Damage(enumAttackMeans.WEAPON);
                        int confirmedDamage = m_Condition.DamagedPoint(damage);
                    }
                }
                break;
            case "Enemy":
                {
                    //                    int damage = ((IDamage)m_Enemy).Damage(enumAttackMeans.BODY);
                    Debug.Log(collider.name + " : BODY : 参照 = " + enemyDamageController);
                    int damage = ((IDamage)enemyDamageController).Damage(enumAttackMeans.BODY);
                    int confirmedDamage = m_Condition.DamagedPoint(damage);
                }
                break;
            default:
                {
//                    Debug.Log("OnTriggerEntered but Default.");
                }
                break;
        }



    }





    public int HitPoint
    {
        get { return this.m_Condition.Life; }
    }
    public int MaxHitPoint
    {
        get { return this.m_Condition.MaxLife; }
    }
    public MyClasses.enumState PlayerState
    {
        get { return m_Condition.State; }
    }
    public MyClasses.enumAction PlayerAction
    {
        get { return m_Condition.Action; }
        private set { m_Condition.Action = value; }
    }

    //Attackアクションをセット
    public void Attack()
    {
        PlayerAction = enumAction.ATTACK;
    }

    //アクションをリセット
    public void ResetAction()
    {
        PlayerAction = enumAction.NONE;
    }


    //キャラクタの状態を正常(enumState.Normal)にする。
    //MyClasses.BaseCharacter m_Condition.m_State = MyClasses.enumState.NORMAL;を実行。
    public void PlayerStateNormal()
    {
        m_Condition.State = enumState.NORMAL;
    }
}
