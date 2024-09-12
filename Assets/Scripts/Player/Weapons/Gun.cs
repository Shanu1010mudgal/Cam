using UnityEngine;

public class Gun : MonoBehaviour
{
    private PlayerInverseKinematics ik;
    GameObject bullet;

    void Awake()
    {
        ik = GetComponentInParent<PlayerInverseKinematics>();
    }

    void FixedUpdate()
    {
        
    }
}
