using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Touch_Attack_Input : MonoBehaviour, IPointerClickHandler
{
    [SerializeField, Tooltip("Player(UnityChan)のアクション（攻撃など）を制御するスクリプトをアタッチしてください")]
    private Attack_Damage_Controller m_Attack_Damage_Controller = null;


    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        m_Attack_Damage_Controller.Attack();
    }
}
