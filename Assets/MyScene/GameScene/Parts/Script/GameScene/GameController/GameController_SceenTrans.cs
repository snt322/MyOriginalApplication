using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class GameController_SceenTrans : MonoBehaviour, IExecuteSceenTrans
{
    private bool m_IsSceenTrans = false;


    void IExecuteSceenTrans.SendExecuteSceenTrans()
    {
        m_IsSceenTrans = true;
    }

    /// <summary>
    /// 他のスクリプトのUpdate()内でUnityEngine.Time.timeScaleを変更するので
    /// シーン遷移をする場合LateUpdate()でtimeScaleをもとに戻す必要がある。
    /// Update()内では実行順次第で元に戻らない可能性がある
    /// </summary>
    void LateUpdate()
    {
        if(m_IsSceenTrans)
        {
            UnityEngine.Time.timeScale = 1.0f;      //timeScaleを元に戻す

            SceneManager.LoadScene("OpeningScene", LoadSceneMode.Single);   //SceenをLoadする
        }
    }

}
