using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UnityEngine.UI.Text))]
/// <summary>
/// UI Canvas上のLifeテキストのサイズをスクリーンサイズに自動調整するクラス
/// </summary>
public class Text_Life_Initialize : MonoBehaviour {

    int m_MaxFontSize = 100;
    int m_MinFontSize = 1;

    bool m_IsBestFit = true;
    UnityEngine.UI.Text m_LifeText = null;

    float m_WidthRatio = 0.2f;
    float m_HeightRatio = 0.1f;



	// Use this for initialization
	void Start () {
        Initialize_Size();

    }

    private void Initialize_Size()
    {
        m_LifeText = gameObject.GetComponent<UnityEngine.UI.Text>();                    //Textコンポーネントを取得
        m_LifeText.resizeTextForBestFit = m_IsBestFit;                                  //テキストサイズにベストフィットする

        m_LifeText.resizeTextMaxSize = m_MaxFontSize;                                   //Maxリサイズ
        m_LifeText.resizeTextMinSize = m_MinFontSize;                                   //Minリサイズ

        RectTransform rT = m_LifeText.rectTransform;

        float sW = Screen.width;
        float sH = Screen.height;
         
        Vector2 newSize = new Vector2(sW * m_WidthRatio, sH * m_HeightRatio);

        rT.anchorMax = new Vector2(0, 1);
        rT.anchorMin = new Vector2(0, 1);

        rT.sizeDelta = newSize;



        rT.pivot = new Vector2(0, 0);

        rT.anchoredPosition = new Vector2(0, 10);


    }

	
}
