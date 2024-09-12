using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraINandOUT : MonoBehaviour
{
    public  CinemachineVirtualCamera VirtualCamera;
    public  CinemachineVirtualCamera zoomCam;
    private PlayerAnimations animations;
    private void Awake()
    { animations = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAnimations>(); } 
    void Start()
    {
       
    }

    // Update is called once per frame
   
    void Update()
    {
        Action action = () => { };
            if (animations.isIdle) { zoomCam.Priority = 10; VirtualCamera.Priority = 5; }
            else if (animations.isRunning) { VirtualCamera.Priority = 10; zoomCam.Priority = 5; }
    }
}
