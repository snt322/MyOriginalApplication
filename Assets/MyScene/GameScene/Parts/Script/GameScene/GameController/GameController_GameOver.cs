using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ①PlayerのLifeが0になるとAnimator[UnityChan_Animator]のAnimation[UnityChan_GameOver]が再生される。
/// ②UnityChan_GameOverアニメーション内でAnimationEventが実行される。
/// ③UnityChan_GameOver.csのSendGameOverMsg()メソッドが実行される。
/// ④SendGameOverMsg()内でEventSystems.ExecuteEvents.Execute<IExecuteGameOver>でGameObject[GameOverController]にイベントメッセージを送る
/// ⑤GameObject[GameOverController]がゲームオーバー処理を行う。
/// </summary>
public class GameController_GameOver : MonoBehaviour, IExecuteGameOver
{
    public void GameOver()
    {
        Debug.Log("Game Over!!!!");
    }

    void IExecuteGameOver.ISendExecuteGameOver()
    {
        GameOver();
    }
}
