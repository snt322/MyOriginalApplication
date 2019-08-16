using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private int m_Score = 0;

    private UnityEngine.UI.Text m_Text = null;

    // Use this for initialization
    void Start()
    {
        m_Text = gameObject.GetComponent<UnityEngine.UI.Text>() as UnityEngine.UI.Text;
    }

    // Update is called once per frame
    void Update()
    {
        m_Text.text = "Score : " + m_Score;
    }

    public int AddScore
    {
        set{ m_Score += value; }
    }

    public int GetScore()
    {
        return m_Score;
    }
}
