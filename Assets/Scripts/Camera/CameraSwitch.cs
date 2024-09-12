using UnityEngine;
using Cinemachine;
using UnityEngine.UIElements;

public class CameraFollowController : MonoBehaviour
{
 
    public Transform Front;
    public Transform Center;
    public CinemachineVirtualCamera Vcam;
    public Transform FollowTarget;


    private PlayerAnimations animations;
   

    void Awake()
      {
         animations = GameObject.Find("Shaun Murphy (Player)").GetComponent<PlayerAnimations>();
        FollowTarget = Center;
        Vcam.Follow = FollowTarget;
      
         

    }

    void Update()                       
    {

   
    }

    void ZoomWhenStill() { }
    void ShiftCamera() { }
    

}
