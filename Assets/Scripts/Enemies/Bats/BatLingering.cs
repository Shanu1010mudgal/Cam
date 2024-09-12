using System.Collections;
using UnityEngine;

public class BatLingering : MonoBehaviour
{
    [SerializeField] Transform shawnTransform;
    [SerializeField] Vector2 batPositionVariance;
    [SerializeField] float batFlyingSpeed;
    [SerializeField] float batDiveSpeed;
    [SerializeField] float batWaitBeforeNextAttack;
    [SerializeField] float batFlipAfterReachingY;

    SpriteRenderer batSpriteRenderer;
    Vector2 shawnsCurrentPosition;
    bool isAttacking = false;
    bool canFlipBat = false;

    void Start()
    {
        batSpriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(WaitBeforeNextAttack());
    }

    void Update()
    {
        IdelToAttackPosition();
        FlipBat();

    }

    void IdelToAttackPosition()
    {
        shawnsCurrentPosition = new Vector2(shawnTransform.position.x, shawnTransform.position.y); //Get shawns current position.
        if (!isAttacking)
        {
            Vector2 targetPosition = new Vector2(shawnsCurrentPosition.x + batPositionVariance.x, batPositionVariance.y);
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, batFlyingSpeed * Time.deltaTime); //Move bats to idel position.
        }
        else if (isAttacking)
        {
            transform.position = Vector2.MoveTowards(transform.position, shawnsCurrentPosition, batDiveSpeed * Time.deltaTime); //Let the bat attack shawn.
        }
    }

    void FlipBat()
    {
        if (canFlipBat && transform.position.y > batFlipAfterReachingY)
        {
            batSpriteRenderer.flipX = !batSpriteRenderer.flipX;
            canFlipBat = false;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isAttacking = false;
            canFlipBat = true;
        }
    }

    IEnumerator WaitBeforeNextAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(batWaitBeforeNextAttack);
            isAttacking = true;
            batPositionVariance.x *= -1;
        }
    }
}
