using UnityEngine;
using UnityEngine.U2D.IK;

public class PlayerInverseKinematics : MonoBehaviour
{
    private PlayerInput input;
    private PlayerMovement move;
    private PlayerAnimations anim;

    private IKManager2D ikManager;

    [SerializeField] GameObject target;
    [SerializeField] GameObject headTarget;
    [SerializeField] GameObject handTarget;
    [SerializeField] GameObject defaultHeadPos;
    [SerializeField] GameObject defaultHandPos;
    [SerializeField] GameObject aiming;
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        move = GetComponent<PlayerMovement>();
        anim = GetComponent<PlayerAnimations>();

        ikManager = GameObject.Find("Root").GetComponent<IKManager2D>();
    }
    private void FixedUpdate()
    {

        Vector3 mousePos = Camera.main.ViewportToWorldPoint(input.aimPos);

        Vector3 aimPoint = mousePos - aiming.transform.position;

        float rotateAim = Mathf.Atan2(aimPoint.y, aimPoint.x) * Mathf.Rad2Deg;

        //if(rotateAim  > -20 && rotateAim < 340)
        aiming.transform.rotation = Quaternion.Euler(0, 0, rotateAim);

        if (input.aimInput && !anim.isRunning)
        {
            target.SetActive(true);
            ikManager.enabled = true;
            anim.isAiming = true;

            if (input.aimPos.x > 0.5)
            {
                aiming.transform.localScale = new Vector2(1, aiming.transform.localScale.y);
                transform.localScale = new Vector2(1, transform.localScale.y);
            }
            else if (input.aimPos.x < 0.5)
            {
                aiming.transform.transform.localScale = new Vector2(-1, aiming.transform.localScale.y);
                transform.localScale = new Vector2(-1, transform.localScale.y);
            }

            headTarget.transform.position = Vector2.MoveTowards(headTarget.transform.position, target.transform.position, 0.5f);
            handTarget.transform.position = Vector2.MoveTowards(handTarget.transform.position, target.transform.position, 0.5f);
        }
        else
        {
            target.SetActive(false);
            headTarget.transform.position = Vector2.MoveTowards(headTarget.transform.position, defaultHeadPos.transform.position, 0.5f);
            handTarget.transform.position = Vector2.MoveTowards(handTarget.transform.position, defaultHandPos.transform.position, 0.5f);

            if (handTarget.transform.position == defaultHandPos.transform.position && headTarget.transform.position == defaultHeadPos.transform.position)
            {
                ikManager.enabled = false;
                anim.isAiming = false;
            }

            aiming.transform.rotation = Quaternion.Euler(0, 0, transform.localScale.x);
        }
    }
}