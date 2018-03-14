using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class Jetpack : MonoBehaviour {

    [SerializeField]
    private float jetpackThrust;

    private PlayerMovement move;
    private PlayerController player;

    private void OnCollisionEnter(Collision collision)
    {
    }

}
