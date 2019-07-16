using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCameraController : MonoBehaviour {

//    [SerializeField]
//    Camera m_MainCamera = null;
//    [SerializeField]
//    Transform m_Target = null;


//    private Vector3 m_CamToTarget;

    [SerializeField]
    private bool m_TherdPersonPerspective = true;


    [SerializeField, Range(-10, 10), TooltipAttribute("三人称視点：Unityちゃんに対するカメラ位置、Unityちゃんのローカル座標")]
    private float m_Xpos = 0.0f;
    [SerializeField, Range(-10, 10), TooltipAttribute("三人称視点：Unityちゃんに対するカメラ位置、Unityちゃんのローカル座標")]
    private float m_Ypos = 1.0f;
    [SerializeField, Range(-10, 10), TooltipAttribute("三人称視点：Unityちゃんに対するカメラ位置、Unityちゃんのローカル座標")]
    private float m_Zpos = 0.0f;
    

    // Use this for initialization
    void Start () {
//        m_CamToTarget = new Vector3();
	}
	
	// Update is called once per frame
	void Update () {

    }

    //3人称視点にカメラをセットする
    public void SetCameraPosDir(bool isThirdPerson)
    {
        float x = 0, y = 0, z = 0;

        /*
         * 2019/07/13
         * OnValidate()内でTransform.set_parentを実行するとWarning
         * "SendMessage cannot be called during Awake, CheckConsistency, or OnValidate
         *  UnityEngine.Transform:set_parent(Transform)
         *  MainCameraController:SetCameraPosDir(Boolean) (at Assets/MyScene/StartMenu/StartMenu/MainCameraController.cs:49)
         *  MainCameraController:OnValidate() (at Assets/MyScene/StartMenu/StartMenu/MainCameraController.cs:76)"
         *  が出力される。
         *  さらに、cameraにこのスクリプトをアタッチした瞬間に「カメラがDestroyされた？」ことがあり原因が不明。
         *  再度カメラがDestroyされるようであればTransform.set_parentをOnValidate()以外(例えばUpdate)で実行に変更すること!!!!!
         *
         * 参考URL:https://forum.unity.com/threads/sendmessage-cannot-be-called-during-awake-checkconsistency-or-onvalidate.428580/
         */

        if (isThirdPerson)
        {
            //三人称視点
            if ((GameObject.FindGameObjectWithTag("ThirdPersonPerspective").GetComponent<Transform>() as Transform) != null)
            {
                gameObject.GetComponent<Transform>().parent = null;
                gameObject.GetComponent<Transform>().parent = GameObject.FindGameObjectWithTag("ThirdPersonPerspective").GetComponent<Transform>().parent;

                x = m_Xpos;
                y = m_Ypos;
                z = m_Zpos;
            }
        }
        else
        {
            if ((GameObject.FindGameObjectWithTag("FirstPersonPerspective").GetComponent<Transform>() as Transform) != null)
            {
                //一人称視点
                gameObject.GetComponent<Transform>().parent = null;
                gameObject.GetComponent<Transform>().parent = GameObject.FindGameObjectWithTag("FirstPersonPerspective").GetComponent<Transform>();

                x = -m_Ypos;
                y = m_Zpos;
                z = m_Xpos;
            }
        }

        Vector3 tPos = new Vector3(x, y, z);                         //このオブジェクトの相対位置
        gameObject.GetComponent<Transform>().localPosition = tPos;                  //相対位置をセットする
    }

    private void OnValidate()
    {
        SetCameraPosDir(m_TherdPersonPerspective);
    }

}
