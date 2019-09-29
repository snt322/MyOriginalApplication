using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// UI上のボタンサイズを画面サイズに合わせて自動変更、自動配置する。
/// </summary>
public class Button_InitializeSize : MonoBehaviour
{

    private Vector2 m_SizeRatioToScreen = new Vector2(0.2f, 0.1f);


    // Use this for initialization
    void Start()
    {
        RectTransform tmpRect = this.gameObject.GetComponent<RectTransform>();

        float sW = Screen.width;
        float sH = Screen.height;

        float buttonWidth = sW * m_SizeRatioToScreen.x;
        float buttonHeight = sH * m_SizeRatioToScreen.y;

        Debug.Log(sW + " : " + sH);

        float bAnchorX1 = sW * 0.5f;
        float bAnchorY1 = -20.0f;
        float bAnchorX2 = sW * 0.5f + 10f + buttonWidth;
        float bAnchorY2 = -20.0f;

        string targetTag;

        targetTag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Button_Start_Pause);
        if (gameObject.tag == targetTag)                                                                                //メニューボタンの場合
        {
            tmpRect.anchorMax = new Vector2(0, 1);
            tmpRect.anchorMin = new Vector2(0, 1);
            tmpRect.pivot = new Vector2(0, 1);

            tmpRect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
            tmpRect.anchoredPosition = new Vector2(bAnchorX1, bAnchorY1);

            Transform tmpTForm = gameObject.transform.GetChild(0);
            UnityEngine.UI.Text tmpText = tmpTForm.gameObject.GetComponent<UnityEngine.UI.Text>();
            tmpText.rectTransform.sizeDelta = new Vector2(tmpRect.sizeDelta.x, tmpRect.sizeDelta.y);
            tmpText.rectTransform.anchoredPosition = new Vector2(0, 0);
            tmpText.rectTransform.anchorMax = new Vector2(0, 0);
            tmpText.rectTransform.anchorMin = new Vector2(0, 0);
            tmpText.rectTransform.pivot = new Vector2(0, 0);
            
            tmpText.resizeTextForBestFit = true;
        }

        targetTag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Button_Pause_Save);
        if(gameObject.tag == targetTag)                                                                                 //Pause、Saveボタンの場合
        {
            tmpRect.anchorMax = new Vector2(0, 1);
            tmpRect.anchorMin = new Vector2(0, 1);
            tmpRect.pivot = new Vector2(0, 1);

            tmpRect.sizeDelta = new Vector2(buttonWidth, buttonHeight);
            tmpRect.anchoredPosition = new Vector2(bAnchorX2, bAnchorY2);

            Transform tmpTForm = gameObject.transform.GetChild(0);
            UnityEngine.UI.Text tmpText = tmpTForm.gameObject.GetComponent<UnityEngine.UI.Text>();
            tmpText.rectTransform.sizeDelta = new Vector2(tmpRect.sizeDelta.x - 20, tmpRect.sizeDelta.y);
            tmpText.rectTransform.anchoredPosition = new Vector2(10, 0);
            tmpText.rectTransform.anchorMax = new Vector2(0, 0);
            tmpText.rectTransform.anchorMin = new Vector2(0, 0);
            tmpText.rectTransform.pivot = new Vector2(0, 0);
            
            tmpText.resizeTextForBestFit = true;
        }

    }

}
