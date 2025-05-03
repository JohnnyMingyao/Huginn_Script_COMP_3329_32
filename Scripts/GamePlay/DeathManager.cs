using System.Collections;


using UnityEngine;

public class DeathManager : MonoBehaviour
{
    public GameObject player;
    public GameObject deathEffectPrefab;
    
    public Transform playerPosition;
    //public ScreenFader screenFader;
    public Rigidbody2D playerRigidbody;
    private Vector2 respawnPosition;
    private Animator animator;
    public static DeathManager Instance;
    
    PlayerMove move;
    public void setRespawnPosition(Vector2 position){
        respawnPosition.x = position.x;
        respawnPosition.y = position.y+0.01f;//to avoid stucking into the ground
    }
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            
            move = player.GetComponent<PlayerMove>();
        }
        else
        {
            Destroy(gameObject);
        }
        respawnPosition = new Vector2(-5.2f,-1f);
        
    }
    void Start()
    {
        animator = player.GetComponent<Animator>();
    }



    public void KillPlayer()
    {
        //here I add some thing need to recover
        keyControl[] keys = FindObjectsOfType<keyControl>();
        foreach (var key in keys)
        {
            Debug.Log("Recovering key: " + key.name);
            key.RecoverKey();
        }
        DoorOpen[] doors = FindObjectsOfType<DoorOpen>();
        foreach (var door in doors)
        {
            Debug.Log("Recovering door");
            door.RecoverDoor();
        }
        
        StartCoroutine(HandleDeathSequence());
    }

    private IEnumerator HandleDeathSequence()
    {
        
        Instantiate(deathEffectPrefab, playerPosition.position, Quaternion.identity);//particle effect
        yield return new WaitForSecondsRealtime(0.2f);
        
        
        StartCoroutine(ScreenFaderManager.Instance.fader.FadeOut(0.5f));//screen turn black
        
        
        move.isControllable = false;//wait for one frame
        yield return new WaitForEndOfFrame();
        Time.timeScale = 0f; 
        
        
        yield return new WaitForSecondsRealtime(0.7f);
        playerPosition.position = respawnPosition;
        
        StartCoroutine(ScreenFaderManager.Instance.fader.FadeIn(0.7f));//screen turn bright
        
        Time.timeScale = 1f; 
        playerRigidbody.velocity = Vector2.zero; 
        yield return new WaitForSecondsRealtime(0.7f);
        
        move.isControllable = true; 
        animator.SetBool("isDead",false);
        animator.SetBool("isFlying",false);
        SFPlayer.Instance.StopLoopingSound();
    }
}

