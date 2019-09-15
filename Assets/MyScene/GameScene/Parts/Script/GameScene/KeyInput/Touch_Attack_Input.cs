using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Attack_Input : MonoBehaviour, IPointerClickHandler
{

    delegate void ClickDelegate();
    ClickDelegate m_LBClickDelegate = null;
    ClickDelegate m_RBClickDelegate = null;
    ClickDelegate m_MBClickDelegate = null;

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        switch(eventData.button)
        {
            case UnityEngine.EventSystems.PointerEventData.InputButton.Left:
                m_LBClickDelegate();
                break;
            case UnityEngine.EventSystems.PointerEventData.InputButton.Right:
                m_RBClickDelegate();
                break;
            case UnityEngine.EventSystems.PointerEventData.InputButton.Middle:
                m_MBClickDelegate();
                break;
        }

    }

    // Use this for initialization
    void Start()
    {
        m_LBClickDelegate = new ClickDelegate(LClickFunction);
        m_RBClickDelegate = new ClickDelegate(RClickFunction);
        m_MBClickDelegate = new ClickDelegate(MClickFunction);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LClickFunction()
    {
        Debug.Log("Clicked. Left.");
    }
    void RClickFunction()
    {
        Debug.Log("Clicked. Right.");
    }
    void MClickFunction()
    {
        Debug.Log("Clicked. Middle.");
    }

}
