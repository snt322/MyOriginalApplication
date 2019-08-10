using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyMySelf : MonoBehaviour
{
    [SerializeField]
    private int m_ThisScore = 0;

    ScoreManager m_ScoreManager = null;

    // Use this for initialization
    void Start()
    {
        string tagScore = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Score);
        GameObject gObj = GameObject.FindGameObjectWithTag(tagScore);
        m_ScoreManager = gObj.GetComponent<ScoreManager>() as ScoreManager;
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreManager.AddScore = m_ThisScore;
        Destroy(gameObject);
    }




}
