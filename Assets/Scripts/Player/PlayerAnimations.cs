using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private PlayerStats stats;
    private PlayerInput input;
    private PlayerMovement move;

    Animator anim;

    public bool isIdle;
    public bool isWalking;
    public bool isRunning;
    public bool isJumping;
    public bool isFalling;
    public bool isAiming;
    public bool isSneaking;

    float maxSpeed;
    float animDirection;

    void Awake()
    {
        anim = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMovement>();
    }

    void FixedUpdate()
    {
        AnimationFloats();
        CheckConditions();
        SetBoolAnimations();
    }

    void AnimationFloats()
    {
        maxSpeed = Mathf.Abs(move.horizontalVelocity) > stats.walkSpeed ? stats.runSpeed : stats.walkSpeed;
        float animSpeed = Mathf.Round(Mathf.Abs(move.horizontalVelocity / maxSpeed) * 100) / 100;

        if (isWalking || isRunning)
        {
            if (move.horizontalVelocity / Mathf.Abs(move.horizontalVelocity) == transform.localScale.x)
                animDirection = 1;
            else
                animDirection = -1;

        }
        anim.SetFloat("AnimationSpeed", animSpeed * animDirection);
    }
    void CheckConditions()
    {
        isIdle = !isWalking && !isRunning && !isJumping && !isFalling && !isSneaking;

        isWalking = move.isGrounded && Mathf.Abs(move.horizontalVelocity) > 0.1f && Mathf.Abs(move.horizontalVelocity) <= stats.walkSpeed && !input.sneakInput;

        isRunning = move.isGrounded && Mathf.Abs(move.horizontalVelocity) > stats.walkSpeed;

        isJumping = !move.isGrounded && move.verticleVelocity > 0;

        isFalling = !move.isGrounded && move.verticleVelocity <= 0;

        isSneaking = input.sneakInput && move.isGrounded && Mathf.Abs(move.horizontalVelocity) <= stats.sneakSpeed;
    }
    void SetBoolAnimations()
    {
        anim.SetBool("Idle", isIdle);
        anim.SetBool("Walking", isWalking);
        anim.SetBool("Running", isRunning);
        anim.SetBool("Falling", isFalling);
        anim.SetBool("Jumping", isJumping);
        anim.SetBool("Sneaking", isSneaking);
    }
}
