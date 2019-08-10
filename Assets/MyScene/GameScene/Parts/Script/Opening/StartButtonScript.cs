using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour {

    [SerializeField, TooltipAttribute("RootオブジェクトUIをセットしてください")]
    private Animator m_Animator = null;



    public void Button_LoadScene()
    {
        m_Animator.SetBool("IsHideMainMenu", true);
        StartCoroutine("MyLoadScene", 0.5f);
    }

    IEnumerator MyLoadScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);                               //アニメーション実行時間だけディレイ
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);              //シーンをロード
    }
}
