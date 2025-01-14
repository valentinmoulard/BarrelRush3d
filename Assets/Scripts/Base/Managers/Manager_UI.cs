using Base.Helpers;
using Base.Ui;
using Base.Ui.Screens;
using Sirenix.OdinInspector;
using UnityEngine;

#if true
using UnityEditor;
#endif

namespace Base.Managers
{
    public class Manager_UI : ManagerBase
    {
        [SerializeField]
        private UnitySerializedDictionary<UIScreenType, UIScreen> uiScreens;
        private UIScreen _activeScreen;

        private void Awake()
        {
            ChangeScreen(UIScreenType.Start);
        }

        public void ChangeScreen(UIScreenType type)
        {
            if (CheckIfSameScreen(type)) return;
            DisableScreen();
            EnableScreen(type);
        }

        private bool CheckIfSameScreen(UIScreenType type)
        {
            return _activeScreen && _activeScreen.UiScreenType == type;
        }

        private void DisableScreen()
        {
            _activeScreen?.Hide();
        }

        private void EnableScreen(UIScreenType type)
        {
            uiScreens.TryGetValue(type, out var screen);
            _activeScreen = screen;
            _activeScreen?.Show();
        }

#if UNITY_EDITOR
        
        [Button]
        private void GetScreens()
        {
            uiScreens = new UnitySerializedDictionary<UIScreenType, UIScreen>();
            var screens = GetComponentsInChildren<UIScreen>(true);
            foreach (var screen in screens)
            {
                uiScreens.Add(screen.UiScreenType, screen);
            }
            EditorUtility.SetDirty(this);
            AssetDatabase.SaveAssets();
        }
#endif
    }
}