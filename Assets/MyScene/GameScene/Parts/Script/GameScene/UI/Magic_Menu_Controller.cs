using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Magic_Menu_Controller : MonoBehaviour , IPointerDownHandler{

    private bool isTouched = false;



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        Animator mAnimator = gameObject.GetComponentInParent<Animator>();

        bool tmpFlag = mAnimator.GetBool("IsExpand");

        if(tmpFlag)             //IsExpand == trueの場合
        {
            string aniName = System.Enum.GetName(typeof(MyAnimationStateNames.EquipmentStateName), MyAnimationStateNames.EquipmentStateName.Equipment_Canvas_Expand_Ended);
            if(mAnimator.GetCurrentAnimatorStateInfo(0).IsName(aniName))                //アニメーションが完了している場合
            {
                tmpFlag = !tmpFlag;                                                     //IsExpandにfalseをセット
            }
        }
        else
        {                       //IsExpand == falseの場合
            string aniName = System.Enum.GetName(typeof(MyAnimationStateNames.EquipmentStateName), MyAnimationStateNames.EquipmentStateName.Equipment_Canvas_Close_Ended);
            if (mAnimator.GetCurrentAnimatorStateInfo(0).IsName(aniName))                //アニメーションが完了している場合
            {
                tmpFlag = !tmpFlag;                                                     //IsExpandにfalseをセット
            }
        }
        mAnimator.SetBool("IsExpand", tmpFlag);
    }
}
