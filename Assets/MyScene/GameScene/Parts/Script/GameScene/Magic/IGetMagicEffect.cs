using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyMagic
{
    public enum enumMagicEffect
    {
        DAMAGE,
        HEAL,
    }

    public struct MagicEffects
    {
        public enumMagicEffect m_Effect;
        public int m_EffectMag;
    }



    /// <summary>
    /// このインターフェースを通して魔法の効果を取得する。
    /// </summary>
    public interface IGetMagicEffect
    {
        MagicEffects MagicEffect();
    }

}