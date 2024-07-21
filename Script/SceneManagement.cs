using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class SceneManagement : MonoBehaviour
{
    public TranstitionManager transtition;
    public AudioClip sceneMusic;
    [Range(0, 1)] public float musicVolume = 1;

    [System.Serializable]
    public class Events {
        public UnityEvent onInitialized;
        public UnityEvent onNextSceneFailed;
    } public Events events;

    public void Initialize() 
    {
        PlayCurrentMusic();
        events.onInitialized.Invoke();
    }

    public async static void LoadScene(string name)
    {
        var transtition = GameManager.instance.sceneManager.transtition;

        transtition.Close();
        transtition.waiting = true;

        var operation = SceneManager.LoadSceneAsync(name);
        operation.allowSceneActivation = false;
        float progress = 0;

        while (transtition.waiting) await Task.Yield();

        while (progress != 1)
        {
            progress = Mathf.InverseLerp(0, 0.9f, operation.progress);
            await Task.Yield();
        }
        
        operation.allowSceneActivation = true;
        transtition.Open();
    }

    public static void LoadScene(int index)
    {
        string scenePath = SceneUtility.GetScenePathByBuildIndex(index);
        string sceneName = System.IO.Path.GetFileNameWithoutExtension(scenePath);
        LoadScene(sceneName);
    }

    public static void RestartScene() 
    {
        LoadScene(SceneManager.GetActiveScene().name);
    }

    public static void NextScene() 
    {
        int index = SceneManager.GetActiveScene().buildIndex;
        if (index < SceneManager.sceneCountInBuildSettings-1)
        {
            LoadScene(index + 1);
        }
        else
        {
            GameManager.instance.sceneManager.events.onNextSceneFailed.Invoke();
        }
    }

    public static void PrevScene() 
    {
        LoadScene(SceneManager.GetActiveScene().buildIndex-1);
    }

    public void PlayCurrentMusic()
    {
        if (sceneMusic) PlayMusic(sceneMusic, musicVolume);
    }

    public static void PlayMusic(AudioClip clip, float volume = 1)
    {
        if (clip) GameManager.instance.bGMManager.PlayMusic(clip, volume);
    }

    public static void StopMusic(float smoothFade) 
    {
        GameManager.instance.bGMManager.StopMusic(smoothFade);
    }
}
