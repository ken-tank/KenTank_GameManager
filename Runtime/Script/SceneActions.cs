using UnityEngine;

namespace KenTank.GameManager {
public class SceneActions : MonoBehaviour
{
    public void LoadScene(string name) 
    {
        SceneManagement.LoadScene(name);
    }

    public void LoadScene(int buildIndex)
    {
        SceneManagement.LoadScene(buildIndex);
    }

    public void RestartScene()
    {
        SceneManagement.RestartScene();
    }

    public void NextScene() 
    {
        SceneManagement.NextScene();
    }

    public void BackScene() 
    {
        SceneManagement.PrevScene();
    }
}}
