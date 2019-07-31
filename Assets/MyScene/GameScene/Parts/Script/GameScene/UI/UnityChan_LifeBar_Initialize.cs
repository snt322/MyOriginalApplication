using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(UnityEngine.UI.Image))]
/// <summary>
/// UI Canvas上のLifeProgressBarのサイズをスクリーンサイズに自動調整するクラス
/// </summary>
public class UnityChan_LifeBar_Initialize : MonoBehaviour {

    float m_WidthRatio = 0.3f;                                                          //スクリーンサイズに対するProgressBar幅の比率
    float m_HeightRatio = 0.05f;                                                         //スクリーンサイズに対するProgressBar高さの比率
    Vector2 m_LifeBarSize;                                                              //上記の比率を加味したスクリーン上のProgressBarのサイズ
    UnityEngine.UI.Image m_ThisImage = null;                                            //ProgressBarのImageコンポーネント

	// Use this for initialization
	void Start ()
    {
        Initialize_Size();
        Initialize_Position();
    }
	

    private void Initialize_Size()
    {
        float sW = Screen.width;
        float sH = Screen.height;
        m_LifeBarSize = new Vector2(m_WidthRatio * sW, m_HeightRatio * sH);             //ProgressBarのサイズ

        m_ThisImage = gameObject.GetComponent<UnityEngine.UI.Image>();                  //ProgressBarのImageを取得

        m_ThisImage.rectTransform.sizeDelta = m_LifeBarSize;                            //ProgressBarのImageサイズを設定
    }

    private void Initialize_Position()
    {
        RectTransform rT = m_ThisImage.rectTransform;
        rT.anchorMax = new Vector2(0, 0);                                               //アンカーMAXの正規位置
        rT.anchorMin = new Vector2(0, 0);                                               //アンカーMINの正規位置

        
        float anchorPosX = 0;                         
        float anchorPosY = 0;

        string targetTagStr = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.UnityChan_LifeProgressBar);        //自分自身のタグ(enumuratorに登録済)
        string parentTagStr = gameObject.transform.parent.gameObject.tag;                                                                           //親オブジェクトのタグ

        if(parentTagStr == targetTagStr)                                                //タグが同じ場合
        {
            anchorPosX = 0;                                                             //親オブジェクトの上に重ねて描画                           
            anchorPosY = 0;                                                             //親オブジェクトの上に重ねて描画
        }
        else
        {                                                                               //親オブジェクトのタグとこのオブジェクトのタグが異なる場合
            anchorPosX = m_WidthRatio / 10.0f * Screen.width;                           //親オブジェクトであるCanvasの左下を基準に位置を設定
            anchorPosY = m_HeightRatio / 2.0f * Screen.height;
        }

        rT.anchoredPosition = new Vector2(anchorPosX, anchorPosY);                      //アンカーに対する位置を設定
    }


}
