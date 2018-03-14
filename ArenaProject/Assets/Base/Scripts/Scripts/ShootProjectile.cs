using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootProjectile : MonoBehaviour {

    public Rigidbody BasicProjectile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            Rigidbody rb = Instantiate(BasicProjectile, this.transform.position, this.transform.rotation) as Rigidbody;
        }
    }
}
