using Momos.EditorTools;
using Momos.EditorTools.Control;
using UnityEditor;
using UnityEngine;
namespace Momos.Core.RPG
{
    public class BuffConfigSetWindow : EditorWindow
    {
        [MenuItem("Tools/BuffSystem/BuffConfigSet", priority = 150)]
        private static void ShowWindow()
            => EditorToolSet.ShowWindow<BuffConfigSetWindow>(new Rect(200,200,1000,600));

        private BuffConfigSetAsset config;
        private ScrollViewGrid<BuffConfigItem> svg;
        private ScrollViewGrid<BuffConfigItem> SVG
        {
            get
            {
                if (svg == null)
                {
                    svg = GetNewScrollViewGrid();
                }
                return svg;
            }
        }
        private ScrollViewGrid<BuffConfigItem> GetNewScrollViewGrid()
        {
            return new ScrollViewGrid<BuffConfigItem>(18, 48,
                        config.items.ToArray(),
                        new ScrollViewColumnItem<BuffConfigItem>("唯一id", 60,colStyle:E_ColumnStyle.Single,  colDraw: (rect, item) =>
                            {
                                item.id = EditorGUI.IntField(rect, item.id);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("图标", 48, colDraw: (rect, item) =>
                        {
                            item.icon = (Sprite)EditorGUI.ObjectField(rect, item.icon, typeof(Sprite), false);
                        }),
                        new ScrollViewColumnItem<BuffConfigItem>("名称", 100, 0.3f, colStyle: E_ColumnStyle.Single, colDraw: (rect, item) =>
                            {
                                item.name = EditorGUI.TextField(rect, item.name);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("第二id", 60, colStyle: E_ColumnStyle.Single, colDraw: (rect, item) =>
                            {
                                item.secID = EditorGUI.IntField(rect, item.secID);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("最大层数", 60, colStyle: E_ColumnStyle.Single, colDraw: (rect, item) =>
                            {
                                item.limited = EditorGUI.IntField(rect, item.limited);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("持续时长(ms)", 100, colStyle: E_ColumnStyle.Single, colDraw: (rect, item) =>
                            {
                                item.duration = EditorGUI.IntField(rect, item.duration);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("是否被动", 60, colDraw: (rect, item) =>
                            {
                                item.isEternal = EditorGUI.Toggle(rect, item.isEternal);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("描述", 100, 0.7f, colDraw: (rect, item) =>
                            {
                                item.description = EditorGUI.TextArea(rect, item.description);
                            }),
                        new ScrollViewColumnItem<BuffConfigItem>("操作", 100, colDraw: (rect, item) =>
                            {
                                if (GUI.Button(rect, "删除") && EditorUtility.DisplayDialog("确定删除?", "确定要删除BuffConfigItem?", "确定", "取消"))
                                {
                                    config.items.Remove(item);
                                    ResetSVGData();
                                }
                            })
                    );
        }
        private void ResetSVGData() => SVG.Ts = config.items.ToArray();

        private void SaveConfig()
        {
            if (config != null)
            {
                EditorUtility.SetDirty(config);
                AssetDatabase.SaveAssetIfDirty(config);
            }
        }

        private void OnEnable()
        {
            config = BuffConfigSetAsset.Load;
        }

        private void OnDisable()
        {
            SaveConfig();
        }

        private void OnGUI()
        {
            if (config == null)
                OnNotConfigGUI();
            else
            {
                int rh = 24;
                if (GUI.Button(new Rect(0, 0, 100, rh), "新建"))
                {
                    int i = 0;
                    if (config.items.Count > 0)
                        i = config.items[^1].id + 1;
                    config.items.Add(new BuffConfigItem() { id = i });
                    ResetSVGData();
                }
                if (GUI.Button(new Rect(position.width - 100, 0, 100, rh), "保存"))
                {
                    SaveConfig();
                }
                SVG.OnGUI(new Rect(0,rh, position.width,position.height - rh), UnityEngine.Event.current.mousePosition.y - rh);
            }
        }

        #region OnNotConfigDraw
        private void OnNotConfigGUI()
        {
            GUILayout.Label($"缺失Config在 {BuffConfigSetAsset.ResPath}");
            if (GUILayout.Button("创建") )
            {
                if (EditorToolSet.TrySaveScriptableObject<BuffConfigSetAsset>("BuffConfigSet"))
                { 
                    OnEnable();
                }
            }
        }
        #endregion OnNotConfigDraw
    }
}