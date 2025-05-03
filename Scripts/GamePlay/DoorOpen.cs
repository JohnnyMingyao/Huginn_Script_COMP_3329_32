using System.Collections;

using UnityEngine;
using UnityEngine.Rendering.Universal;
public class DoorOpen : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public int RequiredKeys = 4;
    private int currentKeys = 0;

    private Light2D doorLight;
    private Collider2D doorCollider;
    private SpriteRenderer spriteRenderer;
    private bool opened = false;
    void Start()
    {
        doorLight = GetComponentInChildren<Light2D>();
        doorCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void lossAllKey(){
        currentKeys = 0;
    }

   public void AddOneKey()
    {
        if (opened) return;

        currentKeys++;
        if (currentKeys >= RequiredKeys)
        {
            StartCoroutine(Opendoor());
        }
    }
    IEnumerator Opendoor()
    {
        opened = true;

        // 1. 快速发光
        if (doorLight != null)
        {
            float originalIntensity = doorLight.intensity;
            doorLight.intensity = 15.0f;
            yield return new WaitForSeconds(0.2f);
            doorLight.intensity = originalIntensity;

        }else{
            Debug.Log("Door is not connected to light source");
        }
        SFPlayer.Instance.PlayDoorOpen();


        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = false;
        }

        if (doorCollider != null)
        {
            doorCollider.enabled = false;
        }
        
        yield break;
    }
    public void RecoverDoor(){
        spriteRenderer.enabled = true;
        doorCollider.enabled = true;
        opened = false;
    }
}
