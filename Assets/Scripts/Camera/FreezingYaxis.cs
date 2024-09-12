using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject Player;
    public float Yaxis = 0f;
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = Player.transform.position;
        newPosition.y = Yaxis; 
        transform.position = newPosition;

    }
}
