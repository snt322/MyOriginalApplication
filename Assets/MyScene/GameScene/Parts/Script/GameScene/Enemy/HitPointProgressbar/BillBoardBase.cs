using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アタッチしたObjectを指定したカメラ方向へ向けます。
/// このscriptはCanvasにアタッチ下ください。
/// </summary>
public class BillBoardBase : MonoBehaviour
{
    [SerializeField, Tooltip("ビルボードが向く方向のカメラ、Inspector上で初期値がnullの場合はMainCameraを自動でセットします。")]
    private Camera m_BillBoardTargetCam = null;


    /// <summary>
    /// Use this for initialization 
    /// </summary>
    void Start()
    {
        if (m_BillBoardTargetCam == null)
        {
            string cameraTag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.PlayerCamera);
            m_BillBoardTargetCam = GameObject.FindGameObjectWithTag(cameraTag).GetComponent<Camera>() as Camera;
            if(m_BillBoardTargetCam == null)
            {
                MyDebug.MyDebugLog.Log("メインカメラが見つかりませんでした。");
            }
        }
    }

    /// <summary>
    /// Update is called once per frame 
    /// </summary>
    void Update()
    {
        BillBoard();
    }

    /// <summary>
    /// カメラの方向に向きを変えるメソッド、Update()内で呼び出される。
    /// </summary>
    void BillBoard()
    {
        Transform camTform = m_BillBoardTargetCam.transform;

        this.transform.up = camTform.up;
        this.transform.forward = camTform.forward;

    }
}
