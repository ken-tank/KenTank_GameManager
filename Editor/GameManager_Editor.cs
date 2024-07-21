using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


namespace KenTank.GameManager {
[CustomEditor(typeof(GameManager))]
public class GameManager_Editor : Editor
{
    public VisualTreeAsset visualTree;
    GameManager self => (GameManager)target;
    const int priority = 10;

    [MenuItem("GameObject/KenTank/GameManager/GameManager", false, priority)]
    static void CreateGameManager() 
    {
        string prefab = "Game Manager";
        GameObject instance = Resources.Load<GameObject>(prefab);
        GameObject go = PrefabUtility.InstantiatePrefab(instance) as GameObject;
        Undo.RegisterCreatedObjectUndo(go, "Create " + go.name);
        Selection.activeObject = go;
    }

    

    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        // Create the default inspector
        var defaultInspector = new VisualElement();
        var serializedObject = new SerializedObject(target);
        var iterator = serializedObject.GetIterator();

        if (iterator.NextVisible(true))
        {
            do
            {
                var propertyField = new PropertyField(iterator.Copy());
                propertyField.SetEnabled(iterator.propertyPath != "m_Script");
                defaultInspector.Add(propertyField);
            } while (iterator.NextVisible(false));
        }

        root.Add(defaultInspector);

        if (visualTree == null) return root;

        visualTree.CloneTree(root);

        defaultInspector.Q<ObjectField>("unity-input-sceneManager").RegisterValueChangedCallback((value) => {
            Debug.Log(value.newValue.name);
        });

        if (self.sceneManager)
        {
            var volume = root.Q<Slider>("musicVolume");
            volume.value = self.sceneManager.musicVolume;
            volume.RegisterValueChangedCallback((value) => {
                self.sceneManager.musicVolume = value.newValue;
                EditorUtility.SetDirty(self.sceneManager); 
            });

            var music = root.Q<ObjectField>("musicClip");
            music.objectType = typeof(AudioClip);
            music.value = self.sceneManager.sceneMusic;
            music.RegisterValueChangedCallback((value) => {
                self.sceneManager.sceneMusic = value.newValue as AudioClip;
                EditorUtility.SetDirty(self.sceneManager); 
            });
        }
        else
        {
            root.Q<Slider>("musicVolume").style.display = DisplayStyle.None;
            root.Q<ObjectField>("musicClip").style.display = DisplayStyle.None;
        }

        var transtition = root.Q<ObjectField>("transtition");
        transtition.objectType = typeof(Transtition);
        transtition.value = self.transtitionManager.transition;
        transtition.RegisterValueChangedCallback((value) => {
            self.transtitionManager.transition = value.newValue as Transtition;
            EditorUtility.SetDirty(self.transtitionManager); 
        });

        return root;
    }
}

/// <summary>
/// Adds the given define symbols to PlayerSettings define symbols.
/// Just add your own define symbols to the Symbols property at the below.
/// </summary>
[InitializeOnLoad]
public class AddDefineSymbols : Editor
{
    
    /// <summary>
    /// Symbols that will be added to the editor
    /// </summary>
    public static readonly string [] Symbols = new string[] {
        "KENTANK_GAMEMANAGER"
    };

    static bool onetime = true;
    
    /// <summary>
    /// Add define symbols as soon as Unity gets done compiling.
    /// </summary>
    static AddDefineSymbols ()
    {
        if (!onetime) return;
        string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup ( EditorUserBuildSettings.selectedBuildTargetGroup );
        List<string> allDefines = definesString.Split ( ';' ).ToList ();
        allDefines.AddRange ( Symbols.Except ( allDefines ) );
        PlayerSettings.SetScriptingDefineSymbolsForGroup (
            EditorUserBuildSettings.selectedBuildTargetGroup,
            string.Join ( ";", allDefines.ToArray () ) );
        onetime = false;
    }
}}
