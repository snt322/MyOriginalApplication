using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class Button_StartMenu : MonoBehaviour
{
    [SerializeField, Tooltip("GameSceenの遷移を制御するSceenTransオブジェクトのGameController_SceenTrans.csをアタッチしてください。")]
    private GameObject m_GCSceenTrans = null;



    public void Button_ToStartMenu()
    {
        UnityEngine.EventSystems.ExecuteEvents.Execute<IExecuteSceenTrans>(m_GCSceenTrans, null, (sender, eventData) => { sender.SendExecuteSceenTrans(); });
    }
}
