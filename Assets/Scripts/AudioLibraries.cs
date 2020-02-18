using System.Collections.Generic;
using UnityEngine;

public class AudioLibraries : MonoBehaviour
{
    public static AudioLibraries instance;

    [SerializeField] private List<AudioData> clips;

    private Dictionary<string, AudioClip> audioClips;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            audioClips = new Dictionary<string, AudioClip>();
            CopyToDictionary();
            instance = this;
        } else
        {
            Destroy(this);
        }
    }

    // Just want to copy my list into dictionary to make it easier to call elements by name
    void CopyToDictionary()
    {
        foreach (AudioData data in clips)
        {
            audioClips.Add(data.name, data.clip);
        }
    }

    public AudioClip GetClip(string name)
    {
        return audioClips[name];
    }
}

[System.Serializable]
public class AudioData
{
    public string name;
    public AudioClip clip;

    public AudioData(string name, AudioClip clip)
    {
        this.name = name;
        this.clip = clip;
    }
}
