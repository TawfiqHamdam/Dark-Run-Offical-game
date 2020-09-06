using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

[RequireComponent(typeof(PlayerMoter))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask MovementMask;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float controllSpeed = 1f;
    [SerializeField] float ShiftSpeed = 15f;

    [SerializeField] Transform rotationHelper;
    //[SerializeField] GameObject playerBody;

    [SerializeField] float speedMultiplier = 1f;
    [SerializeField] float maxSpeed = 2f;

    [SerializeField] Animator animator;

    [SerializeField] float jumpValue=2000;
    float animState;

    Camera cam;
    PlayerMoter moter;

    float ZThrow, YThrow, XThrow;
    float xAxisClamp = 0;

    bool isJumping;
    bool isSettingJump;

    void Start()
    {
        cam = Camera.main;
        moter = GetComponent<PlayerMoter>();
        isJumping = false;
        isSettingJump = false;
        //animator = GetComponent<Animator>();
        animState = 0;
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Jump();
        print(isJumping);
        print(isSettingJump);

        animator.SetFloat("stateFloat", animState);
    }

    void Jump()//Old()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isSettingJump)
        {
            //moter.GetAgent().enabled = false;
            //GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent.<Rigidbody>().isKinematic = false;
            GetComponent<Rigidbody>().AddForce(0, jumpValue, 0);
            isSettingJump = true;
            StartCoroutine(SetJumpToTrue());//might not be needed. Will check later.
            animState = 2;
        }
    }

    IEnumerator SetJumpToTrue()
    {
        yield return new WaitForSeconds(.1f);
        isJumping = true;
        isSettingJump = false;
    }

    public void GroundCheck(string tagToCheck)
    {
        if(tagToCheck == "floor" && isJumping)
        {
            //print("grounded");
            //playerBody.GetComponent<Rigidbody>().isKinematic = true;
            isJumping = false;
            animState = 3;
        }
    }

    /*
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "floor" && isJumping)
        {
            moter.GetAgent().enabled = true;
            playerBody.GetComponent<Rigidbody>().isKinematic = true;
            isJumping = false;
        }
    }
    */

    //public



    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
        Rotation();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controllSpeed = ShiftSpeed;
        }
        else
        {
            controllSpeed = 10;
        }
        //if (CrossPlatformInputManager.GetButton("Vertical"))
        //{
            ZThrow = CrossPlatformInputManager.GetAxisRaw("Vertical");
            float zOffset = ZThrow * controllSpeed * Time.deltaTime;
            Vector3 zVectorOffset = zOffset * transform.forward * speedMultiplier;
            GetComponent<Rigidbody>().AddForce(zVectorOffset, ForceMode.VelocityChange);
            //GetComponent<Rigidbody>().velocity = zVectorOffset + new Vector3 (0, GetComponent<Rigidbody>().velocity.y, 0);
            print(zVectorOffset);
            //transform.localPosition = transform.localPosition + zVectorOffset;
        //}
        //if (CrossPlatformInputManager.GetButton("Horizontal"))
        //{
            XThrow = CrossPlatformInputManager.GetAxisRaw("Horizontal");
            float xOffser = XThrow * controllSpeed * Time.deltaTime;
            Vector3 xVectorOffset = xOffser * transform.right * speedMultiplier;
            GetComponent<Rigidbody>().AddForce(xVectorOffset, ForceMode.VelocityChange);
        //GetComponent<Rigidbody>().velocity = xVectorOffset + new Vector3(0, GetComponent<Rigidbody>().velocity.y, 0);
        //transform.localPosition = transform.localPosition + xVectorOffset;

        //}
        print(GetComponent<Rigidbody>().velocity);

        //adjust velocity
        //compare the absolute value of that with the max allowed velocity (horizontal, not including jumps/ falling etc):
        Vector3 horizontalMovement = new Vector3(GetComponent<Rigidbody>().velocity.x, 0, GetComponent<Rigidbody>().velocity.z);
        float velocityAbsoluteValue = horizontalMovement.magnitude;
        if (velocityAbsoluteValue > maxSpeed)
        {
            float percentage = maxSpeed / velocityAbsoluteValue;
            float velocityX = GetComponent<Rigidbody>().velocity.x * percentage;
            float velocityZ = GetComponent<Rigidbody>().velocity.z * percentage;
            float velocityY = GetComponent<Rigidbody>().velocity.y;
            GetComponent<Rigidbody>().velocity = new Vector3(velocityX, velocityY, velocityZ);
        }

        if (velocityAbsoluteValue > 0.05) //animation- check speed of animation.
        {
            if (!isJumping && !isSettingJump)//animState < 1.9)
            {
                animState = 1;
            }
        }
        else if (!isJumping && !isSettingJump)//animState < 1.9)
        {
            animState = 0;
        }



    }

    private void Rotation()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        float rotationX = mouseX * rotationSpeed;
        float rotationY = mouseY * rotationSpeed;

        xAxisClamp += rotationY;

        Vector3 rotationVec = rotationHelper.transform.rotation.eulerAngles;
        Vector3 rotationPlayer = transform.rotation.eulerAngles;

        rotationVec.x -= rotationY;
        //rotationVec.z = 0;
        rotationVec.y += rotationX;
        rotationPlayer.y += rotationX;
        //print(xAxisClamp);
        //print(rotationPlayer.x);
        if(xAxisClamp > 90)
        {
            xAxisClamp = 90;
            rotationVec.x = 270;
        }
        else if (xAxisClamp < -90)
        {
            xAxisClamp = -90;
            rotationVec.x = 90;
        }

        transform.rotation = Quaternion.Euler(rotationPlayer);
        rotationHelper.rotation = Quaternion.Euler(rotationVec);
    }
}