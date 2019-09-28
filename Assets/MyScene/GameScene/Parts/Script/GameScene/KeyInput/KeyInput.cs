//#define MYDEBUG

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MyInput
{
    public class KeyInput : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IEventSystemHandler
    {
        delegate void InputFunctionDelegate();                  //デリゲート
        InputFunctionDelegate myKeyDownFunc;                    //Inputキー押下デリゲート
        InputFunctionDelegate myKeyUpFunc;                      //Inputキー離しデリゲート

        /// <summary>
        /// タッチ中の判定フラグ
        /// </summary>
        private bool m_IsPressed = false;

#if MYDEBUG
        /// <summary>
        /// デバッグ用CanvasのUI.Text
        /// </summary>
        private UnityEngine.UI.Text m_DebugText = null;
#endif
        [SerializeField,Tooltip("Player(UnityChan)の移動を制御するスクリプトをアタッチしてください")]
        private UnityChanController m_UnityChanController = null;

#if UNITY_EDITOR
        [SerializeField, Tooltip("Player(UnityChan)のアクション（攻撃など）を制御するスクリプトをアタッチしてください")]
        private Attack_Damage_Controller m_Attack_Damage_Controller = null;
#endif

        /// <summary>
        /// キーボード入力から取得したPlayerの移動ベクトル
        /// </summary>
        private static Vector3 m_KeyValue = new Vector3();
        /// <summary>
        /// タッチパネル入力から取得したPlayerの移動ベクトル
        /// </summary>
        private static Vector3 m_TouchValue = new Vector3();
        /// <summary>
        /// Playerの移動ベクトル、キーボードとタッチパネルの入力の合算。最大長さ1.0のベクトル
        /// </summary>
        private static Vector3 m_InputValue = new Vector3();


        private Vector3 m_MousePos = new Vector3();

        /// <summary>
        /// キーボード入力から取得したPlayerのY軸周りの回転速度
        /// </summary>
        private static float m_KeyRotateDeg = 0.0f;
        /// <summary>
        /// タッチパネル入力から取得したPlayerのY軸周りの回転速度
        /// </summary>
        private static float m_TouchRotateDeg = 0.0f;
        /// <summary>
        /// Playerの回転速度、キーボードとタッチパネル入力の合算。最大1.0
        /// </summary>
        private static float m_RotateDeg = 0.0f;

        //重力
        private const float m_Gravity = -9.8f;

        /// <summary>
        /// MouseDown位置をセットする
        /// </summary>
        private void SetMouseDownPos(PointerEventData eventData)
        {
            m_MousePos = eventData.position;
        }

#if MYDEBUG
        /// <summary>
        /// デバッグ用CanvasのUI.Textメッセージをリセットする
        /// </summary>
        private void ResetDebugTextMsg()
        {
            m_DebugText.text = "押下されていません。";
        }
        /// <summary>
        /// デバッグ用CanvasのUI.Text色を戻す
        /// </summary>
        private void ResetDebugTextColor()
        {
            m_DebugText.color = Color.green;
        }
        /// <summary>
        /// デバッグ用CanvasのUI.Text色を変更する
        /// </summary>
        private void SetDebugTextColor()
        {
            m_DebugText.color = Color.red;
        }
#endif

        /// <summary>
        /// 押下処理
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerDown(PointerEventData eventData)
        {
            m_IsPressed = true;

            myKeyDownFunc();                                      //Inputキー押下処理

            SetMouseDownPos(eventData);
#if MYDEBUG
            SetDebugTextColor();
#endif
        }
        /// <summary>
        /// 押下終了処理
        /// </summary>
        /// <param name="eventData"></param>
        public void OnPointerUp(PointerEventData eventData)
        {
            m_IsPressed = false;

            myKeyUpFunc();                                    //Inputキー離し処理
#if MYDEBUG
            ResetDebugTextMsg();
            ResetDebugTextColor();
#endif
        }


        // Use this for initialization
        void Start()
        {
#if MYDEBUG
            m_DebugText = GameObject.Find("Debug_Text").GetComponent<UnityEngine.UI.Text>();
#endif

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
#if MYDEBUG
                    m_DebugText.text = "押下中の移動";
#endif
                }
            }


#if UNITY_EDITOR
            m_KeyValue = EditorInput(ref m_KeyRotateDeg);
            
            Vector3 tmpVect = m_KeyValue + m_TouchValue;
            tmpVect.y = 0.0f;
            tmpVect = UnityEngine.Vector3.ClampMagnitude(tmpVect, 1.0f);
            tmpVect.y = m_Gravity;
            m_InputValue = tmpVect;

            m_RotateDeg = m_TouchRotateDeg + m_KeyRotateDeg;

            GetKeys();

#elif UNITY_ANDROID
            m_InputValue= m_TouchValue;
            m_RotateDeg = m_TouchRotateDeg;
#endif


            m_UnityChanController.InputKeyVect = m_InputValue;
            m_UnityChanController.InputKeyRot = m_RotateDeg;
        }


        /// <summary>
        /// UPキー相当の押下処理
        /// </summary>
        private void UpTouchDownFunc()
        {
#if MYDEBUG
            m_DebugText.text = "UPが押下。";
#endif

            m_TouchValue.z = 1.0f;
            m_TouchValue.y = m_Gravity;
        }
        /// <summary>
        /// UPキー相当の離し処理
        /// </summary>
        private void UpTouchUpFunc()
        {
            m_TouchValue.z = 0.0f;
            m_TouchValue.y = m_Gravity;
        }

        /// <summary>
        /// Downキー相当の押下処理
        /// </summary>
        private void DownTouchDownFunc()
        {
#if MYDEBUG
            m_DebugText.text = "Downが押下";
#endif
            m_TouchValue.z = -1.0f;
            m_TouchValue.y = m_Gravity;
        }
        /// <summary>
        /// Downキー相当の離し処理
        /// </summary>
        private void DownTouchUpFunc()
        {
            m_TouchValue.z = 0.0f;
            m_TouchValue.y = m_Gravity;
        }

        /// <summary>
        /// Leftキー相当の押下処理
        /// </summary>
        private void LeftTouchDownFunc()
        {
#if MYDEBUG
            m_DebugText.text = "Leftが押下";
#endif

            m_TouchRotateDeg = - 1.0f;
            m_TouchValue.y = m_Gravity;
        }
        /// <summary>
        /// Leftキー相当の離し処理
        /// </summary>
        private void LeftTouchUpFunc()
        {
            m_TouchRotateDeg = 0.0f;
            m_TouchValue.y = m_Gravity;
        }

        /// <summary>
        /// Rightキー相当の押下処理
        /// </summary>
        private void RightTouchDownFunc()
        {
#if MYDEBUG
            m_DebugText.text = "Rightが押下";
#endif

            m_TouchRotateDeg = + 1.0f;
            m_TouchValue.y = m_Gravity;
        }
        /// <summary>
        /// Rightキー相当の離し処理
        /// </summary>
        private void RightTouchUpFunc()
        {
            m_TouchRotateDeg = 0.0f;
            m_TouchValue.y = m_Gravity;
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
        /// <summary>
        /// UnityEditor上でのInput処理(移動、回転以外)
        /// キーボード入力
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
