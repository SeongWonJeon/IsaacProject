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
                GameObject gameObject = new GameObject(soundNames[i]);      // ���ӿ�����Ʈ�� i��° �̸��� ���� ������Ʈ�� ����
                audioSources[i] = gameObject.AddComponent<AudioSource>();
                gameObject.transform.parent = root.transform;
            }

            audioSources[(int)Define.Sounds.BGM].loop = true;
        }
    }

    public void Clear()
    {
        // ����� ���� ��ž�ϰ�, ������ ����
        foreach(AudioSource audioSource in audioSources)
        {
            audioSource.clip = null;
            audioSource.Stop();
        }
        // ȿ������ ����
        audioClips.Clear();
    }

    public void Play(AudioClip audioClip, Define.Sounds type = Define.Sounds.Effect, float pitch = 1.0f)
    {
        if (audioClip == null)
            return;

        if (type == Define.Sounds.BGM) // BGM ������� ���
        {
            AudioSource audioSource = audioSources[(int)Define.Sounds.BGM];
            if (audioSource.isPlaying)
                audioSource.Stop();

            audioSource.pitch = pitch;
            audioSource.clip = audioClip;
            audioSource.Play();
        }
        else // Effect ȿ���� ���
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
                path = $"Sounds/{path}"; // Sound ���� �ȿ� ����� �� �ֵ���

            AudioClip audioClip = null;

            if (type == Define.Sounds.BGM) // BGM ������� Ŭ�� ���̱�
            {
                audioClip = GameManager.Resource.Load<AudioClip>(path);
            }
            else // Effect ȿ���� Ŭ�� ���̱�
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