
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToHiddenStage : MonoBehaviour
{
    // Start is called before the first frame update


    private void OnCollisionEnter2D(Collision2D collision)
    {
     
        if (collision.gameObject.tag == "Player"){
            SceneManager.LoadScene("Stage Hidden");
            Debug.Log("Collided ufo");
        }
    }
    
}
