using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;

/// <summary>
/// Sceenを遷移するEventMessageを受け取るインターフェース
/// </summary>
public interface IExecuteSceenTrans : IEventSystemHandler
{
    void SendExecuteSceenTrans();
}
