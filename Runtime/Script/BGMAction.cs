using UnityEngine;

namespace KenTank {
public class BGMAction : MonoBehaviour
{
    public static void PlayUI(AudioClip clip)
    {
        GameManager.instance.bGMManager.ui.PlayOneShot(clip);
    }

    public static void PlayEffect(AudioClip clip)
    {
        GameManager.instance.bGMManager.effect.PlayOneShot(clip);
    }

    public static void PlayMusic(AudioClip clip)
    {
        GameManager.instance.bGMManager.music.PlayOneShot(clip);
    }
}}
