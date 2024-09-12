using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    #region Unity Refrences
    private PlayerStats stats;
    private PlayerInput input;
    private PlayerAnimations anim;
    private Rigidbody2D rb;
    private BoxCollider2D groundCheck;
    #endregion

    private float lastTimeOnGround;
    private float lastTimeJumped;
    private float jumpHeldTime;
    private float jumpBufferTimer;
    private float airAccelRate;
    private float airAccel;
    private float airDecel;
    private bool jumpPressed;
    private bool canJump;
    private float accelRate;
    private float targetSpeed;

    #region Public Variables
    public float horizontalVelocity;
    public float verticleVelocity;
    #endregion

    #region CurrentState
    [HideInInspector] public bool isGrounded;
    #endregion

    void Awake()
    {
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInput>();
        anim = GetComponent<PlayerAnimations>();
        rb = GetComponent<Rigidbody2D>();
        groundCheck = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalVelocity = rb.velocity.x;
        verticleVelocity = rb.velocity.y;

        #region Checks
        isGrounded = groundCheck.IsTouchingLayers(LayerMask.GetMask("Ground"));
        #endregion

        #region Timers
        lastTimeOnGround = isGrounded ? 0 : lastTimeOnGround += Time.deltaTime;
        lastTimeJumped = jumpPressed ? 0 : lastTimeJumped += Time.deltaTime;
        jumpHeldTime = jumpPressed ? jumpHeldTime += Time.deltaTime : 0;
        jumpBufferTimer = input.jumpInput ? jumpBufferTimer += Time.deltaTime : 0;
        #endregion
    }

    void FixedUpdate()
    {
        #region Walk & Run & Sneak

        float targetVelocity = input.moveInput.x * targetSpeed;

        if (input.runInput && !anim.isAiming)
        {
            //calculate run acceleration or deceleration
            accelRate = (Mathf.Abs(horizontalVelocity) > stats.walkSpeed) ? stats.runSpeed / stats.runAccelerationTime : stats.runSpeed / stats.runDecelerationTime;

            //set runspeed
            if (isGrounded)
                targetSpeed = stats.runSpeed;
        }
        else if (input.sneakInput && isGrounded && !anim.isRunning)
        {
            accelRate = (Mathf.Abs(targetVelocity) > 0f) ? stats.sneakSpeed / stats.sneakAccelerationTime : stats.sneakSpeed / stats.sneakDecelerationTime;
            targetSpeed = stats.sneakSpeed;
        }
        else
        {
            //calculate walk acceleration or deceleration
            if (Mathf.Abs(horizontalVelocity) <= stats.walkSpeed)
                accelRate = (Mathf.Abs(targetVelocity) > stats.sneakSpeed) ? stats.walkSpeed / stats.walkAccelerationTime : stats.walkSpeed / stats.walkDecelerationTime;

            //set walkspeed
            targetSpeed = stats.walkSpeed;
        }

        if (isGrounded)
        {
            airAccelRate = 1f;
        }
        else
        {
            //set air acceleration or deceleration multiplier if not on ground
            airAccelRate = Mathf.Abs(targetVelocity) > Mathf.Abs(horizontalVelocity) ? stats.airAcceleration : stats.airDeceleration;
        }

        //calculate difference between current velocity and to reach velocity
        float speedDif = targetVelocity - rb.velocity.x;
        float movement = speedDif * accelRate * airAccelRate;

        //actual movement
        rb.AddForce(movement * Vector2.right, ForceMode2D.Force);

        #region Movement Corrections

        //set Velocity to 0 if near 0 and no move input
        if (input.moveInput.x == 0 && Mathf.Abs(horizontalVelocity) <= 0.5f)
            rb.velocity = new Vector2(0, rb.velocity.y);

        //set Velocity to walkSpeed if near walkSpeed and no run input
        if (!input.runInput && Mathf.Abs(horizontalVelocity) <= stats.walkSpeed + 0.1f && Mathf.Abs(horizontalVelocity) > stats.walkSpeed)
            rb.velocity = new Vector2(input.moveInput.x * stats.walkSpeed, rb.velocity.y);

        //set Velocity to runSpeed if near runSpeed and run input
        if (input.runInput && Mathf.Abs(horizontalVelocity) >= stats.runSpeed - 0.1f)
            rb.velocity = new Vector2(input.moveInput.x * stats.runSpeed, rb.velocity.y);
        #endregion

        #endregion

        #region Jumping & Gravity
        if (input.jumpInput)
        {
            if (lastTimeOnGround <= stats.cyoteTime  && lastTimeJumped > stats.jumpCooldown && canJump)
            {
                jumpPressed = true;
            }
        }
        else if (!input.jumpInput)
        {
            if (jumpPressed)
            {
                if (verticleVelocity > 0)
                    rb.velocity = new Vector2(horizontalVelocity, 0);

                jumpPressed = false;    
            }
              
        }

        canJump = isGrounded && jumpBufferTimer <= stats.jumpBufferTime;

        if (jumpHeldTime > stats.jumpTime)
            jumpPressed = false;

        if (jumpPressed && jumpHeldTime <= stats.jumpTime)
        {
           rb.velocity = new Vector2(horizontalVelocity, stats.jumpSpeed);
        }

        if (!isGrounded)
        {
            if (verticleVelocity > 0)
            {
                rb.gravityScale = stats.jumpingGravity;
            }
            else
            {
                bool jumpCompleted = (jumpHeldTime > stats.jumpTime * 0.75);
                rb.gravityScale = jumpCompleted ? stats.fallingGravity : stats.incompleteJumpGravity;
            }
        }
        else
        {
            rb.gravityScale = stats.groundedGravity;
            jumpHeldTime = 0;
        }
        #endregion

        #region Flip Player
        if (!anim.isAiming)
        {
            if (rb.velocity.x < 0f && input.moveInput.x < 0f)
                transform.localScale = new Vector2(-Mathf.Abs(transform.localScale.x), transform.localScale.y);
            else if (rb.velocity.x > 0f && input.moveInput.x > 0f)
                transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        #endregion

    }
}
