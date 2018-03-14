using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private Camera firstCam;
    [SerializeField]
    private Camera thirdCam;

    private Vector3 velocity = Vector3.zero;
    private Vector3 rotation = Vector3.zero;
    private float cameraRotationX = 0f;
    private float currentCameraRotationX = 0;
    private Vector3 jumpHeight = Vector3.zero;
    private Vector3 _jetpackThrust = Vector3.zero;

    [SerializeField]
    private float cameraRotationLimit = 85f;
    [SerializeField]
    private bool hasJetpack;
    [SerializeField]
    private float sprintMultiplier = 2f;


    private Rigidbody rb;
    [SerializeField]
    private bool onGround;
    private RaycastHit groundRay;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    //Gets a movement vector
    public void Move(Vector3 _velocity)
    {
        velocity = _velocity;
    }

    //Gets rotational Vector
    public void Rotate(Vector3 _rotation)
    {
        rotation = _rotation;
    }

    //Gets rotational Vector for camera
    public void RotateCamera(float _cameraRotationX)
    {
        cameraRotationX = _cameraRotationX;
    }

    //get force vector for jump
    public void ApplyJump(Vector3 _jumpHeight)
    {
        jumpHeight = _jumpHeight;
        
    }

    private void FixedUpdate()
    {
        GroundCheck();
        PerformMovement();
        PerformRotation();
    }

    //Perform movement based on velocity variable
    void PerformMovement()
    {
        if (Input.GetButton("Sprint"))
        {
            if (velocity != Vector3.zero)
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime * sprintMultiplier);
            }
        }
        else
        {
            if (velocity != Vector3.zero)
            {
                rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
            }
        }

        if(velocity != Vector3.zero)
        {
            rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
        }

        if(jumpHeight != Vector3.zero && onGround)
        {
            rb.AddForce(jumpHeight * Time.fixedDeltaTime, ForceMode.Impulse);
        }

        if (hasJetpack && !onGround)
        {
            rb.AddForce(jumpHeight * Time.fixedDeltaTime / 10, ForceMode.Impulse);
        }

    }

    void PerformRotation()
    {
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rotation));
        if(firstCam != null)
        {
            //set rotation and clamp it
            currentCameraRotationX -= cameraRotationX;
            currentCameraRotationX = Mathf.Clamp(currentCameraRotationX, -cameraRotationLimit, cameraRotationLimit);

            //apply rotation to camera
            firstCam.transform.localEulerAngles = new Vector3(currentCameraRotationX, 0, 0);
            thirdCam.transform.RotateAround(this.transform.position, new Vector3(currentCameraRotationX, 0, 0), 0);
        }
    }

    void GroundCheck()
    {
        //RaycastHit hit;
        float distance = 1.2f;
        Vector3 dir = new Vector3(0, -1);

        if (Physics.Raycast(transform.position, dir, out groundRay, distance))
        {
            onGround = true;
        }
        else
        {
            onGround = false;
        }
    }

}
