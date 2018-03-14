using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerController : MonoBehaviour {
    
    //Shows in inspector for private fields
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float lookSensitivity = 3f;
    [SerializeField]
    private float jumpHeight = 10f;


    private PlayerMovement move;

    private void Start()
    {

        move = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        //Calc movement velocity
        float _xMove = Input.GetAxisRaw("Horizontal");
        float _zMove = Input.GetAxisRaw("Vertical");

        Vector3 _moveHorizontal = transform.right * _xMove;
        Vector3 _moveVertical = transform.forward * _zMove;

        //Final Movement Vector
        Vector3 _velocity = (_moveHorizontal + _moveVertical).normalized * speed;

        //Apply movement
        move.Move(_velocity);

        //Calculate rotation as a 3D vector (Turning Around)
        float _yRot = Input.GetAxisRaw("Mouse X");

        Vector3 _rotation = new Vector3(0f, _yRot, 0f) * lookSensitivity;

        //Apply Camera Rotation
        move.Rotate(_rotation);

        //Calculate camera rotation as a 3D vector (Looking Around)
        float _xRot = Input.GetAxisRaw("Mouse Y");

        float _cameraRotationX = _xRot * lookSensitivity;

        //Apply Camera Rotation
        move.RotateCamera(_cameraRotationX);

        Vector3 _jumpHeight = Vector3.zero;

        //calculate jump on player input
        if (Input.GetButton("Jump"))
        {
            _jumpHeight = Vector3.up * jumpHeight;
        }

        //Apply jump height
        move.ApplyJump(_jumpHeight);


    }



}
