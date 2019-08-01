using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;



public class Magic_EachMenu_Controller : MonoBehaviour , IPointerDownHandler
{
    Attack_Damage_Controller m_AttackDamageController = null;



    [SerializeField]
    GameObject m_MagicOject = null;
    
    // Use this for initialization
    void Start () {
        string playerTag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Player);
        GameObject gObj = GameObject.FindGameObjectWithTag(playerTag);

        m_AttackDamageController = gObj.GetComponent<Attack_Damage_Controller>() as Attack_Damage_Controller;

	}


    void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
    {
        if (m_MagicOject != null)
        {
            m_AttackDamageController.UseMagic();
            StartCoroutine("UseMagicDelay", MyMagicEnum.EnumMagic.Explosion_A);
        }
        else
        {
            Heal();
        }

        Animator tmpAnimator = gameObject.GetComponentInParent<Animator>();
        bool tmpFlag = tmpAnimator.GetBool("IsExpand");
        tmpAnimator.SetBool("IsExpand", !tmpFlag);
    }

    private IEnumerator UseMagicDelay(MyMagicEnum.EnumMagic enumMagic)
    {
        yield return new WaitForSeconds(1.0f);
        UseAttackMagic(GetMagicTargetPos());
        yield return null;
        m_MagicOject.GetComponent<SphereCollider>().enabled = false;                                                                                                 //コライダーをオフにする
        yield break; ;
    }

    private void UseAttackMagic(Vector3 pos)
    {
        GameObject gObj = m_MagicOject;
        gObj.GetComponent<SphereCollider>().enabled = true;

        Vector3 magicTargetPos = pos;
        gObj.transform.position = magicTargetPos;

        ParticleSystem[] pSystems = gObj.GetComponentsInChildren<ParticleSystem>();
        for (int i = 0; i < pSystems.Length; i++)
        {
            pSystems[i].Play();
        }
    }

    private Vector3 GetMagicTargetPos()
    {
        string playerTag = System.Enum.GetName(typeof(MyEnumerator.EnumeratorTag), MyEnumerator.EnumeratorTag.Player);
        GameObject playerObj = GameObject.FindGameObjectWithTag(playerTag);


        Vector3 magicTargetPos = playerObj.transform.position;
        magicTargetPos += playerObj.transform.forward * 4.0f;
        magicTargetPos.y += 1.0f;

        return magicTargetPos;
    }


    private void Heal()
    {
        m_AttackDamageController.Heal();
    }

    /// <summary>
    /// 引数に与えたコライダーを次のフレームでenabled = falseにする
    /// </summary>
    /// <param name="collider"></param>
    /// <returns></returns>
    IEnumerator SColliderMakeDisabledCoroutine(SphereCollider collider)
    {
        yield return null;
        collider.enabled = false;                                                                                                 //コライダーをオフにする
        yield return null;
    }

}
