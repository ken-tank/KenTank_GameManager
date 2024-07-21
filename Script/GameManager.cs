using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

    public SettingsManager settingsManager;
    public BGMManager bGMManager;
    public SceneManagement sceneManager;
    public TranstitionManager transtitionManager;

    void Awake()
    {
        SetTimeScale(1);

        if (instance == null)
        {
            Initialize();
        }
        else
        {
            sceneManager.Initialize();
            settingsManager.Initialize();
            transtitionManager.Initialize();
            
            Destroy(gameObject);
        }
    }

    void Initialize() 
    {
        // Init GameManager
        instance = this;
        Screen.sleepTimeout = SleepTimeout.NeverSleep;

        // Init AnotherManager
        settingsManager.Initialize();
        sceneManager.Initialize();
        transtitionManager.Initialize();

        transform.SetParent(null);
        DontDestroyOnLoad(gameObject);
    }

    public static void SetTimeScale(float value) 
    {
        Time.timeScale = value;
    }

    public static void Quit() 
    {
        Application.Quit();
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
