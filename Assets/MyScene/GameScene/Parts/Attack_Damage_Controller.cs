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
        m_Condition.Resurrection = m_Condition.MaxLife;
    }



    void OnTriggerEnter(Collider collider)
    {
        string tagStr;
        tagStr = collider.gameObject.tag;

        switch (tagStr)
        {
            case "EnemyWeapon":
                {
                    int damage = ((IDamage)m_Enemy).Damage(enumAttackMeans.WEAPON);
                    int confirmedDamage = m_Condition.DamagedPoint(damage);   
                }
                break;
            case "Enemy":
                {
                    int damage = ((IDamage)m_Enemy).Damage(enumAttackMeans.BODY);
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

    //キャラクタの状態を正常(enumState.Normal)にする。
    //MyClasses.BaseCharacter m_Condition.m_State = MyClasses.enumState.NORMAL;を実行。
    public void PlayerStateNormal()
    {
        m_Condition.State = enumState.NORMAL;
    }
}
