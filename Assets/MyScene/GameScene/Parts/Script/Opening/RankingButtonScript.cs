using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MyTestClass
{
    public class DummyOutputClass
    {
        private   int m_LV = 1;
        protected int m_HP = 100;
        public    int m_MP = 20;

        protected DummyOutputClass()
        {
            Debug.Log("DummyOutputClass is Called.");
        }
    }

    sealed class DummyOutputClassParent : DummyOutputClass
    {

        public DummyOutputClassParent()
        {
            Debug.Log("DummyOutputClassParent default.");
            MyOutput();
        }

        public DummyOutputClassParent(int a, int b)
        {
            m_HP = a;
            m_MP = b;
            Debug.Log("DummyOutputClassParent with Argument.");
            MyOutput();
        }

        public void MyOutput()
        {
            Debug.Log(m_HP + " : " + m_MP);
        }
    }


}



public class RankingButtonScript : MonoBehaviour {

    [SerializeField, TooltipAttribute("RootオブジェクトUIをセットしてください")]
    private Animator m_Animator = null;



    // Use this for initialization
    void Start () {
        MyTestClass.DummyOutputClassParent cls = new MyTestClass.DummyOutputClassParent(10,9);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    /// <summary>
    /// ランキング結果を表示する
    /// </summary>
    public void Button_ShowRanking()
    {
        m_Animator.SetBool("IsHideMainMenu", true);
        m_Animator.SetBool("IsShowRanking", true);
    }
}
