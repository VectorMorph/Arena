using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour {

    public GameObject dest;
    public float Offset;

    private void OnCollisionEnter(Collision collision)
    {
        collision.rigidbody.MovePosition(new Vector3(dest.transform.position.x, dest.transform.position.y, (dest.transform.position.z + Offset)));
    }

}
