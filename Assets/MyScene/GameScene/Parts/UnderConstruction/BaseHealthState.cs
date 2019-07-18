using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * キャラクタの健康状態を管理する
 * 残りHPが2割になったらm_HealthState = Tired
 * 残りHPが0になったら  m_HealthState = Down  にする
 */
public class HealthState {

    public enum enumHealthes
    {
        Health,                 //健康
        Tired,                  //被ダメージが大きい
        Down,                   //倒された
    }

    private float m_TiredLimit = 0.2f;
    private enumHealthes m_HealthState;

    public HealthState()
    {
        m_HealthState = enumHealthes.Health;
    }
    //enumHealthes.Tiredを判定する閾値tiredLimit自動で0.1～1.0範囲に丸める
    public HealthState(float tiredLimit)
    {
        float threshold = Mathf.Clamp(tiredLimit, 0.1f, 1.0f);

        m_TiredLimit = threshold;
        m_HealthState = enumHealthes.Health;
    }

    public enumHealthes State
    {
        get { return m_HealthState; }
    }

    public void SetHealth(int currentHP, int MaxHP)
    {
        float current = (float)currentHP / MaxHP;
    }
    public void SetHealth(float currentHPRatio)
    {
        if(currentHPRatio <= 0.0f) { m_HealthState = enumHealthes.Down; }
        else if (currentHPRatio <= m_TiredLimit) { m_HealthState = enumHealthes.Tired; }
        else{ m_HealthState = enumHealthes.Health; }
    }

}
