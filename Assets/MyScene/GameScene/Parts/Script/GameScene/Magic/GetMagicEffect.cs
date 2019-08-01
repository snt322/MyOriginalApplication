using System.Collections;
using System.Collections.Generic;
using MyMagic;
using UnityEngine;



/// <summary>
/// MagicクラスはPlayer、および、Enemyオブジェクトにアタッチする
/// 
/// </summary>
public class GetMagicEffect : MonoBehaviour, MyMagic.IGetMagicEffect
{
    [SerializeField]
    int m_EffectValue = 0;

    [SerializeField]
    MyMagic.enumMagicEffect m_Effect;


    MagicEffects IGetMagicEffect.MagicEffect()
    {
        MagicEffects effect = new MagicEffects();

        effect.m_Effect = m_Effect;
        effect.m_EffectMag = m_EffectValue;

        return effect;
    }
}
