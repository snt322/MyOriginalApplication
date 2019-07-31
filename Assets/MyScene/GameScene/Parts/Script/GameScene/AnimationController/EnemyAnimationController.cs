using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour {

    [SerializeField]
    private CharacterController m_CharacterController = null;

    [SerializeField]
    private Animator m_Animator = null;

    [SerializeField]
    private Enemy_Attack_Damege_Controller m_Enemy_Attack = null;


	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //アニメーションのパラメータをセット
        SetAnimatorParameter();
	}

    /*
     *  Enemy_Attack_Damage_Controllerスクリプトの 
     *  メンバークラスMyClasses.BaseCharacter m_Condition.m_Stateに応じてアニメーションを変更する
     *  EnemyAnimatorControllerにParameterをセットする
     */
     private void SetAnimatorParameter()
    {
        float frontBackSpeed;
        float turnSpeed;

        //要変更
        if (m_CharacterController != null)
        {
            Vector3 v = m_CharacterController.velocity;                             //velocityはワールド座標系での速度となる
            v = transform.InverseTransformDirection(v);                             //velocityをワールド座標系からローカル座標系に変換

            frontBackSpeed = v.z;
            turnSpeed = -v.x;
            
            m_Animator.SetFloat(AnimatorParaName.Speed, frontBackSpeed);
            m_Animator.SetFloat(AnimatorParaName.Turn, turnSpeed);
        }

        switch (m_Enemy_Attack.EnemyHealthState)
        {
            case MyClasses.enumHealthState.ALMOST_DYING:
                //今のところ瀕死モーションは実装しない。
                break;
            case MyClasses.enumHealthState.NORMAL:                        //正常な状態
                m_Animator.SetInteger(AnimatorParaName.Die, 0);
                break;
            case MyClasses.enumHealthState.DEAD:                          //倒された場合
                m_Animator.SetInteger(AnimatorParaName.Die, -1);
                break;
            case MyClasses.enumHealthState.GET_DAMAGE:                    //ダメージを受けた場合
                m_Animator.SetBool(AnimatorParaName.Get_Damage, true);
                break;
        }

        //実行中のアニメーションが連続で実行されないようにする
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("DAMAGE"))              //ダメージを受けたアニメーションを開始したら
        {
            m_Animator.SetBool(AnimatorParaName.Get_Damage, false);                  //ダメージを受けたモーションのフラグをクリア
            m_Enemy_Attack.EnemyStateNormal();                            //PlayerState=Normalにするメソッドを呼び出す。
        }

        switch (m_Enemy_Attack.EnemyAction)
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
            default:
                break;
        }

        //実行中のアニメーションが連続で実行されないようにする
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("ATTACK"))
        {
            m_Animator.SetBool(AnimatorParaName.Attack, false);                     //攻撃アニメーションのフラグをクリア
            m_Enemy_Attack.ResetAction();                                           //攻撃行動のフラグをクリア
        }
        if (m_Animator.GetCurrentAnimatorStateInfo(0).IsName("RECOVER"))
        {
            m_Animator.SetBool(AnimatorParaName.Recover, false);                     //攻撃アニメーションのフラグをクリア
            m_Enemy_Attack.ResetAction();                                            //攻撃行動のフラグをクリア
        }


    }




}
