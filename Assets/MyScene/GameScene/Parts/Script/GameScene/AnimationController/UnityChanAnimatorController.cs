using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//AnimatorParameterName
static public class AnimatorParaName
{
    static readonly string m_Speed = "SPEED";
    static readonly string m_Turn = "TURN";
    static readonly string m_IsDie = "DIE";
    static readonly string m_GetDamage = "GET_DAMAGE";
    static readonly string m_Attack = "ATTACK";
    static readonly string m_Recover = "RECOVER";
    static readonly string m_MagicExplosion = "MAGIC";

    static public string Speed
    {
        get { return m_Speed; }
    }
    static public string Turn
    {
        get { return m_Turn; }
    }
    static public string Die
    {
        get { return m_IsDie; }
    }
    static public string Get_Damage
    {
        get { return m_GetDamage; }
    }
    static public string Attack
    {
        get { return m_Attack; }
    }
    static public string Recover
    {
        get { return m_Recover; }
    }
    static public string Magic
    {
        get { return m_MagicExplosion; }
    }
}


public class UnityChanAnimatorController : MonoBehaviour {
    [SerializeField]
    private CharacterController m_CharacterController = null;

    [SerializeField]
    private Animator m_Animator = null;

    [SerializeField]
    private Attack_Damage_Controller m_AttackDamageController = null;
    

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        //アニメーションのパラメータをセット
        SetAnimatorParameter();
        
    }
    /* 
     * Attack_Damage_Controllerスクリプトの
    *  メンバークラスMyClasses.BaseCharacter m_Condition.m_Stateに応じてアニメーションを変更する
    *  AnimatorControllerにParameteをセットする
    * */
    private void SetAnimatorParameter()
    {
        float frontBackSpeed;
        float turnSpeed;

        Vector3 v = m_CharacterController.velocity;                             //velocityはワールド座標系での速度となる
        v = transform.InverseTransformDirection(v);                             //velocityをワールド座標系からローカル座標系に変換

        frontBackSpeed = v.z;
        turnSpeed = -v.x;


        m_Animator.SetFloat(AnimatorParaName.Speed, frontBackSpeed);
        m_Animator.SetFloat(AnimatorParaName.Turn, turnSpeed);

        switch(m_AttackDamageController.PlayerHealthState)
        {
            case MyClasses.enumHealthState.ALMOST_DYING:
                //今のところ瀕死モーションは実装しない。
                break;
            case MyClasses.enumHealthState.NORMAL:                        //正常な状態
                m_Animator.SetInteger(AnimatorParaName.Die, 1);
                break;
            case MyClasses.enumHealthState.DEAD:                          //倒された場合
                m_Animator.SetInteger(AnimatorParaName.Die, -1);
                break;
            case MyClasses.enumHealthState.GET_DAMAGE:                    //ダメージを受けた場合
                m_Animator.SetBool(AnimatorParaName.Get_Damage, true);
                break;
        }

        //実行中のアニメーションが連続で実行されないようにする
        string strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.DAMAGE);
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(strAniStateName))        //ダメージを受けたアニメーションを開始したら
        {
            m_Animator.SetBool(AnimatorParaName.Get_Damage, false);                  //ダメージを受けたモーションのフラグをクリア
            m_AttackDamageController.PlayerStateNormal();                            //PlayerState=Normalにするメソッドを呼び出す。
        }

        switch(m_AttackDamageController.PlayerAction)
        {
            case MyClasses.enumAction.NONE:
                m_Animator.SetBool(AnimatorParaName.Attack, false);
                m_Animator.SetBool(AnimatorParaName.Recover, false);
                break;
            case MyClasses.enumAction.RECOVER:
                m_Animator.SetBool(AnimatorParaName.Recover, true);
                break;
            case MyClasses.enumAction.ATTACK:
                m_Animator.SetBool(AnimatorParaName.Attack, true);
                break;
            case MyClasses.enumAction.MAGIC_EXPLOSION:
                m_Animator.SetBool(AnimatorParaName.Magic, true);
                break;
            default:
                break;
        }

        //実行中のアニメーションが連続で実行されないようにする
        strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.ATTACK);
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(strAniStateName))
        {
            m_Animator.SetBool(AnimatorParaName.Attack, false);                     //攻撃アニメーションのフラグをクリア
            m_AttackDamageController.ResetAction();                                 //攻撃行動のフラグをクリア
        }
        strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.RECOVER);
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName(strAniStateName))
        {
            m_Animator.SetBool(AnimatorParaName.Recover, false);                     //回復アニメーションのフラグをクリア
            m_AttackDamageController.ResetAction();                                  //回復行動のフラグをクリア
        }
        strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.MAGIC);
        if(m_Animator.GetCurrentAnimatorStateInfo(0).IsName(strAniStateName))
        {
            m_Animator.SetBool(AnimatorParaName.Magic, false);                       //爆発魔法の詠唱アニメーションのフラグをクリア
            m_AttackDamageController.ResetAction();                                  //爆発魔法行動のフラグをクリア
        }


    }

    /// <summary>
    /// アニメーションRecoveryを再生中かどうかを返す
    /// </summary>
    public bool isRecover_AnimationPlaying()
    {
               string strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.RECOVER);
//        string strAniStateName = "RECOVER";
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName(strAniStateName);
    }
    /// <summary>
    /// アニメーションDAMAGEを再生中かどうかを返す
    /// </summary>
    /// <returns></returns>

    public bool isDAMAGE_AnimationPlaying()
    {
        string strAniStateName = System.Enum.GetName(typeof(MyAnimationStateNames.StateNames), MyAnimationStateNames.StateNames.DAMAGE);
        return m_Animator.GetCurrentAnimatorStateInfo(0).IsName(strAniStateName);
    }

}
