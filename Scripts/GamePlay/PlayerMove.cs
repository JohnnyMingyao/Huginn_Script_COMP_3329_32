using System.Collections;
using UnityEngine;


public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sp;
    private Vector2 v;
    private float MaxFlyForce=250f;
    private float flyForce=250f;
    private bool isFlying=false;
    private float flyTimer = 0f; // 飞行计时器

    //dash values:
    private bool canDash = true;
    private bool isDashing;
    private float dashTime = 0.2f;
    private float dashCooldown = 0.05f;
    private float dashSpeed=20f;
    private int avaliableDashes=3;
    private int maxDashes=3;
    [SerializeField]private TrailRenderer tr;

    public Vector2 startPosition = new Vector2(0f, 0f); 

    public bool isControllable; // 是否可以控制角色
    
    public int getDashes(){
        return avaliableDashes;
    }
    public float getFlyForce(){
        return flyForce;
    }
    public int getMaxDashes(){
        return maxDashes;
    }
    public float getMaxFlyForce(){
        return MaxFlyForce;
    }
    // Start is called before the first frame update

    public void recoverDash(){
        avaliableDashes = maxDashes;
    }
    public void changeMaxDash(int maxDashes){
        this.maxDashes = maxDashes;
    }
    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
        isControllable = true;
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void DirectionInput_X (){
        v = rb.velocity;
        v.x = Input.GetAxis("Horizontal")*6.5f;
        if (v.x>0){
            sp.flipX = true;
        }
        if (v.x<0){
            sp.flipX=false;
        }
        rb.velocity = v;
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 检查碰撞对象名称是否为 "platform" 且包含 Rigidbody2D
        if (collision.gameObject.tag== "platform")
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                DeathManager.Instance.setRespawnPosition(rb.position);
                RestoreStrengthFly();
                recoverDash();
                animator.SetBool("isLanded",true);
                

                Debug.Log($"Collided with {"platform"}! StrengthFly restored."); // 调试输出

            }
        }
        if (collision.gameObject.tag == "ground")
        {
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                animator.SetBool("isLanded",true);
                SFPlayer.Instance.PlayLand();
                Debug.Log($"Collided with ground!"); // 调试输出

            }
        }
        if (collision.gameObject.tag == "wall"){
            Rigidbody2D otherRb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (otherRb != null)
            {
                animator.SetBool("isDead",true);
                DeathManager.Instance.KillPlayer();
                animator.SetBool("isFlying",false);
                RestoreStrengthFly();
                recoverDash();
                SFPlayer.Instance.PlayDie();
                
            }
        }
    }

    private void RestoreStrengthFly()
    {
        flyForce = MaxFlyForce;
        avaliableDashes = maxDashes;
    }


    
    private IEnumerator Dash(){

        canDash = false;
        isDashing = true;
        
        float originalGravityScale = rb.gravityScale;
        rb.gravityScale = 0f;
        Vector2 direction = new Vector2(
            Input.GetAxisRaw("Horizontal"),  // 使用 GetAxisRaw 避免平滑滤波
            Input.GetAxisRaw("Vertical")
        ).normalized;
        Debug.Log($"Input: H={Input.GetAxisRaw("Horizontal")}, V={Input.GetAxisRaw("Vertical")}");
        //if the direction is not set, use the current x direction of player

        
        rb.velocity = direction*dashSpeed;

        tr.emitting = true;
        yield return new WaitForSeconds(dashTime);
        isDashing = false;
        rb.velocity = direction*10f;
        
        rb.gravityScale = originalGravityScale;
        tr.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
        
    }
    void Update()
    {   
        
        if (flyForce<=0){
            isFlying =false;
        }
        
        if (!isControllable)
        {
            rb.velocity = Vector2.zero; // 如果不可以控制，则停止移动
            isDashing = false; // 如果不可以控制，则停止冲刺
            isFlying = false; // 如果不可以控制，则停止飞行
            return;
        }
        if (isDashing)
        {
            if (Input.GetButtonDown("Jump")&& flyForce>0)//still allow flying
            {
                isFlying = true;
                flyTimer = 0f;
                animator.SetBool("isFlying",isFlying);
                animator.SetBool("isLanded",false);
                Debug.Log($"Currentss strengthState: {isFlying}");
                SFPlayer.Instance.StartFly();
            }
            return; // 如果正在冲刺，则不处理其他输入
        }
        DirectionInput_X();
        if (Input.GetButtonDown("Jump") && flyForce>0)
            {
                isFlying = true;
                flyTimer = 0f;
                animator.SetBool("isFlying",isFlying);
                animator.SetBool("isLanded",false);
                Debug.Log($"Currentss strengthState: {isFlying}");
                SFPlayer.Instance.StartFly();
            }
        if (Input.GetButtonUp("Jump"))
            {
                isFlying = false;
                animator.SetBool("isFlying",isFlying);
                SFPlayer.Instance.StopLoopingSound();
                
            }
        //the code for dashing
        if ((Input.GetKeyDown(KeyCode.LeftShift)||Input.GetKeyDown(KeyCode.K)) && canDash &&avaliableDashes>0)
        {   
            
            StartCoroutine(Dash());
            isFlying = false;
            avaliableDashes--;

        }


    }
    void FixedUpdate()
    {   
        if (!isControllable)
        {   
            rb.velocity = Vector2.zero; // 如果不可以控制，则停止移动
            isDashing = false; // 如果不可以控制，则停止冲刺
            isFlying = false;
            return;
        }
        //flying code  
        if(isDashing){
            return; // 如果正在冲刺，则不处理其他输入
        }
        if (isFlying)
        
        {
            //decrease the strength of fly buy time
            flyTimer += Time.deltaTime;
            if (flyTimer >= 0.2f)
            {
                flyForce -= 10f;
                flyForce = Mathf.Max(flyForce, 0f); // 最小值为 0
                flyTimer = 0f; // 重置计时器
                Debug.Log($"Currentss strengthFly: {flyForce}"); // 调试输出
            }


            rb.AddForce(Vector2.up * flyForce, ForceMode2D.Force);
        }else{
            
            if (rb.velocity.y > 0)
            {
                rb.AddForce(Vector2.down * 130f, ForceMode2D.Force);
            }else{
                rb.AddForce(Vector2.down * 100f, ForceMode2D.Force);
            }
            
        }
        

    }
}
