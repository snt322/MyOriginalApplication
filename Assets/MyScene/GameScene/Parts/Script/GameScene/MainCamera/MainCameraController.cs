using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEditor;

public class MainCameraController : MonoBehaviour
{

    GameObject m_GObjThirdPerson = null;                //三人称視点時のMainCameraの親オブジェクト
    GameObject m_GObjFirstPerson = null;                //一人称視点時のMainCameraの親オブジェクト


    [SerializeField]
    private bool m_ThirdPersonPerspective;


    [SerializeField, Range(-10, 10), TooltipAttribute("三人称視点：Unityちゃんに対するカメラ位置、Unityちゃんのローカル座標")]
    private float m_Xpos;
    [SerializeField, Range(-10, 10), TooltipAttribute("三人称視点：Unityちゃんに対するカメラ位置、Unityちゃんのローカル座標")]
    private float m_Ypos;
    [SerializeField, Range(-10, 10), TooltipAttribute("三人称視点：Unityちゃんに対するカメラ位置、Unityちゃんのローカル座標")]
    private float m_Zpos;



    // Use this for initialization
    void Start()
    {
        //enumeratorからstringを取得する
        string strFirst = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.FirstPersonPerspective);
        string strThird = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.ThirdPersonPerspective);

        m_GObjThirdPerson = GameObject.FindGameObjectWithTag(strThird) as GameObject;
        m_GObjFirstPerson = GameObject.FindGameObjectWithTag(strFirst) as GameObject;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// 一人称、三人称視点の切り替え
    /// </summary>
    /// <param name="isThirdPerson"></param>
    public void SetCameraPosDir(bool isThirdPerson)
    {
        float x = 0, y = 0, z = 0;

        Transform camTform;                                     //カメラのTransform
        Transform parentTform;                                  //カメラの親オブジェクトのTransform

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
         *  参考URL:https://forum.unity.com/threads/sendmessage-cannot-be-called-during-awake-checkconsistency-or-onvalidate.428580/
         *  
         *  2019/07/20
         *  Transformの親のセットはTransform.parentのセッタを使用しないこと
         *  Transform.SetParent()でエラーが出なくなった。
         *  OnValidate等とも無関係なエラーだった。
         */

        if (isThirdPerson)
        {
            //三人称視点
            if (m_GObjThirdPerson != null)
            {
                camTform = gameObject.GetComponent<Transform>() as Transform;                 //カメラのTransformを取得
                parentTform = m_GObjThirdPerson.GetComponent<Transform>() as Transform;       //カメラの親オブジェクトのTransformを取得

                camTform.SetParent(parentTform);

                camTform.up = parentTform.up;
                camTform.forward = parentTform.forward;

                x = m_Xpos;
                y = m_Ypos;
                z = m_Zpos;

                Vector3 tPos = new Vector3(x, y, z);                                        //このオブジェクトの相対位置
                gameObject.GetComponent<Transform>().localPosition = tPos;                  //相対位置をセットする

            }
        }
        else
        {
            if (m_GObjFirstPerson != null)
            {
                //一人称視点

                camTform = gameObject.GetComponent<Transform>() as Transform;                 //カメラのTransformを取得
                parentTform = m_GObjThirdPerson.GetComponent<Transform>() as Transform;       //カメラの親オブジェクトのTransformを取得
                camTform.SetParent(parentTform);

                camTform.up = parentTform.up;
                camTform.forward = parentTform.forward;

                x = -m_Ypos;
                y = m_Zpos;
                z = m_Xpos;

                Vector3 tPos = new Vector3(x, y, z);                                        //このオブジェクトの相対位置
                gameObject.GetComponent<Transform>().localPosition = tPos;                  //相対位置をセットする
            }
        }

    }

    /*
        /// <summary>
        /// エディタ拡張を習得してから修正すること
        /// </summary>
        private void OnValidate()
        {
            SetCameraPosDir(m_ThirdPersonPerspective);
        }
    */
/*
    [CustomEditor(typeof(MainCameraController))]//拡張するクラスを指定
    public class MainCameraControllerEditor : Editor
    {
        /// <summary>
        /// InspectorのGUIを更新
        /// </summary>
        public override void OnInspectorGUI()
        {
            //元のInspector部分を表示
            base.OnInspectorGUI();
            var mainCameraController = target as MainCameraController;
            //ボタンを表示
            if (GUILayout.Button("SetCameraPosDir"))
            {
                mainCameraController.SetCameraPosDir(!mainCameraController.m_ThirdPersonPerspective);
            }
        }
    }
*/
}
