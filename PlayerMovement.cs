using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float jumpingPower = 12f;
    private bool isFacingRight = true;
    private int state=0;

    private bool risingflag=false;
    private bool fallingflag=false;

    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private LayerMask groundLayer;

    private Animator animator;


    void Awake(){
        animator = GetComponent<Animator>();

    }

    void Update()
    {



        state=StateMachine(state);

/*

        horizontal = Input.GetAxisRaw("Horizontal");


        if(horizontal!=0){
            animator.Play("PlayerRunning");
        }

        if(horizontal==0){
            animator.Play("PlayerIdle");
        }



        if (Input.GetButtonDown("Jump") && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
        }

        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
        {

            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
        }*/

        Flip();
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * speed, rb.velocity.y);
    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f || !isFacingRight && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }


    int StateMachine(int state){


         switch(state) 
        {
            case 0://Idle

            horizontal = Input.GetAxisRaw("Horizontal");


            if(horizontal!=0 && IsGrounded()){
                state=1;
            }


            if (Input.GetButtonDown("Jump") && IsGrounded())
            {
                animator.Play("PlayerJumpingStart");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                
                state=2;
            }


            if (Input.GetButtonDown("Fire1")){
                state=3;
            }
            
            animator.Play("PlayerIdle");
            

            break;
            case 1://Running
            horizontal = Input.GetAxisRaw("Horizontal");

            if(horizontal==0 && IsGrounded()){
                state=0;
            }

            if (Input.GetButtonDown("Jump") && IsGrounded() )
            {
                animator.Play("PlayerJumpingStart");
                rb.velocity = new Vector2(rb.velocity.x, jumpingPower);
                state=2;
            }


            if (Input.GetButtonDown("Fire1")){
                state=3;
            }

            animator.Play("PlayerRunning");

            // code block
            break;


            case 2://Jumping
            horizontal = Input.GetAxisRaw("Horizontal");


            if (Input.GetButtonUp("Jump") && rb.velocity.y > 0f)
            {

                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
            }


            if (risingflag){
                
                animator.Play("PlayerJumpingRising");
            }

            if ((rb.velocity.y <= 1f && rb.velocity.y >= -0.1f) && risingflag){
                animator.Play("PlayerJumpingMax");
                risingflag=false;
            }





            if (rb.velocity.y < -0.1f){
                animator.Play("PlayerJumpingFalling");
                fallingflag=true;
            }




            if(fallingflag && IsGrounded()){
                
                animator.Play("PlayerJumpingGround");
            }


            //animator.Play("PlayerIdle");

            // code block
            break;

            case 3://Attacking
                horizontal=0;
                animator.Play("PlayerAttack");

            break;

            default:
            // code block
            break;
}

        return state;
    }

    void AnimationGroundFinish(){
        fallingflag=false;
        state=0;
    }

    void AnimationJumpStart(){
        risingflag=true;
    }

    void AnimationAttackfinish(){
        state=0;
    }

}
