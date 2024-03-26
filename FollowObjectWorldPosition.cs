using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowObjectWorldPosition : MonoBehaviour
{
    // Simple script which makes the parent object follow the world position of another object


    public GameObject ObjectToFollow;

    public bool FollowX = true;
    public bool FollowY = true;
    public bool FollowZ = true;

    public Vector3 FollowOffset = Vector3.zero;


    private void Update() {
        if (ObjectToFollow == null) return;

        var position = transform.position;

        if (FollowX) {
            position.x = ObjectToFollow.transform.position.x + FollowOffset.x;
        }

        if (FollowY) {
            position.y = ObjectToFollow.transform.position.y + FollowOffset.y;
        }

        if (FollowZ) {
            position.z = ObjectToFollow.transform.position.z + FollowOffset.z;
        }

        transform.position = position;  
    }
}
