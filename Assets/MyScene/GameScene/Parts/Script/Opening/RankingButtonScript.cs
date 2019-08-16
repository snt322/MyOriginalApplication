using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public class RankingButtonScript : MonoBehaviour {

    [SerializeField, TooltipAttribute("RootオブジェクトUIをセットしてください")]
    private Animator m_Animator = null;

    [SerializeField, Tooltip("RankingDataControllerをセットしてください")]
    private RankingDataController m_RankingData = null;


    /// <summary>
    /// ランキング結果を表示する
    /// </summary>
    public void Button_ShowRanking()
    {
        m_RankingData.InitializeRanking();

        m_Animator.SetBool("IsHideMainMenu", true);
        m_Animator.SetBool("IsShowRanking", true);
    }
}
