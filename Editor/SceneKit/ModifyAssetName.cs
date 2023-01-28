
#if UNITY_EDITOR

using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace CwssCommon.Editor
{
    [System.Serializable]
    public enum HandleType
    {
        Prefix,
        Suffix,
        Replace
    }

    /*
     * 修改场景中选中对象的对象名称
     * 需求，打开面板的时，选中车型资源文件，绘制数据
     * 
     * 
     */
    public class ModifAssetNameWin : OdinEditorWindow
    {
        [MenuItem("CwssCommon/批量工具/修改资源对象名")]
        private static void Open()
        {
            var EditorWin = GetWindow<ModifAssetNameWin>(title: "批量资源对象名称");
            EditorWin.position = GUIHelper.GetEditorWindowRect().AlignCenter(500, 200);
        }

        [LabelText("选择处理位置"), LabelWidth(120)]
        public HandleType handleType;

        [Space]
        [LabelText("原前缀："), LabelWidth(60)]
        [Multiline(1)]
        [SuffixLabel("如果为空，表示直接添加前缀", true)]
        [ShowIf("handleType", HandleType.Prefix)]
        public string oldStart;
        [Space]
        [LabelText("新前缀："), LabelWidth(60)]
        [Multiline(1)]
        [SuffixLabel("", true)]
        [ShowIf("handleType", HandleType.Prefix)]
        public string newStart;
        [Space]
        [LabelText("原后缀："), LabelWidth(60)]
        [Multiline(1)]
        [SuffixLabel("如果为空，表示直接添加后缀", true)]
        [ShowIf("handleType", HandleType.Suffix)]
        public string oldEnd;

        [Space]
        [LabelText("新后缀："), LabelWidth(60)]
        [Multiline(1)]
        [SuffixLabel("", true)]
        [ShowIf("handleType", HandleType.Suffix)]
        public string newEnd;

        [LabelText("更新"), LabelWidth(60)]
        [ShowInInspector]
        [ShowIf("handleType", HandleType.Prefix)]
        public void ModifySart()
        {
            Debug.Log(Selection.assetGUIDs.Length);
            if (Selection.assetGUIDs.Length == 0)
            {
                this.ShowNotification(new GUIContent { text = "没有选中的对象！" });
            }
            else
            {
                foreach (var item in Selection.assetGUIDs)
                {
                    string path = AssetDatabase.GUIDToAssetPath(item);
                    var asset = AssetDatabase.LoadMainAssetAtPath(path);
                    if (asset == null) continue;
                    if (newStart != null) newStart.Trim();

                    if (oldStart == null)
                    {
                        AssetDatabase.RenameAsset(path, newStart + asset.name);
                    }
                    else
                    {
                        if (asset.name.StartsWith(oldStart))
                        {
                            AssetDatabase.RenameAsset(path, newStart + asset.name.Remove(0, oldStart.Length));
                        }
                    }

                }
                this.ShowNotification(new GUIContent { text = "更新完成！" });
            }
        }

        [LabelText("更新"), LabelWidth(60)]
        [ShowInInspector]
        [ShowIf("handleType", HandleType.Suffix)]
        public void Modify()
        {
            Debug.Log(Selection.assetGUIDs.Length);
            if (Selection.assetGUIDs.Length == 0)
            {
                this.ShowNotification(new GUIContent { text = "没有选中的对象！" });
            }
            else
            {
                foreach (var item in Selection.assetGUIDs)
                {
                    string path = AssetDatabase.GUIDToAssetPath(item);
                    var asset = AssetDatabase.LoadMainAssetAtPath(path);
                    if (asset == null) continue;
                   
                    if (newEnd != null) newEnd.Trim();
                    if (oldEnd == null)
                    {
                        AssetDatabase.RenameAsset(path, asset.name + newEnd);
                    }
                    else
                    {
                        if (asset.name.EndsWith(oldEnd))
                        {
                            AssetDatabase.RenameAsset(path, asset.name.Remove(asset.name.Length - oldEnd.Length, oldEnd.Length) + newEnd);
                        }
                    }
                  
                }
                this.ShowNotification(new GUIContent { text = "更新完成！" });
            }
        }

    }
}

#endif