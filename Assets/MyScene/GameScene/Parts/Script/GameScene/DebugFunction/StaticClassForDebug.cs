using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * あまり知られていないけど便利なUnityのAttribute(属性)
 * 強火で進め様参照
 * http://nakamura001.hatenablog.com/entry/20151202/1449029637
 * 
 * 【Unity】 UnityEditorの時のみDebug.Logを出す方法
 *  @toRisouP様参照
 *  https://qiita.com/toRisouP/items/d856d65dcc44916c487d
 * 
 * MSDN参照
 * https://docs.microsoft.com/ja-jp/dotnet/api/system.diagnostics.conditionalattribute?view=netframework-3.5
 * 
 * Unity Documentation
 * プラットフォームの #define ディレクティブ参照
 * https://docs.unity3d.com/ja/current/Manual/PlatformDependentCompilation.html
 * 
 * 
 */




namespace MyDebug
{
    /// <summary>
    /// Debugメソッド、UnityEditor上でのみ実行される
    /// </summary>
    public static class MyDebugLog
    {
        [System.Diagnostics.Conditional("UNITY_EDITOR")]                //UnityEditor上でのみ実行
        public static void Log(string str)
        {
            UnityEngine.Debug.Log(str);
        }
    }


}
