using UnityEngine;

public class SFPlayer : MonoBehaviour
{
    public static SFPlayer Instance;

    public AudioSource srcLoop;     // 专门用于循环播放（走路、飞行）
    public AudioSource srcOneShot;  // 瞬时播放（捡钥匙、开门）

    public AudioClip fly, walk, key, doorOpen, die, land;

    void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // 如果你希望它持久
            }
            else
            {
                Destroy(gameObject);
            }
        }

    // 一次性播放
    public void PlayKey() => srcOneShot.PlayOneShot(key);
    public void PlayDoorOpen() => srcOneShot.PlayOneShot(doorOpen);
    public void PlayDie() => srcOneShot.PlayOneShot(die);
    public void PlayLand() => srcOneShot.PlayOneShot(land);

    // 循环播放控制
    public void StartWalk()
    {
        if (srcLoop.clip != walk)
        {
            srcLoop.clip = walk;
            srcLoop.loop = true;
            srcLoop.Play();
        }
    }

    public void StartFly()
    {
        if (srcLoop.clip != fly)
        {
            srcLoop.clip = fly;
            srcLoop.loop = true;
            srcLoop.Play();
        }
    }

    public void StopLoopingSound()
    {
        srcLoop.Stop();
        srcLoop.clip = null;
    }
}
