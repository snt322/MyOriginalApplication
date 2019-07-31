using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicDamage : MonoBehaviour , IMagicDamage {

    [SerializeField]
    int m_Damage;
    	
    int IMagicDamage.IMagicDamage()
    {
        return m_Damage;
    }

}
