using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowRoomba : MonoBehaviour
{
    public Transform roomba; // Assign the Roomba GameObject in the Inspector
    public Vector3 offset = new Vector3(0, 1, 0); // Adjust to keep the player above

    void Update()
    {
        if (roomba != null)
        {
            transform.position = roomba.position + offset;
        }
    }
}