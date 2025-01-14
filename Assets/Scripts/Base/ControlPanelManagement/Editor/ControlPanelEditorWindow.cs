using System.Collections.Generic;
using Base.AssetsManagement;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Base.ControlPanelManagement.Editor
{
    public class ControlPanelEditorWindow : OdinMenuEditorWindow
    {
        private OdinMenuTree _odinMenuTree;
        private readonly HashSet<string> _odinTreePaths = new ();
    
        [MenuItem("YufisBase/Control Panel")]
        private static void OpenWindow()
        {
            var window = GetWindow<ControlPanelEditorWindow>();
            window.titleContent = new GUIContent("Control Panel", EditorIcons.RulerRect.Active);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree() {
            _odinMenuTree = new OdinMenuTree();
            _odinTreePaths.Clear();
            AddControlPanelAssets();
            return _odinMenuTree;
        }

        private void AddControlPanelAssets()
        {
            var orderedItems = new LinkedList<IControlPanelAsset>();
            foreach (var controlPanelAsset in AssetsHelper.GetInterfacesWithScriptableObjects<IControlPanelAsset>())
            {
                AddItemToOrderedList(orderedItems, controlPanelAsset);
            }

            foreach (var controlPanelAsset in orderedItems)
            {
                var scriptableObject = controlPanelAsset as ScriptableObject;
                AddToTree(controlPanelAsset.TreeParentPath + "/" + scriptableObject.name, scriptableObject);
            }
        }

        private static void AddItemToOrderedList(LinkedList<IControlPanelAsset> orderedItems, IControlPanelAsset controlPanelAsset)
        {
            var currentOrderedItem = orderedItems.First;
            for (int i = 0; i < orderedItems.Count; i++)
            {
                if (controlPanelAsset.ControlPanelPriority > currentOrderedItem.Value.ControlPanelPriority)
                {
                    orderedItems.AddBefore(currentOrderedItem, controlPanelAsset);
                    return;
                }
                currentOrderedItem = currentOrderedItem.Next;
            }
            orderedItems.AddLast(controlPanelAsset);
        }

        private void AddToTree(string name, Object item)
        {
            if (_odinTreePaths.Contains(name))
            {
                AddToTree(name + " - ", item);
                return;
            }
            _odinMenuTree.Add(name, item);
            _odinTreePaths.Add(name);
        }
    }
}
