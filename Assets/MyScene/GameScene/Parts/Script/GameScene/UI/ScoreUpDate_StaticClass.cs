using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreUpDate_StaticClass : MonoBehaviour
{
    public static void SaveScore(int sc, System.DateTime tm, string playeName)
    {
        string tag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.OnlyOneObjInApp);
        GameObject gObj = GameObject.FindGameObjectWithTag(tag);

        RankingDataController script = gObj.GetComponent<RankingDataController>() as RankingDataController;

        MyScoreStruct.MyScore score = new MyScoreStruct.MyScore();
        score.Time = tm;
        score.Score = sc;
        score.PlayerName = playeName;

        Debug.Log("Saved");

        script.SaveScore(score);
    }
}
