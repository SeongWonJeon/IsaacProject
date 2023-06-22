using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    AudioSource[] audioSource = new AudioSource[(int)TypeOfSound.Sounds.MaxCount];
    Dictionary<string, AudioClip> audioClip = new Dictionary<string, AudioClip>();

    public void InIt()
    {
        GameObject root = GameObject.Find("SoundManager");
    }
}
