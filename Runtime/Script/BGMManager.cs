using System.Collections;
using UnityEngine;

namespace KenTank.GameManager {
public class BGMManager : MonoBehaviour
{
    public AudioSource music, effect, ui;

    public static AudioSource _currentMusic;

    public void PlayMusic(AudioClip clip, float volume = 1, float smoothFade = 1)
    {
        IEnumerator transtition()
        {
            var newSource = Instantiate(music);
            DontDestroyOnLoad(newSource.gameObject);
            newSource.clip = clip;
            newSource.volume = 0;
            newSource.Play();
            float currentVolume = 0;

            if (_currentMusic) currentVolume = _currentMusic.volume;

            float duration = smoothFade;
            float time = 0;
            if (_currentMusic)
            {
                while (time <= duration)
                {
                    time += Time.unscaledDeltaTime;
                    float t = Mathf.InverseLerp(0, duration, time);
                    if (_currentMusic) _currentMusic.volume = Mathf.Lerp(currentVolume, 0, t);
                    newSource.volume = Mathf.Lerp(0, volume, t);
                    yield return null;
                }
            }
            /*
            time = 0;
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                float t = Mathf.InverseLerp(0, duration, time);
                newSource.volume = Mathf.Lerp(0, volume, t);
                yield return null;
            }
            */

            if (_currentMusic) Destroy(_currentMusic.gameObject);
            _currentMusic = newSource;
            _currentMusic.volume = volume;
        }

        IEnumerator play()
        {
            var newSource = Instantiate(music);
            DontDestroyOnLoad(newSource.gameObject);
            newSource.clip = clip;
            newSource.volume = 0;
            newSource.Play();

            float duration = smoothFade;
            float time = 0;
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                float t = Mathf.InverseLerp(0, duration, time);
                newSource.volume = Mathf.Lerp(0, volume, t);
                yield return null;
            }
            _currentMusic = newSource;
            _currentMusic.volume = volume;
        }

        IEnumerator changeVolume()
        {
            float time = 0;
            float start = _currentMusic.volume;
            while (time <= 2)
            {
                time += Time.unscaledDeltaTime;
                _currentMusic.volume = Mathf.Lerp(start, volume, Mathf.InverseLerp(0, 2, time));
                yield return null;
            }
        }

        if (clip)
        {
            if (_currentMusic) 
            {
                if (_currentMusic.clip != clip) StartCoroutine(transtition());
                else StartCoroutine(changeVolume());
            }
            else
            {
                StartCoroutine(play());
            }
        }
    }

    public void StopMusic(float smoothFade = 0.5f)
    {
        IEnumerator transtition() 
        {
            float start = _currentMusic.volume;
            float duration = smoothFade;
            float time = 0;
            while (time <= duration)
            {
                time += Time.unscaledDeltaTime;
                float t = Mathf.InverseLerp(0, duration, time);
                _currentMusic.volume = Mathf.Lerp(start, 0, t);
                yield return null;
            }
            Destroy(_currentMusic.gameObject);
            _currentMusic = null;
        }
        
        if (_currentMusic)
        {
            StartCoroutine(transtition());
        }
    }

    public void PlayUI(AudioClip clip, float volume = 1)
    {
        if (clip) ui.PlayOneShot(clip, volume);
    }

    public void PlayEffect(AudioClip clip, float volume = 1)
    {
        if (clip) effect.PlayOneShot(clip, volume);
    }

    public static void PlayMusicOnce(AudioClip clip) 
    {
        GameManager.instance.bGMManager.music.PlayOneShot(clip);
        if (_currentMusic) GameManager.instance.bGMManager.PlayMusic(_currentMusic.clip, 0f);
    }
}}
