using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioLibraries))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] private AudioSource bgm;
    [SerializeField] private AudioSource sfx;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
            bgm.loop = true;
            DontDestroyOnLoad(gameObject);
            PlayBGM();
        } else
        {
            Destroy(this);
        }
    }

    public void PlayBGM()
    {
        bgm.clip = AudioLibraries.instance.GetClip("bgm");
        bgm.Play();
    }

    public void PlayOneTime(string command)
    {
        sfx.PlayOneShot(AudioLibraries.instance.GetClip(command));
    }
}
