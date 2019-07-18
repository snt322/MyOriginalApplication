using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * PlayerおよびEnemy毎にアタッチする
 * BaseCharaInputがBaseCharaControllerにActionを渡し、BaseCharaControllerはActionに応じて
 * Animator、Charaの座標・姿勢、etcを制御する
 * BaseCharaInputにはOnTriggerEnter()を実装し、対象と接触した場合のActionもBaseCharaControllerへ渡す
 * 
 * OnTriggerEnterについて
 * ①OnTriggerEnter(Collider other)のotherの判定はother.tagにて行う。
 * ②タグEnemy・EnemyWeaponはEnemyレイヤーに設定してEnemyレイヤー同士の衝突を検知しないように[Project Settings] → [Pysic]で設定する
 * ③タグPlayer・PlayerWeaponはPlayerレイヤーに設定してPlayerレイヤー同士の衝突を検知しないように[Project Settings] → [Pysic]で設定する
 * otherはスクリプトActionState.csをアタッチし、Action内容をプロパティで受け取る
 */

//Tag一覧
public enum Tags
{
    Untagged,
    Enemy,
    EnemyWeapon,
    Player,
    PlayerWeapon
}




public class BaseCharaInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    // Enemyレイヤー同士の衝突の場合は本関数は呼び出されない
    //
    private void OnTriggerEnter(Collider other)
    {
        /*
         * string other.tagをenum Tagsに変換する
         * 参考URL:https://garafu.blogspot.com/2015/07/c-enum.html
         */

        Tags v = (Tags)System.Enum.Parse(typeof(Tags), other.tag, true);

        //衝突した相手に応じて処理を分岐
        switch(v)
        {
            case Tags.Enemy:
                { }
                break;
            case Tags.EnemyWeapon:
                {
                    BaseCharaController controller = gameObject.transform.root.GetComponent<BaseCharaController>();
                    GameObject gObj = other.transform.root.gameObject;
                    controller.SetCollisionResult(gObj, Tags.EnemyWeapon);
                }
                break;
            case Tags.Player:
                { }
                break;
            case Tags.PlayerWeapon:
                {

                }
                break;
            case Tags.Untagged:
                { }
                break;
            default:
                break;
        }

    }




}
