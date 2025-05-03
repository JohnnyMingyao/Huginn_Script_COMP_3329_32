
using UnityEngine;

public class MakeChoice : MonoBehaviour
{
    // Start is called before the first frame update
    public string whichChoice;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player"){
            MusicManager.Instance.StopMusic();
            string currentPlotCode = GameSceneManager.Plotcode;
            GameSceneManager.Plotcode = currentPlotCode + whichChoice;
            Debug.Log("The current plot code is:" + GameSceneManager.Plotcode);
            GameSceneManager.Instance.SwitchToNext();
        }
    }
    
}
