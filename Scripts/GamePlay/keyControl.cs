
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class keyControl : MonoBehaviour
{
    // Start is called before the first frame update
    private bool canUse = true;
    private Light2D glowinglight;
    public DoorOpen doorToControl;
    void Start()
    {
        glowinglight = GetComponentInChildren<Light2D>();
    }

    public void RecoverKey(){
        canUse = true;
        doorToControl.lossAllKey();
        glowinglight.intensity = 2.0f;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {
            
            if (canUse){
                if (glowinglight != null)
                {
                    glowinglight.intensity = 0.0f;
                    canUse =false;
                    SFPlayer.Instance.PlayKey();
                    doorToControl.AddOneKey();
                   

                }else{
                    Debug.Log("not gameobject attached to glowing light");
                }
                
            }
        }
        
    }   
}
