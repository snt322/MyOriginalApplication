using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

public class Equipment_Menu_Controller : MonoBehaviour , IPointerEnterHandler , IPointerExitHandler {

    private bool isTouched = false;


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        UnityEngine.UI.Image img = GetComponent<UnityEngine.UI.Image>() as UnityEngine.UI.Image;
        Material material = img.material;
        material.color = Color.blue;

        Animator mAnimator = gameObject.GetComponent<Animator>();
        mAnimator.SetBool("IsExpand", true);

/*
        Vector3 pos = new Vector3();
        foreach (Transform tf in this.transform)
        {
            Debug.Log("a");
            GameObject gObj = tf.gameObject;
            RectTransform rForm = gObj.GetComponent<RectTransform>();
            switch(rForm.gameObject.name)
            {
                case "Child_Image1":
                    pos = rForm.localPosition;
                    pos.y += 50;
                    break;
                case "Child_Image2":
                    pos = rForm.localPosition;
                    pos.x += 50;
                    break;
                case "Child_Image3":
                    pos = rForm.localPosition;
                    pos.x -= 50;
                    break;
                default:
                    Debug.Log(rForm.gameObject.name);
                    break;
            }
            rForm.localPosition = pos;
        }
*/


        Debug.Log("Enter");
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        UnityEngine.UI.Image img = GetComponent<UnityEngine.UI.Image>() as UnityEngine.UI.Image;
        Material material = img.material;
        material.color = Color.white;

        Animator mAnimator = gameObject.GetComponent<Animator>();
        mAnimator.SetBool("IsExpand", false);


        Debug.Log("Exit");
    }

}
