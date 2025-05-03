
using UnityEngine;

public class ScreenFaderManager : MonoBehaviour
{
    public static ScreenFaderManager Instance;
    public ScreenFader fader;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
