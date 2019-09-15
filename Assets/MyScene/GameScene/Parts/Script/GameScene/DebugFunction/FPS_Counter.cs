#define FPS

using System.Collections;
using System.Collections.Generic;
using UnityEngine;


#if FPS
public class FPS_Counter : MonoBehaviour
{

    private float m_CountStartTime = 0;
    private float m_Fps = 0;
    private float m_Elapse = 0;
    private uint m_DrawCount = 0;
    private float m_FpsCalInterval = 0.1f;


    private UnityEngine.UI.Text m_Text = null;
    private Vector2 m_TextSizeRatio = new Vector2(0.3f, 0.2f);                  //スクリーンサイズに対するテキストサイズ、x成分はスクリーン幅に対するテキスト幅の比率

    void Start()
    {
        m_Text = GameObject.Find("FPS_Text").GetComponent<UnityEngine.UI.Text>();

        //テキストのサイズを調整する
        m_Text.rectTransform.sizeDelta = new Vector2(Screen.width * m_TextSizeRatio.x, Screen.height * m_TextSizeRatio.y);
        m_Text.resizeTextForBestFit = true;
        m_Text.rectTransform.anchorMax = new Vector2(0, 0);
        m_Text.rectTransform.anchorMin = new Vector2(0, 0);
        m_Text.rectTransform.position = new Vector2(Screen.width / 20.0f, Screen.height / 5.0f);
    }


    // Update is called once per frame
    void Update()
    {
        MyFpsFunction();                                                //FPSを計算し、メンバ変数m_Fpsへ格納する
        OutputToText();
    }

    void OutputToText()
    {
        m_Text.text = "FPS:" + string.Format("{0:F2}", m_Fps);
    }


    /// <summary>
    /// 外部のスクリプトがFPS_Counter.csスクリプト内部のFPSを取得するメソッド
    /// </summary>
    /// <returns>FPS</returns>
    public float GetFPS()
    {
        return this.m_Fps;
    }

    /// <summary>
    /// FPSを計算し、メンバ変数m_Fpsへ格納する
    /// </summary>
    private void MyFpsFunction()
    {
        m_DrawCount++;                                                  //描画回数(Updateが呼び出された回数)

        m_Elapse = Time.time - m_CountStartTime;                        //m_DrawCount回の描画に経過した時間

        if (m_Elapse >= m_FpsCalInterval)                                           //
        {
            m_CountStartTime = Time.time;
            m_Fps = CalFPS(m_Elapse, m_DrawCount);
            m_DrawCount = 0;

//            Debug.Log(m_Fps);
        }
    }


    /// <summary>
    /// FPSを計算するメソッド
    /// </summary>
    /// <param name="elapse">描画drawCount回数に費やした時間</param>
    /// <param name="drawCount">描画回数</param>
    /// <returns></returns>
    private float CalFPS(float elapse, uint drawCount)
    {
        float tmpFps = 0;
        tmpFps = (float)drawCount / elapse;
        return tmpFps;
    }



}
#endif
