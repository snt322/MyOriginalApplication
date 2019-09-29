using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// GameOverになった際にAnimationClip[UnityChan_GameOver]のAnimationEventから呼ばれるスクリプト
/// GameControllerのGameOverControllerにゲームオーバーになったことを通知する。
/// </summary>
public class UnityChan_GameOver : MonoBehaviour
{
    private GameObject m_GOverObj = null;

    private void Start()
    {
        string tag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.GameOverController);
        m_GOverObj = GameObject.FindGameObjectWithTag(tag);
    }

    /// <summary>
    /// 
    /// </summary>
    public void SendGameOverMsg()
    {
        Debug.Log("-----------------------------------------");
        if(m_GOverObj == null)
        {
            Debug.Log("m_GOVerObj is null.");
        }

        UnityEngine.EventSystems.ExecuteEvents.Execute<IExecuteGameOver>(m_GOverObj, null, (sender, msg) => sender.ISendExecuteGameOver());
    }

}
