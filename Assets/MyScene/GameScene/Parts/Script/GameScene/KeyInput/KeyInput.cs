using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyInput
{

    public class KeyInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        delegate void InputFunctionDelegate();                  //デリゲート
        InputFunctionDelegate myKeyDownFunc;                    //Inputキー押下デリゲート
        InputFunctionDelegate myKeyUpFunc;                      //Inputキー離しデリゲート

        private bool m_IsPressed = false;
        private UnityEngine.UI.Text m_Text = null;

        [SerializeField,Tooltip("Player(UnityChan)の移動を制御するスクリプトをアタッチしてください")]
        private UnityChanController m_UnityChanController = null;
        [SerializeField, Tooltip("Player(UnityChan)のアクション（攻撃など）を制御するスクリプトをアタッチしてください)")]
        private Attack_Damage_Controller m_Attack_Damage_Controller = null;

        private static Vector3 m_InputValue = new Vector3();

        private Vector3 m_MousePos = new Vector3();

        private static float m_RotateDeg = 0.0f;

        //重力
        private const float m_Gravity = -9.8f;

        private void SetFunc()
        {
            m_MousePos = Input.mousePosition;
            m_Text.color = Color.red;
        }

        /// <summary>
        /// 押下処理
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsPressed = true;

            myKeyDownFunc();                                      //Inputキー押下処理

            SetFunc();
        }

        /// <summary>
        /// 押下終了処理
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            m_IsPressed = false;

            myKeyUpFunc();                                    //Inputキー離し処理

            ResetFunc();
        }


        // Use this for initialization
        void Start()
        {
            m_Text = GameObject.Find("Debug_Text").GetComponent<UnityEngine.UI.Text>();

            Debug.Log(gameObject.tag);

            //スクリプトのアタッチ対象に応じてdelegate myKeyDownFuncの中身を変更する
            MyEnumerator.EnumeratorTag enumTag = (MyEnumerator.EnumeratorTag)System.Enum.Parse(typeof(MyEnumerator.EnumeratorTag), gameObject.tag);
            switch (enumTag)
            {
                case MyEnumerator.EnumeratorTag.Touch_UP:                               //Touch_UPの場合
                    myKeyDownFunc = new InputFunctionDelegate(UpTouchDownFunc);         //押下処理を登録
                    myKeyUpFunc = new InputFunctionDelegate(UpTouchUpFunc);             //離し処理を登録
                    break;
                case MyEnumerator.EnumeratorTag.Touch_Down:                             //Touch_Downの場合
                    myKeyDownFunc = new InputFunctionDelegate(DownTouchDownFunc);       //押下処理を登録
                    myKeyUpFunc = new InputFunctionDelegate(DownTouchUpFunc);           //離し処理を登録
                    break;
                case MyEnumerator.EnumeratorTag.Touch_Left:                             //Touch_Leftの場合
                    myKeyDownFunc = new InputFunctionDelegate(LeftTouchDownFunc);       //押下処理を登録
                    myKeyUpFunc = new InputFunctionDelegate(LeftTouchUpFunc);           //離し処理を登録
                    break;
                case MyEnumerator.EnumeratorTag.Touch_Right:                            //Touch_Rightの場合
                    myKeyDownFunc = new InputFunctionDelegate(RightTouchDownFunc);      //押下処理を登録
                    myKeyUpFunc = new InputFunctionDelegate(RightTouchUpFunc);          //離し処理を登録
                    break;
                default:
                    Debug.Log(gameObject.tag);
                    break;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (m_IsPressed)
            {
                Vector3 tmpMousePos = Input.mousePosition;
                if (tmpMousePos != m_MousePos)
                {
                    m_Text.text = "押下中の移動";
                }
            }


#if UNITY_EDITOR
            m_InputValue = EditorInput(ref m_RotateDeg);
            GetKeys();
#endif
            m_UnityChanController.InputKeyVect = m_InputValue;
            m_UnityChanController.InputKeyRot = m_RotateDeg;
        }


        /// <summary>
        /// UPキー相当の押下処理
        /// </summary>
        private void UpTouchDownFunc()
        {
            m_Text.text = "UPが押下。";

            m_InputValue.z = 1.0f;

            m_InputValue.y = m_Gravity;
        }
        /// <summary>
        /// UPキー相当の離し処理
        /// </summary>
        private void UpTouchUpFunc()
        {
            m_InputValue.z = 0.0f;

            m_InputValue.y = m_Gravity;
        }

        /// <summary>
        /// Downキー相当の押下処理
        /// </summary>
        private void DownTouchDownFunc()
        {
            m_Text.text = "Downが押下";

            m_InputValue.z = -1.0f;
            m_InputValue.y = m_Gravity;
        }
        /// <summary>
        /// Downキー相当の離し処理
        /// </summary>
        private void DownTouchUpFunc()
        {
            m_InputValue.z = 0.0f;
            m_InputValue.y = m_Gravity;
        }

        /// <summary>
        /// Leftキー相当の押下処理
        /// </summary>
        private void LeftTouchDownFunc()
        {
            m_Text.text = "Leftが押下";

            m_RotateDeg = 1.0f;
            m_InputValue.y = m_Gravity;
        }
        /// <summary>
        /// Leftキー相当の離し処理
        /// </summary>
        private void LeftTouchUpFunc()
        {
            m_RotateDeg = 0.0f;
            m_InputValue.y = m_Gravity;
        }

        /// <summary>
        /// Rightキー相当の押下処理
        /// </summary>
        private void RightTouchDownFunc()
        {
            m_Text.text = "Rightが押下";

            m_RotateDeg = -1.0f;
            m_InputValue.y = m_Gravity;
        }
        /// <summary>
        /// Rightキー相当の離し処理
        /// </summary>
        private void RightTouchUpFunc()
        {
            m_RotateDeg = 0.0f;
            m_InputValue.y = m_Gravity;
        }

        private void ResetFunc()
        {
            m_Text.text = "押下されていません。";
            m_Text.color = Color.green;
        }

#if UNITY_EDITOR
        /// <summary>
        /// UnityEditorでのInput処理(移動、回転成分)
        /// </summary>
        /// <param name="yRot"></param>
        /// <returns></returns>
        private Vector3 EditorInput(ref float yRot)
        {
            float x = Input.GetAxis("SideStep");            //ローカル座標X軸方向の移動
            float y = m_Gravity;                            //ローカル座標Y軸方向の移動(重力)
            float z = Input.GetAxis("Vertical");            //ローカル座標Z軸方向の移動

            yRot = Input.GetAxis("Horizontal");             //ローカル座標Y軸周りの回転

            return new Vector3(x,y,z);
        }
#endif

#if UNITY_EDITOR
        /// <summary>
        /// UnityEditor上でのInput処理(移動、回転以外)
        /// </summary>
        private void GetKeys()
        {
            bool spaceKey = Input.GetKey(KeyCode.Space);

            if(spaceKey)
            {
                Debug.Log("GETKEYS()");
                m_Attack_Damage_Controller.Attack();
            }
        }
#endif






    }
}
