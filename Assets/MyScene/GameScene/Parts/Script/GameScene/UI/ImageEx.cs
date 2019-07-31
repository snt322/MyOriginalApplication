using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UnityEngine.UI
{
    /// <summary>
    /// Imageの拡張クラス
    /// </summary>
    public class ImageEx : Image
    {
        /// <summary>
        /// コンポーネント追加時に呼び出されるイベント
        /// </summary>
        new void Reset()
        {
            // デフォルトの当たりはoffにする。
            raycastTarget = false;
            // テストで赤色にする
            color = Color.red;
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// ImageExのエディタ拡張
    /// </summary>
    public class ImageExEditor
    {
        /// <summary>
        /// 右クリックメニューにGameObject/UI/ImageExを追加。
        /// </summary>
        [MenuItem("GameObject/UI/ImageEx", false)]
        private static void AddConfig()
        {
            // イメージを生成
            GameObject obj = new GameObject("NewImage");
            obj.AddComponent<ImageEx>();
            // 選択中のオブジェクトがあれば、子階層に配置
            var selectObject = Selection.activeGameObject;
            if (selectObject != null)
            {
                obj.transform.SetParent(selectObject.transform);
                obj.transform.localPosition = Vector3.zero;
            }
        }
    }
#endif
}