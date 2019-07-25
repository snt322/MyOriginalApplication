using System.Collections;
using System.Collections.Generic;
using UnityEngine;




[RequireComponent(typeof(CharacterController))]
//[RequireComponent(typeof(Animator))]
public class UnityChanController : MonoBehaviour {

    [SerializeField]
    private Attack_Damage_Controller m_Attack_Damage_Controller = null;                 //Attack_Damage_Controllerスクリプト内でPlayerの状態を管理しているので取得する

    [SerializeField]
    private UnityChanAnimatorController m_UnityChanAnimatorController= null;

    //最大移動速度
    private float m_MaxSpeed = 5;
    //最大回転速度
    private float m_MaxRot = 180;
    //最大バックスピード
    private float m_MaxBackSpeed = 1;

    //重力
    private float m_Gravity = -9.8f;

    //CharacterController
    private CharacterController m_ChController = null;
    
    // Use this for initialization
    void Start () {
        m_ChController = gameObject.GetComponent<CharacterController>() as CharacterController;
    }
	
	// Update is called once per frame
	void Update () {

        float rotDeg = 0;
        Vector3 keyInput = GetInput(ref rotDeg);    //戻り値はキャラクタのローカル座標方向のキー入力、戻り値ref rotDegはローカル座標Y軸周りの回転

        bool isUnityChanNotDead = m_Attack_Damage_Controller.PlayerHealthState != MyClasses.enumHealthState.DEAD;          //UnityChanが倒された場合
        bool isUnityChanDamageMotion = m_UnityChanAnimatorController.isDAMAGE_AnimationPlaying();                          //UnityChanがダメージを受けるモーションを実行中?
        bool isUnityChanRecoverMotion = m_UnityChanAnimatorController.isRecover_AnimationPlaying();
        if (isUnityChanNotDead && !isUnityChanDamageMotion && !isUnityChanRecoverMotion)                                    //UnityChanが倒されている、または、被ダメージモーション中、または、回復モーション中の場合はUnityChanは行動できない
        {
            Move(keyInput, rotDeg);                     //キャラクタの移動、回転の実行
        }


        GetKeys();                                  //キーボードから攻撃などの動作入力を受け取る


    }

    //キー入力(平行移動、回転移動以外)
    void GetKeys()
    {
        bool spaceKey = Input.GetKey(KeyCode.Space);

        if (spaceKey)
        {
            Debug.Log("GETKEYS()");
            m_Attack_Damage_Controller.Attack();
        }
    }


    //キー入力平行移動成分
    Vector3 GetInput(ref float yRot)
    {
        float x = Input.GetAxis("SideStep");        //ローカル座標X軸方向の移動
        float y = m_Gravity;                        //ローカル座標Y軸方向の移動(重力)
        float z = Input.GetAxis("Vertical");        //ローカル座標Z軸方向の移動

        yRot = Input.GetAxis("Horizontal");         //ローカル座標Y軸周りの回転

        return new Vector3(x,y,z);
    }

    //void Move
    //モデルを回転、移動させる
    //モデルは1人称視点の回転、移動をする
    private void Move(Vector3 movDistPerFrame, float rotDegPerFrame)
    {
        //参考URL https://docs.unity3d.com/ScriptReference/CharacterController.SimpleMove.html


        //movDistPerFrameはモデルのローカル座標での移動方向
        Vector3 moveDir = new Vector3(movDistPerFrame.x, movDistPerFrame.y, movDistPerFrame.z);

        //バックの場合
        if(moveDir.z < 0)
        {
            moveDir 
                = moveDir * m_MaxBackSpeed * Time.deltaTime;
        }
        else
        {
            moveDir = moveDir * m_MaxSpeed * Time.deltaTime;
        }


        //ローカル座標の移動方向をワールド座標へ変換
        moveDir = gameObject.transform.TransformDirection(moveDir);

        //回転
        float rotDeg = rotDegPerFrame * m_MaxRot * Time.deltaTime;
        gameObject.transform.Rotate(0, rotDeg, 0);
        
        //移動
        m_ChController.Move(moveDir);
//        m_ChController.SimpleMove(moveDir);

    }



}
