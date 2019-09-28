using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPointProgressBar : MonoBehaviour
{
    [SerializeField, Tooltip(TooltipMsg)]
    private UnityEngine.UI.Image m_HitPointProgressBar = null;

    private const string TooltipMsg = "このscriptをアタッチするCanvasの子オブジェクトに\nEnemyのHitPointProgressBarを表すUI.Imageを配置して、そのImageをセットしてください。\nInspector上で初期がnull残り時間場合は子オブジェクト内を探索します。";


    // Use this for initialization
    void Start()
    {
        if(m_HitPointProgressBar == null)
        {
            //子オブジェクト内のImageでtagが"EnemyHitPointTag"の物を探索する
            UnityEngine.UI.Image[] images = GetComponentsInChildren<UnityEngine.UI.Image>() as UnityEngine.UI.Image[];
            for(int i=0; i<images.Length; i++)
            {
                if(images[i].tag == "EnemyHitPointTag")
                {
                    m_HitPointProgressBar = images[i];
                }
            }
        }
    }

    /// <summary>
    /// HitPoint表示プログレスバーのfillAmountをセットする
    /// セットする値は0～1に丸められる
    /// </summary>
    public float HitPointBarValue
    {
        set { SetProgressBarFillValue(value); }
    }

    /// <summary>
    /// プログレスバーの体力表示を更新する
    /// </summary>
    /// <param name="ratio">ratio : 範囲0～1、HitPointフルが1、範囲外の場合は0又は1に丸める</param>
    private void SetProgressBarFillValue(float ratio)
    {
        if (m_HitPointProgressBar != null)
        {
            m_HitPointProgressBar.fillAmount = Mathf.Clamp01(ratio);
        }
        else
        {
            Debug.Log("SetProgressBarFillValue is null error.");
        }
    }
}
