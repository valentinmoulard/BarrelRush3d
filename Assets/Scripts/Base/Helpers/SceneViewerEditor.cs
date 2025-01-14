using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Overlays;
using UnityEditor.SceneManagement;
#endif

#if UNITY_EDITOR
namespace Base.Helpers
{
    [Overlay(typeof(SceneView), id: IdSceneViewerOverlay, displayName: "Scene Viewer")]
    [Icon("Assets/Sprites/Icons/unity_scene.png")] 
    class SceneViewerEditor : Overlay
    {
        private const string IdSceneViewerOverlay = "sceneViewerOverlay";
        private VisualElement root;
        private int indexLoader = -1;
        private static bool s_loadLoader;
        
        public override VisualElement CreatePanelContent()
        {
            root = new VisualElement
            {
                style =
                {
                    width = new StyleLength(new Length(120, LengthUnit.Pixel)),
                    backgroundColor = new StyleColor(Color.black),
                    opacity = new StyleFloat(0.85f),
                    fontSize = 14
                }
            };

            CreateSceneButtons();

            return root;
        }
        
        public override void OnCreated()
        {
            EditorBuildSettings.sceneListChanged += CreateSceneButtons;
        }

        public override void OnWillBeDestroyed()
        {
            base.OnWillBeDestroyed();
            EditorBuildSettings.sceneListChanged -= CreateSceneButtons;
        }

        private void CreateSceneButtons()
        {
            root.Clear();

            if (SceneManager.sceneCountInBuildSettings == 0)
            {
                var warningText = new TextElement();
                warningText.text = "No Scenes in Build Settings";
                warningText.style.fontSize = 12;
                warningText.style.color = new StyleColor(Color.red);

                root.Add(warningText);
                return;
            }
            

            for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
            {
                int tempIndex = i;
            
                string fileName = Path.GetFileName(SceneUtility.GetScenePathByBuildIndex(tempIndex));

                if (fileName.Contains("Loader"))
                {
                    indexLoader = tempIndex;
                }

                if ((!fileName.Contains("Level") || (fileName.Contains("Load"))) && fileName.Substring(0, fileName.Length - 6) != "Loader")
                {
                    continue;
                }
            
                var sceneButton = new Button(() => ButtonCallback(tempIndex));
            
                //Removes the extension part of the file name (e.g: "MainScene.unity" -> "MainScene")
                sceneButton.text = fileName.Substring(0, fileName.Length - 6);

                root.Add(sceneButton);
            }
        }

        private void ButtonCallback(int index)
        {
            if (SceneManager.GetActiveScene().isDirty)
            {
                int dialogResult = EditorUtility.DisplayDialogComplex(
                    "Scene has been modified",
                    "Do you want to save the changes you made in the current scene?",
                    "Save", "Don't Save", "Cancel");

                switch (dialogResult)
                {
                    case 0: //Save and open the new scene
                        EditorSceneManager.SaveScene(SceneManager.GetActiveScene());
                        EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(index));
                        break;
                    case 1: //Open the new scene without saving current.
                        EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(index));
                        break;
                    case 2: //Cancel process (Basically do nothing for now.)
                        break;
                    default:
                        Debug.LogWarning("Something went wrong when switching scenes.");
                        break;
                }
            }
            else
            {
                if(s_loadLoader) 
                    EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(indexLoader));
                
                EditorSceneManager.OpenScene(SceneUtility.GetScenePathByBuildIndex(index), s_loadLoader ? OpenSceneMode.Additive : OpenSceneMode.Single);
            }
        }

    }
}
#endif
