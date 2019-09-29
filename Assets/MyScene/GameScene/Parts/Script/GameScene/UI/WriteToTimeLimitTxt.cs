using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WriteToTimeLimitTxt : MonoBehaviour
{
    [SerializeField, Tooltip("残りプレイ時間を表示するテクスチャをセットしてください。")]
    private UnityEngine.UI.Text m_TimeLimitTxt = null;

    [SerializeField, Tooltip("プレイ時間を管理するGameController_TimeLimitt.csスクリプトをアタッチしてください。")]
    private GameController_TimeLimit m_TimeController = null;


    private const string m_BaseText = "Time Limit : ";

    // Use this for initialization
    void Start()
    {
        m_TimeLimitTxt.text = m_BaseText;
    }

    // Update is called once per frame
    void Update()
    {
        m_TimeLimitTxt.text = m_BaseText + string.Format("{0:F5}", m_TimeController.GetRemainingTime());
    }
}
