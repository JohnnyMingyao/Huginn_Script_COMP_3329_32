using UnityEngine;

public class UI_Manager : MonoBehaviour
{
    public void OnStartButtonClicked()
    {
        Debug.Log("Start button clicked");
        MusicManager.Instance.StopMusic();
        GameSceneManager.Instance.LoadLevel("0");
    }

    public void OnQuitButtonClicked()
    {
        Application.Quit();
    }
}
