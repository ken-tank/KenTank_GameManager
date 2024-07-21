using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.Linq;

namespace KenTank.GameManager {
public class GameManager_Editor : Editor
{
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
