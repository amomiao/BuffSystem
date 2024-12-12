using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;

namespace Momos.EditorTools
{
    public static class EditorToolSet
    {
        /// <summary> 加载一个窗口 </summary>
        /// <typeparam name="T"> 窗口类 </typeparam>
        /// <param name="position"> 初始Rect </param>
        /// <param name="title"> 标题 </param>
        /// <param name="evt"> 调用<see cref="EditorWindow.Show"/>前做 </param>
        public static T ShowWindow<T>(Rect position, string title = null, UnityAction<T> evt = null) where T : EditorWindow
        { 
            if(string.IsNullOrEmpty(title))
                title = typeof(T).Name;
            T win = EditorWindow.GetWindow<T>();
            win.titleContent = new GUIContent(title);
            win.position = position;
            evt?.Invoke(win);
            win.Show();
            return win;
        }

        /// <summary> 尝试 选择路径并创建 一个可编程物体 </summary>
        public static bool TrySaveScriptableObject<T>(string defaultName) where T : ScriptableObject
        {
            if (SavePathScriptableObjectInProject<T>(defaultName, out string path))
            {
                CreateScriptableObject<T>(path);
                return true;
            }
            return false;
        }

        /// <summary> 选择保存路径 给一个可编程物体 </summary>
        /// <returns> true:点击'确定'给出了一个正确的路径 </returns>
        public static bool SavePathScriptableObjectInProject<T>(string defaultName, out string path) where T : ScriptableObject
        { 
            path = EditorUtility.SaveFilePanelInProject(
                    $"创建{typeof(T).Name}",
                    $"{defaultName}",
                    "asset",
                    "");
            return !string.IsNullOrEmpty(path);
        }

        /// <summary> 给予路径去创建 一个可编程物体 </summary>
        public static T CreateScriptableObject<T>(string saveAssetsPath) where T : ScriptableObject
        { 
            T asset = ScriptableObject.CreateInstance<T>();
            AssetDatabase.CreateAsset(asset, saveAssetsPath);
            EditorUtility.SetDirty(asset);
            AssetDatabase.SaveAssetIfDirty(asset);
            AssetDatabase.Refresh();
            return asset;
        }
    }
}
