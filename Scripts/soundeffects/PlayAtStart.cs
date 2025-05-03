using UnityEngine;

public class PlayAtStart : MonoBehaviour
{
    public AudioClip bgm;

    void Start()
    {
        MusicManager.Instance.PlayMusic(bgm);
    }
}

