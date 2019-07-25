using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace MyClasses
{

    //キャラクタの状態
    public enum enumHealthState
    {
        NORMAL = 1,
        ALMOST_DYING = 0,
        DEAD = 2,
        GET_DAMAGE,
    }

    //本クラスが攻撃を受けた部位
    enum enumDamagedPart
    {
        //処理が難しいので体のどこをダメージを受けても同じとする。
        NORMAL = 0,
    }

    //本クラスが攻撃を相手に与えた際の方法、本クラスの武器または本クラスの部位
    //要素の値は必ず0から順にすること
    public enum enumAttackMeans
    {
        WEAPON = 0,
        BODY = 1,
    }

    //キャラクタが実行中の行動の種類
    public enum enumAction
    {
        NONE,                                   //行動なし
        ATTACK,                                 //攻撃
        DEFENCE,                                //防御
        RECOVER,                                //回復
    }



    class BaseCharacter
    {
        private int[] m_Attacks;
        private int m_Life = 200;
        private int m_MaxLife = 200;
        private float m_AlmostDyingRatio = 0.3f;
        private int m_Defence = 100;

        private enumAction m_Action = enumAction.NONE;

        private enumHealthState m_State = enumHealthState.NORMAL;
        private float[] m_DamageRatio = { 0.75f, 1.25f };

        public BaseCharacter() {
            //本クラスの部位による攻撃力を初期化
            int len = System.Enum.GetValues(typeof(enumAttackMeans)).Length;     //enumAttackMeansの数だけ攻撃力の数を指定
            m_Attacks = new int[len];
            m_Attacks[0] = 110;                                                  //武器による攻撃力を設定
            m_Attacks[1] = 10;                                                   //体による攻撃力を設定
        }                                                                        //初期値のままのコンストラクタ
        public BaseCharacter(int life, int defence, enumHealthState state, float damageRatioMin = 0.75f, float damageRatioMax = 1.25f)
        {
            int len = System.Enum.GetValues(typeof(enumAttackMeans)).Length;    //enumAttakMeansの数だけ攻撃力の数を指定
            m_Attacks = new int[len];
            m_Attacks[0] = 110;                                                  //武器による攻撃力を設定
            m_Attacks[1] = 10;                                                   //体による攻撃力を設定

            m_MaxLife = life;
            m_Life = life;

            m_Defence = defence;

            m_State = state;
        }

        //被ダメージ計算
        public int DamagedPoint(int givenDamage)
        {
            int tmpDamage = 0;
            int actualDamage = 0;

            tmpDamage = givenDamage - this.m_Defence;
            if(tmpDamage < 0) { tmpDamage = 0; }

            tmpDamage = (int)(tmpDamage * UnityEngine.Random.Range(m_DamageRatio[0], m_DamageRatio[1]));

            actualDamage = (m_Life < tmpDamage) ? m_Life : tmpDamage;   //ほかの人が見たら意味が分からない処理かも?m_Life==0になるまでに実際に受けたダメージ
            m_Life -= actualDamage;                                     //ほかの人が見たら意味が分からない処理化も?

            SetState_in_DamagePoint();                                  //本キャラクタが受けたダメージに応じてm_Condition.m_State(enumState)の状態を変更する
                                                                        //m_Condition.m_Stateの値に応じてUnityChanAnimatorController内でアニメーションを分岐させる。

            return actualDamage;                                        //実際に受けたダメージを返す。
                                                                        //ダメージによりライフm_Life<0となる場合は、実際に受けたダメージを返す。
                                                                        //例) m_Life = 10、被ダメージgivenDamage = 24の場合
                                                                        // 戻り値 = actualDamage = 24 - 10 = 14(実際に受けたダメージ)

        }


        //予ダメージ計算
        public int AttackPoint(enumAttackMeans means)
        {
            int attack = m_Attacks[(int)means];

            return attack;
        }

        //被ダメージ計算の処理内で使用
        //ダメージ値、残りm_Lifeの値に応じてm_Stateを変更する
        private void SetState_in_DamagePoint()
        {
            if (m_Life <= 0)
            {
                m_Life = 0;                                             //ライフが0より小さくなる場合はm_Life = 0にする

                m_State = enumHealthState.DEAD;                               //倒された場合
            }
            else if (((float)m_Life / m_MaxLife) < m_AlmostDyingRatio)
            {
                m_State = enumHealthState.ALMOST_DYING;                       //瀕死の場合、※アニメーションは未実装で無視される
            }
            else
            {
                m_State = enumHealthState.GET_DAMAGE;                         //ダメージを受けた状態へ         
                                                                        //ダメージを受けた場合のモーションへ
            }
        }



        //体力取得のプロパティ
        public int Life { get { return m_Life; } }

        //最大ヒットポイント取得のプロパティ
        public int MaxLife { get { return m_MaxLife; } }

        //キャラクタの状態
        public enumHealthState HealthState
        {
            get { return m_State; }
            set { m_State = value; }
        }
        //キャラクタの行動
        public enumAction Action
        {
            get { return m_Action; }
            set { m_Action = value; }
        }

        //キャラクタを蘇生させる
        public int Resurrection
        {
            set {
                if (value > m_MaxLife) { this.m_Life = m_MaxLife; }
                else { this.m_Life = value; }

                m_Action = enumAction.RECOVER;
            }
        }


    }

}