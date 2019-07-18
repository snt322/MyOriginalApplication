using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputToController
{

    InputToController() { }
}



public struct Order
{
    public enumActions m_Action;
    public Vector3 m_Direction;
    public GameObject m_TargetObj;
    public Tags m_Tag;
}

public class BaseCharaController : MonoBehaviour {

    private System.Collections.Generic.Queue<Order> m_Queue = new Queue<Order>();

    BaseActionState m_ActionState;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //衝突した相手をBaseCharaInputスクリプトから取得する
    public void SetCollisionResult(GameObject gObj, Tags tag)
    {
        Order order = new Order();
        order.m_Action = enumActions.HIT;
        order.m_TargetObj = gObj;
        order.m_Tag = tag;

        m_Queue.Enqueue(order);
    }


}
