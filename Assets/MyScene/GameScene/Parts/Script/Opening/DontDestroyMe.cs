using System.Collections;
using System.Collections.Generic;
using UnityEngine;

struct MyScore
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

public class DontDestroyMe : MonoBehaviour {

    [SerializeField]
    private UnityEngine.UI.Text m_Ranking1st = null;
    [SerializeField]
    private UnityEngine.UI.Text m_Ranking2nd = null;
    [SerializeField]
    private UnityEngine.UI.Text m_Ranking3rd = null;


    private void Awake()
    {
        GameObject[] gObj = GameObject.FindGameObjectsWithTag("OnlyOneObjInApp");
        if (gObj.Length == 1)
        {
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(this);
        }
    }

    /// <summary>
    /// 保存されたスコアを読み込む
    /// </summary>
    private void Start()
    {
        MyScore mScore = new MyScore();
        mScore.Score = 100;
        mScore.PlayerName = "Player1です。";
        mScore.Time = System.DateTime.Now;

        string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
        path += "\\testOutput.txt";

//        SaveRanking(path, false, JsonUtility.ToJson(mScore));
        LoadRanking(path);

    }

    private void LoadRanking(string path)
    {
        List<string> inStrs = new List<string>();


        System.IO.StreamReader sReader = new System.IO.StreamReader(path);
        string inputStr;

        while (!sReader.EndOfStream)
        {
            inputStr = sReader.ReadLine();
            inStrs.Add(inputStr);   
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
        for(int k=0; k<showRankCount; k++)
        {
            int forCount = inScores.Count - k - 1;
            for (int i=1; i<=forCount; i++)
            {
                if(inScores[i-1] > inScores[i])                              //inScores[i]とtmpMaxScoreを比較する
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


    private void SaveRanking(string path, bool append, string saveStr)
    {
        try
        {
            System.IO.StreamWriter sWriter = new System.IO.StreamWriter(path, append);

            sWriter.WriteLine(saveStr);

            sWriter.Close();
        }
        catch(System.Exception e)
        {
            string logPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop);
            System.IO.StreamWriter sWriter = new System.IO.StreamWriter(logPath, true);
            sWriter.WriteLine(e.Message);
            sWriter.Close();
        }


    }

}
