using System.Collections;
using System.Collections.Generic;
using MyClasses;
using UnityEngine;



public class Attack_Damage_Controller : MonoBehaviour , IDamage
{

    private MyClasses.BaseCharacter m_Condition = new MyClasses.BaseCharacter();

    // Use this for initialization
    void Start () {
	}
	

    /// <summary>
    /// 
    /// </summary>
    /// <param name="means"></param>
    /// <returns></returns>
    int IDamage.Damage(enumAttackMeans means)
    {
        return m_Condition.AttackPoint(means);
    }

    //Restartボタンを押した場合
    public void Restart_ButtonClick()
    {
        string tagStr = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Player);

        GameObject obj = GameObject.FindGameObjectWithTag(tagStr);                  //Player
        obj.GetComponent<ParticleSystem>().Play();

        m_Condition.Resurrection = m_Condition.MaxLife;

        m_Condition.HealthState = enumHealthState.NORMAL;
    }


    //相手に接触した場合にどこに接触したか判定してダメージ計算を行う。
    //
    void OnTriggerEnter(Collider collider)
    {
        //タグをEnumに変換
        MyEnumerator.EnumeratorTag enumTag = (MyEnumerator.EnumeratorTag)System.Enum.Parse(typeof(MyEnumerator.EnumeratorTag), collider.tag);

        switch (enumTag)
        {
            case MyEnumerator.EnumeratorTag.EnemyWeapon:                    //EnemyWeaponに接触した場合
                {
                    string strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.ATTACK);
                    if (collider.transform.root.gameObject.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(strAniStateName))
                    {
                        Enemy_Attack_Damege_Controller enemyDamageController = collider.GetComponentInParent<Enemy_Attack_Damege_Controller>();
                        int damage = ((IDamage)enemyDamageController).Damage(enumAttackMeans.WEAPON);

                        MyDebug.MyDebugLog.Log(collider.name + " : WEAPON : 参照 = " + enemyDamageController);
                        m_Condition.DamagedPoint(damage);
                    }
                }
                break;
            case MyEnumerator.EnumeratorTag.Enemy:
                {
                    Enemy_Attack_Damege_Controller enemyDamageController = collider.GetComponentInParent<Enemy_Attack_Damege_Controller>();
                    int damage = ((IDamage)enemyDamageController).Damage(enumAttackMeans.BODY);
                    m_Condition.DamagedPoint(damage);

                    MyDebug.MyDebugLog.Log(collider.name + " : WEAPON : 参照 = " + enemyDamageController);
                    Debug.Log(collider.name + " : BODY : 参照 = " + enemyDamageController);
                }
                break;
            default:
                {
                    MyDebug.MyDebugLog.Log("OnTriggerEntered but Default.");
                }
                break;
        }



    }

    private bool canUse()
    {
        bool flag = true;

        if((m_Condition.HealthState == enumHealthState.DEAD) || (m_Condition.HealthState == enumHealthState.GET_DAMAGE))
        {
            flag = false;
        }

        return flag;
    }

    /// <summary>
    /// PlayerAction = enumAction.MAGIC_EXPLOSIONをセットする
    /// </summary>
    public void UseMagic()
    {
        PlayerAction = enumAction.MAGIC_EXPLOSION;
    }



    public int HitPoint
    {
        get { return this.m_Condition.Life; }
    }
    public int MaxHitPoint
    {
        get { return this.m_Condition.MaxLife; }
    }
    public MyClasses.enumHealthState PlayerHealthState
    {
        get { return m_Condition.HealthState; }
    }
    public MyClasses.enumAction PlayerAction
    {
        get { return m_Condition.Action; }
        private set { m_Condition.Action = value; }
    }

    /// <summary>
    /// enumActionにATTACK攻撃中をセット
    /// </summary>
    public void Attack()
    {
        PlayerAction = enumAction.ATTACK;
    }

    /// <summary>
    /// enumActionをNONEにリセットする
    /// </summary>
    public void ResetAction()
    {
        PlayerAction = enumAction.NONE;
    }

    /// <summary>
    /// キャラクタの状態を正常(enumState.Normal)にする。 
    /// MyClasses.BaseCharacter m_Condition.m_State = MyClasses.enumState.NORMAL;を実行する
    /// </summary>
    public void PlayerStateNormal()
    {
        m_Condition.HealthState = enumHealthState.NORMAL;
    }
}
