using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource[] audioSources = new AudioSource[(int)Define.Sounds.MaxCount];
    Dictionary<string, AudioClip> audioClips = new Dictionary<string, AudioClip>();

    private void Awake()
    {
        GameObject root = GameObject.Find("SoundRoot");

        if (root == null)
        {
            root = new GameObject { name = "SoundRoot" };
            Object.DontDestroyOnLoad(root);

            string[] soundNames = System.Enum.GetNames(typeof(Define.Sounds));     // Bgm, Effect

            for (int i = 0; i < soundNames.Length - 1; i++)
            {
                GameObject gameObject = new GameObject(soundNames[i]);      // 게임오브잭트로 i번째 이름을 가진 오브잭트를 생성
                audioSources[i] = gameObject.AddComponent<AudioSource>();
                gameObject.transform.parent = root.transform;
            }

            audioSources[(int)Define.Sounds.BGM].loop = true;
        }
    }

    public void Clear()
    {
        // 재생을 전부 스탑하고, 음반을 뺀다
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // 효과음을 비운다
        audioClips.Clear();
    }

    public void Play(AudioClip audioClip, Define.Sounds type = Define.Sounds.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sounds.BGM) // BGM 배경음악 재생
        {
            AudioSource audioSource = audioSources[(int)Define.Sounds.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect 효과음 재생
        {
            AudioSource audioSource = audioSources[(int)Define.Sounds.Effect];
            audioSource.pitch = pitch;
            audioSource.PlayOneShot(audioClip);
        }
    }

    public void Play(string path, Define.Sounds type = Define.Sounds.Effect, float pitch = 1.0f)
    {
        AudioClip audioClip = GetOrAddAudioClip(path, type);
        Play(audioClip, type, pitch);

        AudioClip GetOrAddAudioClip(string path, Define.Sounds type = Define.Sounds.Effect)
        {
            if (path.Contains("Sounds/") == false)
                path = $"Sounds/{path}"; // Sound 폴더 안에 저장될 수 있도록

            AudioClip audioClip = null;

            if (type == Define.Sounds.BGM) // BGM 배경음악 클립 붙이기
            {
                audioClip = GameManager.Resource.Load<AudioClip>(path);
            }
            else // Effect 효과음 클립 붙이기
            {
                if (audioClips.TryGetValue(path, out audioClip) == false)
                {
                    audioClip = GameManager.Resource.Load<AudioClip>(path);
                    audioClips.Add(path, audioClip);
                }
            }

            if (audioClip == null)
                Debug.Log($"AudioClip Missing ! {path}");

            return audioClip;
        }
    }
}