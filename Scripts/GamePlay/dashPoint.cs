using System.Collections;

using UnityEngine;
using UnityEngine.Rendering.Universal;

public class dashPoint : MonoBehaviour
{   
    private bool canUse = true;
    private Light2D glowinglight;

    void Start()
    {
        glowinglight = GetComponentInChildren<Light2D>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            // 触发器被玩家碰撞
            Debug.Log("Player has entered the trigger area.");
            if (canUse){
                other.gameObject.GetComponent<PlayerMove>().recoverDash();
                canUse =false;
                if (glowinglight != null)
                {
                    glowinglight.intensity = 0.1f;
                }else{
                    Debug.Log("mei bang ding");
                }
                StartCoroutine(ResetLightAndDash());
                
            }
        }
        
    }   
    private IEnumerator ResetLightAndDash()
    {
        yield return new WaitForSeconds(1f); 

        if (glowinglight != null)
        {
            glowinglight.intensity = 1.0f; 
        }

        canUse = true;
    }
    
}
