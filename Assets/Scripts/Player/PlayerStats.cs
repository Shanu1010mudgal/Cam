using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Walk")]
    public float walkSpeed;
    public float walkAccelerationTime;
    public float walkDecelerationTime;

    [Space(3)]

    [Header("Run")]
    public float runSpeed;
    public float runAccelerationTime;
    public float runDecelerationTime;

    [Space(3)]

    [Header("Jumping")]
    public float jumpSpeed;
    public float jumpTime;
    public float jumpCooldown;
    public float jumpBufferTime;
    public float airAcceleration;
    public float airDeceleration;
    public float cyoteTime;

    [Space(3)]

    [Header("Gravity")]
    public float groundedGravity;
    public float jumpingGravity;
    public float fallingGravity;
    public float incompleteJumpGravity;


    [Space(3)]

    [Header("Sneaking")]
    public float sneakSpeed;
    public float sneakAccelerationTime;
    public float sneakDecelerationTime;

    [Space(3)]

    [Header("Stamina")]
    public float maxStamina;
    public float idleStamina;
    public float walkStamina;
    public float runStamina;
    public float staminaRegenDelay;
}


