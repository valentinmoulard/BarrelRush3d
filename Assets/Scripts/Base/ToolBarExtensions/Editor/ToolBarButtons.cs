using System;
using Base.GameManagement;
using Base.Managers;
using Base.SaveSystem.SaveableScriptableObject.Scripts;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityToolbarExtender;

namespace Base.ToolBarExtensions.Editor
{
    [InitializeOnLoad]
    public class ToolBarButtons 
    {
        private const string levelSceneMainPath = "Assets/Scenes/";
    
        static ToolBarButtons()
        {
            ToolbarExtender.LeftToolbarGUI.Add(OnToolbarGUILeft);
            ToolbarExtender.RightToolbarGUI.Add(OnToolbarGUIRight);
        }
    
        private static void OnToolbarGUILeft()
        {
            GUILayout.FlexibleSpace();

            if(GUILayout.Button(new GUIContent("AddCoin", "Adds coin")))
            {
                GameObject.FindObjectOfType<Manager_Coin>().AddCoin(5000);
            }
        
            if(GUILayout.Button(new GUIContent("Level", "Opens Selected Level")))
            {
                OpenLevels();
            }
  
            if(GUILayout.Button(new GUIContent("PFL", "Plays From Loader")))
            {
                PlayFromLoader();
            }
            
            if(GUILayout.Button(new GUIContent("DeleteSave", "Deletes Save Data")))
            {
                DeleteSave();
            }
        
            if(GUILayout.Button(new GUIContent("FreshPFL", "Deletes Save Data And PFL")))
            {
                DeleteSave();
                PlayFromLoader();
            }
        }

        private static void DeleteSave()
        {
            PlayerPrefs.DeleteAll();
            var managerSave = GameObject.FindObjectOfType<Manager_Save>();
            managerSave.DeleteAll();
        }

        private static void OnToolbarGUIRight()
        {
            GUILayout.FlexibleSpace();
            if(GUILayout.Button(new GUIContent("Scene Setup Helper", "Sets Up Next Scene")))
            {
                // OpenNextSceneAndSetup();
            }
        }
        private static void OpenLevels()
        {
            var menu = new GenericMenu();
            var guids = AssetDatabase.FindAssets("t:Scene", new[] {levelSceneMainPath});
            SceneAsset[] scenes = new SceneAsset[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                scenes[i] = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            }
                
            foreach (var scene in scenes)
            {
                menu.AddItem(new GUIContent(scene.name), false, SceneHelper.StartScene, scene);
            }
                
            menu.ShowAsContext();
        }

        private static void PlayFromLoader()
        {
            var loader = AssetDatabase.FindAssets("Loader t:Scene")[0];
            var path = AssetDatabase.GUIDToAssetPath(loader);
            EditorSceneManager.OpenScene(path);
            EditorApplication.EnterPlaymode();
        }
    
        private static SceneAsset[] GetScenes(string path)
        {
            var guids = AssetDatabase.FindAssets("t:Scene", new[] {path});
            SceneAsset[] scenes = new SceneAsset[guids.Length];
            for (int i = 0; i < guids.Length; i++)
            {
                string scenePath = AssetDatabase.GUIDToAssetPath(guids[i]);
                scenes[i] = AssetDatabase.LoadAssetAtPath<SceneAsset>(scenePath);
            }

            return scenes;
        }

        private static SceneAsset GetNextScene()
        {
        
            SceneAsset nextScene = null;
            string currentOpenSceneName = EditorSceneManager.GetActiveScene().name;
        
            SceneAsset[] allScenes = GetScenes(levelSceneMainPath);
        
            for (int i = 0; i < allScenes.Length; i++)
            {
                if (allScenes[i].name == currentOpenSceneName)
                {
                    if (i == allScenes.Length - 1)
                    {
                        nextScene = allScenes[0];
                    }
                    else
                    {
                        nextScene = allScenes[i + 1];
                    }
                }
            }
        
            return nextScene;
        }
    }

    internal class SceneHelper
    {
        [Obsolete("Obsolete")]
        public static void StartScene(object userdata)
        {
            SceneAsset scene = (SceneAsset) userdata;
            string scenePath = AssetDatabase.GetAssetPath(scene);
            if (!EditorApplication.SaveCurrentSceneIfUserWantsTo())
                return;
            EditorSceneManager.OpenScene(scenePath);
        }
    
        public static void OpenSceneAdditive(object userdata)
        {
            SceneAsset scene = (SceneAsset) userdata;
            string scenePath = AssetDatabase.GetAssetPath(scene);
            EditorSceneManager.OpenScene(scenePath, OpenSceneMode.Additive);
        }
    }
}