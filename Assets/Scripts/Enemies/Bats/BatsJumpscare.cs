using UnityEngine;

public class BatsJumpscare : MonoBehaviour
{
    [SerializeField] ParticleSystem batsJumpscarePS;
    [SerializeField] AudioSource batsJumpscareAS;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            batsJumpscarePS.Play();
            batsJumpscareAS.Play();
        }
    }
}
