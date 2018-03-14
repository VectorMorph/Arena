using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPad : MonoBehaviour {

    public float BoostHeight;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("Boosting!");
            other.GetComponent<Rigidbody>().AddRelativeForce(Vector3.up * BoostHeight, ForceMode.Impulse);
        }
    }
}
