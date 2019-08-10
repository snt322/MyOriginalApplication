using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UI_Input : MonoBehaviour
{
    UnityEngine.Animator m_Animator = null;

    // Use this for initialization
    void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>() as Animator;
    }

    // Update is called once per frame
    void Update()
    {
        bool mouseButton0 = Input.GetMouseButtonUp(0);
        bool mouseButton1 = Input.GetMouseButtonUp(1);
        bool mouseButton2 = Input.GetMouseButtonUp(2);

        bool flag = mouseButton0 || mouseButton1 || mouseButton2;

        if(flag)
        {
            m_Animator.SetBool("IsHideMainMenu", false);
            m_Animator.SetBool("IsShowRanking", false);
        }
    }



}
