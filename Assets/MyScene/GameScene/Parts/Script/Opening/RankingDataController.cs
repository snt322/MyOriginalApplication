using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyScoreStruct;


namespace MyScoreStruct
{
    public struct MyScore
    {
        [SerializeField]
        private int m_Score;
        [SerializeField]
        private string m_PlayerName;
        [SerializeField]
        private string m_PlayTime;

        public int Score
        {
            set
            {
                m_Score = value;
            }
        }
        public string PlayerName
        {
            set
            {
                m_PlayerName = value;
            }
        }
        public System.DateTime Time
        {
            set
            {
                m_PlayTime = string.Format("{0}", value);
            }
        }

    }
}

public class RankingDataController : MonoBehaviour
{

    [SerializeField]
    private UnityEngine.UI.Text m_Ranking1st = null;
    [SerializeField]
    private UnityEngine.UI.Text m_Ranking2nd = null;
    [SerializeField]
    private UnityEngine.UI.Text m_Ranking3rd = null;

    private GameObject m_DebugText = null;


    /// <summary>
    /// 保存されたスコアを読み込む
    /// </summary>
    private void Start()
    {
        string path = GetSDcardDirectory() + "\\score_save.txt";
        GetDebugText(false).text = path;

        InitializeRanking();
    }

    public void InitializeRanking()
    {
        string path = GetSDcardDirectory() + "\\score_save.txt";
        LoadRanking(path);
    }


    /// <summary>
    /// ランキングのテキストファイルからランキングを読み込んで
    /// 上位3位をUI.Text m_Ranking1st、m_Ranking2nd、m_Ranking3rdにセットする
    /// </summary>
    /// <param name="path"></param>
    public void LoadRanking(string path)
    {
        List<string> inStrs = new List<string>();

        System.IO.StreamReader sReader = null;

        try
        {
            sReader = new System.IO.StreamReader(path);
            string inputStr;

            while (!sReader.EndOfStream)
            {
                inputStr = sReader.ReadLine();
                inStrs.Add(inputStr);
            }
        }
        catch (System.Exception e)                               //その他の例外
        {
            //ランキングの上位3つを"スコアがありません"と出力する
            string scoreStr = "スコアがありません";
            m_Ranking1st.text = scoreStr;
            m_Ranking2nd.text = scoreStr;
            m_Ranking3rd.text = scoreStr;
            return;                                             //強制的に処理を抜ける
        }

        //読み込んだランキングのうち上位3つのみを取得する
        List<int> inScores = new List<int>();

        string[] separatorStrs = new string[] { "{\"m_Score\":", ",", "\"m_PlayerName\":", "\"}", "\"m_PlayTime\":\"", "\"" };

        List<int> order = new List<int>();                              //このスコープ内の後半で行うバブルソートに使用するリスト

        int tmpCounter = 0;
        foreach (var v in inStrs)
        {
            string[] strs = v.Split(separatorStrs, System.StringSplitOptions.RemoveEmptyEntries);

            //読み込んだ文字列からスコアを抽出してList<int>に格納する
            if (strs.Length != 0)                                       //要素数が0で無ければ
            {
                string tmpScoreStr = strs[0];                           //strs[0]にスコアが有るので抜き出す
                int tmpScore = 0;
                int.TryParse(tmpScoreStr, out tmpScore);                //int型であるかチェックする

                inScores.Add(tmpScore);                                 //int型の場合はList<int>に格納する
                order.Add(tmpCounter++);

            }
        }

        //バブルソート 上位3つを抽出
        int[] scoreTop3 = new int[3];                                   //スコアの上位3つを1位から順にscoreTop3[0]、[1]、[2]に格納する
        int[] scoreTop3Num = new int[3];                                //inScoresに格納されたスコアの上位3つの要素番号を1位から順にscoreTop3Num[0]、[1]、[2]に格納する

        int showRankCount = (inScores.Count >= 3) ? 3 : inScores.Count;     //保存されたスコア数が3つ以上の場合showRankCount=3、3つ未満の場合はshowRankCountinScores.Count
        for (int k = 0; k < showRankCount; k++)
        {
            int forCount = inScores.Count - k - 1;
            for (int i = 1; i <= forCount; i++)
            {
                if (inScores[i - 1] > inScores[i])                              //inScores[i]とtmpMaxScoreを比較する
                {
                    int t = inScores[i];
                    inScores[i] = inScores[i - 1];
                    inScores[i - 1] = t;

                    t = order[i];
                    order[i] = order[i - 1];
                    order[i - 1] = t;
                }
            }
            scoreTop3[k] = inScores[forCount];
            scoreTop3Num[k] = order[forCount];
        }




        //ランキングの上位3つをUnityEngine.UI.Textに出力する
        int counter = 0;

        for (int k = 0; k < 3; k++)
        {
            string scoreStr = "スコアがありません";

            if (k < inStrs.Count)                                           //保存されているランキング数が3つ以上ある場合
            {
                string[] strs = inStrs[scoreTop3Num[k]].Split(separatorStrs, System.StringSplitOptions.RemoveEmptyEntries);
                scoreStr = "<i>スコア</i> : " + "<color=#00ff00ff>" + strs[0] + "</color>" + "\n" + "プレイヤ : " + strs[1] + " " + strs[2];
            }


            switch (counter++)
            {
                case 0:
                    m_Ranking1st.text = scoreStr;
                    break;
                case 1:
                    m_Ranking2nd.text = scoreStr;
                    break;
                case 2:
                    m_Ranking3rd.text = scoreStr;
                    break;
            }
        }




    }

    /// <summary>
    /// スコアを保存する為に外部に公開するメソッド
    /// 内部でprivate void SaveRanking(string, bool, string)を実行する
    /// </summary>
    public void SaveScore(MyScoreStruct.MyScore score)
    {
        string path = GetSDcardDirectory() + "\\score_save.txt";
        SaveRanking(path, true, JsonUtility.ToJson(score));
    }


    /// <summary>
    /// ランキングを保存する
    /// </summary>
    /// <param name="path">保存パス</param>
    /// <param name="append">上書き:false</param>
    /// <param name="saveStr">保存する文字列</param>
    private void SaveRanking(string path, bool append, string saveStr)
    {
        try
        {
            System.IO.StreamWriter sWriter = new System.IO.StreamWriter(path, append);

            sWriter.WriteLine(saveStr);

            sWriter.Close();
        }
        catch (System.Exception e)
        {
            string logPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            System.IO.StreamWriter sWriter = new System.IO.StreamWriter(logPath, true);
            sWriter.WriteLine(e.Message);
            sWriter.Close();
        }


    }

    /// <summary>
    /// 作成中、外部ストレージ(SDカード)へのパスを返す予定。
    /// 現在はApplication.persistentDataPathを返す
    /// </summary>
    /// <returns></returns>
    private string GetSDcardDirectory()
    {
        string str;

        str = Application.persistentDataPath;

        return str;
    }

    /// <summary>
    /// デバッグテキストを取得する
    /// </summary>
    /// <param name="isShow">テキストenableをセット</param>
    /// <returns>デバッグテキストのインスタンス</returns>
    private UnityEngine.UI.Text GetDebugText(bool isShow)
    {
        UnityEngine.UI.Text text = null;

        string tag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Debug_Text);
        GameObject obj = GameObject.FindGameObjectWithTag(tag);

        text = obj.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
        text.enabled = isShow;

        return text;
    }
}
