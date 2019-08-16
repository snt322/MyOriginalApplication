using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲーム中の残り時間を表示するスクリプト
/// テキストにアタッチ
/// </summary>
//[RequireComponent(typeof(UnityEngine.UI.Text))]
public class GameTimer : MonoBehaviour
{

    UnityEngine.UI.Text m_TextTime = null;

    [SerializeField, Tooltip("ゲームのプレイ時間")]
    private int m_LimitSeconds = 0;

    private System.TimeSpan m_TimeLimitSpan = System.TimeSpan.Zero; //ゲームの残り時間


    private System.TimeSpan m_TotalSpendSpan;                       //総経過時間を格納するTimeSpan
    private System.TimeSpan m_FormerSpan;                           //ポーズ実行前までの総経過時間を格納するTimeSpan
                                                                    /*
                                                                     * m_TotalSpendSpan(総経過時間) = DateTime.Now(現時刻) - m_FormerTime(カウントスタート時刻) + m_formerSpan(カウント一時停止する前迄の総経過時間)
                                                                     */


    private System.DateTime m_FormerTime;                           //カウントダウンスタート時刻、ポーズ解除をするたびに解除時刻に更新する
                                                                    //    private System.DateTime m_CurrentTime;              //現在の時刻

    private bool m_IsScoreSaved = false;

    private bool m_IsPaused = false;                                //ポーズ中フラグ

    // Use this for initialization
    void Start()
    {
        m_TextTime = gameObject.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;     //ゲームの残り時間を表示するテキストを取得
        m_FormerTime = System.DateTime.Now;                                                     //カウントスタート時刻を保持(※Start()内で実行しているので厳密なスタート時刻ではない)
        m_FormerSpan = System.TimeSpan.Zero;                                                    //一時

        m_TimeLimitSpan = new System.TimeSpan(System.TimeSpan.TicksPerSecond * m_LimitSeconds);

        m_IsScoreSaved = false;
    }

    private void FixedUpdate()
    {
        if (IsTimeUp())
        {
            if(m_IsScoreSaved == false)
            {
                SaveScore();
                m_IsScoreSaved = true;
            }

            string timeLimit = "Time Limit : " + string.Format("{0:D3} sec", 0);
            m_TextTime.text = timeLimit;
            return;
        }
        else
        {
            string timeLimit = "Time Limit : " + string.Format("{0:D3} sec", (int)m_TimeLimitSpan.TotalSeconds - (int)m_TotalSpendSpan.TotalSeconds);
            m_TextTime.text = timeLimit;
        }
    }

    /// <summary>
    /// ポーズ・ポーズ解除を行うボタン処理内で呼ぶ事で、UnityEngine.Time.timeScaleを変更する
    /// </summary>
    /// <returns>
    /// ポーズ中に本メソッドを呼ぶとtrue(ポーズ中)を返して、ポーズを解除する。
    /// ポーズ解除中に本メソッドを呼ぶとfalse(ポーズ解除中)を返して、ポーズ中にする
    /// </returns>
    public bool StartPause()
    {
        bool formerIsPause = m_IsPaused;

        if (m_IsPaused)                                              //ポーズ中の場合(m_IsPause == true)
        {
            m_FormerTime = System.DateTime.Now;
            UnityEngine.Time.timeScale = 1;
        }
        else
        {                                                               //ポーズ解除中なので、ポーズに入る処理を実行する処理
            m_FormerSpan = m_TotalSpendSpan;                            //ポーズに入る直前までの総経過時間を保持する
            UnityEngine.Time.timeScale = 0;
        }

        m_IsPaused = !m_IsPaused;                                    //ポーズ中フラグを変更する

        return formerIsPause;                                      
    }

    /// <summary>
    /// 強制的にポーズ中にする
    /// </summary>
    public void SetPause()
    {
        m_IsPaused = true;
        m_FormerSpan = m_TotalSpendSpan;
        UnityEngine.Time.timeScale = 0;
    }

    /// <summary>
    /// タイムアップしたかチェックする関数
    /// タイムアップした場合はUnityEngine.Time.timeScale = 0をセットする
    /// </summary>
    /// <returns>タイムアップしたらtrueが返る</returns>
    private bool IsTimeUp()
    {
        bool returnValue = false;

        m_TotalSpendSpan = System.DateTime.Now - m_FormerTime + m_FormerSpan;

        if(m_TimeLimitSpan <= m_TotalSpendSpan)                     //総経過時間がタイムリミットを超えた場合
        {
            UnityEngine.Time.timeScale = 0;                         //タイムスケールを0にするメソッドを外部から呼ぶ様に変更する

            returnValue = true;
        }
        return returnValue;
    }

    private void SaveScore()
    {

        string tag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Score);     //Scoreタグを取得
        GameObject obj = GameObject.FindGameObjectWithTag(tag);                                                     //Scoreタグのオブジェクトを取得
        int score = (obj.GetComponent<ScoreManager>() as ScoreManager).GetScore();                                  //スコアを取得する

        ScoreUpDate_StaticClass.SaveScore(score, System.DateTime.Now, "testPlayer");

    }

}


