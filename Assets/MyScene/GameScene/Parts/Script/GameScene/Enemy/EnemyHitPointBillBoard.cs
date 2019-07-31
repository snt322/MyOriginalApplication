using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitPointBillBoard : MonoBehaviour {

    [SerializeField]
    Camera m_BillBoardTargetCam = null;
    [SerializeField]
    private UnityEngine.UI.Image m_HitPointProgressBar = null;


	// Use this for initialization
	void Start () {
        string mainCamTag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.MainCamera);
        m_BillBoardTargetCam = GameObject.FindGameObjectWithTag(mainCamTag).GetComponent<Camera>() as Camera;
        if(m_BillBoardTargetCam == null)
        {
            Debug.Log("There is no Camera.");
        }

        UnityEngine.UI.Image[] images = GetComponentsInChildren<UnityEngine.UI.Image>() as UnityEngine.UI.Image[];
        foreach(UnityEngine.UI.Image img in images)
        {
            if(img.tag == "EnamyHitPointTag")
            {
                m_HitPointProgressBar = img;
            }
        }


//        m_HitPointProgressBar = GameObject.FindGameObjectWithTag("EnemyHitPointTag").GetComponent<UnityEngine.UI.Image>() as UnityEngine.UI.Image;
    }
	
	// Update is called once per frame
	void Update () {
        BillBoard();

    }

    //カメラの方向を向く
    void BillBoard()
    {
        Transform camTform = m_BillBoardTargetCam.transform;

        this.transform.up = camTform.up;
        this.transform.forward = camTform.forward;

    }

    //HitPoint表示プログレスバーのfillAmountをセットする
    //セットする値は0～1に正規化される
    public float HitPointBarValue
    {
        set { SetProgressBarFillValue(value); }
    }

    //プログレスバーの体力表示を更新する
    //ratio : 0～1でHitPointフルで1
    private void SetProgressBarFillValue(float ratio)
    {
        Debug.Log("SetProgressBarFillValue call");
        if(m_HitPointProgressBar != null)
        {
            Debug.Log("SetProgressBarFillValue is not null ok." + "  ratio = " + ratio);
            m_HitPointProgressBar.fillAmount = Mathf.Clamp01(ratio);
        }
        else
        {
            Debug.Log("SetProgressBarFillValue is null error.");
        }
        
    }

}
