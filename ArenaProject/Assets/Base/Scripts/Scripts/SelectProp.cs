using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class SelectProp : NetworkBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float range = 20;
    [SerializeField]
    private LayerMask mask;
    private bool transmutate = false;

    private void Start()
    {
        if (cam == null)
        {
            Debug.Log("No camera referenced!");
            this.enabled = false;
        }
    }

    private void Update()
    {
        if (Input.GetButton("Use"))
        {
            ShowRaycast();
        }
        else if (Input.GetButtonUp("Use"))
        {
            transmutate = true;
            ShowRaycast();
        }
    }

    [Client]
    private void ShowRaycast()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        RaycastHit _ray;
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out _ray, range))
        {
            if (_ray.collider.tag == "Prop")
            {
                if (transmutate)
                {
                    Debug.Log("Transforming into " + _ray.collider.name);
                    CmdSelectProp(this.name, _ray.collider.gameObject);
                    /*this.GetComponent<MeshFilter>().mesh = _ray.collider.gameObject.GetComponent<MeshFilter>().mesh;
                    this.gameObject.transform.localScale = _ray.collider.gameObject.transform.localScale;
                    this.GetComponent<CapsuleCollider>().height = _ray.collider.gameObject.GetComponent<CapsuleCollider>().height;
                    this.GetComponent<CapsuleCollider>().radius = _ray.collider.gameObject.GetComponent<CapsuleCollider>().radius;*/
                    transmutate = false;
                }

                Debug.Log("Selecting " + _ray.collider.name);
            }
        }
    }

    [Command]
    void CmdSelectProp(string _playerID, GameObject prop)
    {
        if (!isLocalPlayer)
        {
            return;
        }

        Player _player = GameManager.GetPlayer(_playerID);

        _player.GetComponent<MeshFilter>().mesh = prop.GetComponent<MeshFilter>().mesh;
        _player.gameObject.transform.localScale = prop.transform.localScale;
        _player.GetComponent<CapsuleCollider>().height = prop.GetComponent<CapsuleCollider>().height;
        _player.GetComponent<CapsuleCollider>().radius = prop.GetComponent<CapsuleCollider>().radius;
        //this.gameObject = _ray.collider.gameObject;
    }

}
