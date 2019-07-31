using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public interface IDamage
{

    //ダメージを受け渡す
    int Damage(MyClasses.enumAttackMeans means);

    //攻撃を受け渡す
//    int Attack(int attack);
}


