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

    private MyClasses.BaseCharacter m_Condition = new MyClasses.BaseCharacter(100, 100, MyClasses.enumHealthState.NORMAL);

	// Use this for initialization
	void Start () {
        m_ThisHPBar = this.gameObject.GetComponentInChildren<EnemyHitPointBillBoard>() as EnemyHitPointBillBoard;
        m_ThisAnimator = this.gameObject.GetComponent<Animator>() as Animator;                                              //このスクリプトがアタッチされているゲームオブジェクトのアニメータを取得する

        string tagStr = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Player);         //PlayerタグのGameObjectのAttack_Damage_Controllerスクリプトを取得する
        m_Player = GameObject.FindGameObjectWithTag(tagStr).GetComponent<Attack_Damage_Controller>();
	}
	
	// Update is called once per frame
	void Update () {
    }


    int IDamage.Damage(MyClasses.enumAttackMeans means)
    {
        return m_Condition.AttackPoint(means);
    }

    void OnTriggerEnter(Collider collider)
    {
        string tagStr;
        tagStr = collider.tag;

        /*
         * string collider.tagをEnumeratorに変換し、switch文でEnumeratorを定数式に使用する
         * 
         * NinaLabo様ホームページ参照
         * http://ninagreen.hatenablog.com/entry/2015/08/25/201607
         */
        MyEnumerator.EnumeratorTag enumTag = (MyEnumerator.EnumeratorTag)System.Enum.Parse(typeof(MyEnumerator.EnumeratorTag), collider.tag);
        
        switch (enumTag)
        {
            case MyEnumerator.EnumeratorTag.UnityChanWeapon:             //UnityChanの武器に接触した場合
                {
                    string strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.ATTACK);
                    if (collider.GetComponentInParent<Animator>().GetCurrentAnimatorStateInfo(0).IsName(strAniStateName))       //UnityChanが攻撃モーション中であれば
                    {
                        int damage = ((IDamage)m_Player).Damage(MyClasses.enumAttackMeans.WEAPON);
                        int confirmedDamage = m_Condition.DamagedPoint(damage);
                        m_ThisHPBar.HitPointBarValue = (float)m_Condition.Life / m_Condition.MaxLife;
                        MyDebug.MyDebugLog.Log(this.name + " : 被ダメージ " + confirmedDamage + " : " + (float)m_Condition.Life / m_Condition.MaxLife);

                        GameObject particleSystem = GameObject.Find("UnityChanStaffEffect");
                        ParticleSystem pSystem = particleSystem.GetComponent<ParticleSystem>();

                        Vector3 pos = this.gameObject.transform.position;
                        pos.y += this.GetComponent<CharacterController>().height;

                        pSystem.transform.position = pos;
                        pSystem.transform.forward = this.gameObject.transform.up;

                        pSystem.Play();

                    }
                    else
                    {
                        MyDebug.MyDebugLog.Log("攻撃中でないのでダメージはありません。");
                    }
                }
                break;
            default:
                //武器以外に接触した場合はダメージを受けない
                break;
        }//switch

    }


    public MyClasses.enumHealthState EnemyHealthState
    {
        get { return m_Condition.HealthState; }
    }
    public MyClasses.enumAction EnemyAction
    {
        get { return m_Condition.Action; }
        private set { m_Condition.Action = value; }
    }

    /// <summary>
    /// enumActionにATTACK攻撃中をセット
    /// </summary>
    public void Attack()
    {
        EnemyAction = MyClasses.enumAction.ATTACK;
    }

    /// <summary>
    /// enumActionをNONEにリセットする
    /// </summary>
    public void ResetAction()
    {
        EnemyAction = MyClasses.enumAction.NONE;
    }

    //キャラクタの状態を正常(enumState.Normal)にする。
    //MyClasses.BaseCharacter m_Condition.m_State = MyClasses.enumState.NORMAL;を実行。
    public void EnemyStateNormal()
    {
        m_Condition.HealthState = MyClasses.enumHealthState.NORMAL;
    }
}
