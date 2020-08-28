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
    [SerializeField] GameObject playerBody;

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
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Jump();
    }

    void Jump()//Old()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !isJumping && !isSettingJump)
        {

            //moter.GetAgent().enabled = false;
            playerBody.GetComponent<Rigidbody>().isKinematic = false;
            //GetComponent.<Rigidbody>().isKinematic = false;
            playerBody.GetComponent<Rigidbody>().AddForce(0, 400, 0);
            isSettingJump = true;
            StartCoroutine(SetJumpToTrue());

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
            print("grounded");
            playerBody.GetComponent<Rigidbody>().isKinematic = true;
            isJumping = false;
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
        if (CrossPlatformInputManager.GetButton("Vertical"))
        {
            ZThrow = CrossPlatformInputManager.GetAxis("Vertical");
            float zOffset = ZThrow * controllSpeed * Time.deltaTime;
            Vector3 zVectorOffset = zOffset * transform.forward;
            transform.localPosition = transform.localPosition + zVectorOffset;
        }
        else if (CrossPlatformInputManager.GetButton("Horizontal"))
        {
            XThrow = CrossPlatformInputManager.GetAxis("Horizontal");
            float xOffser = XThrow * controllSpeed * Time.deltaTime;
            Vector3 xVectorOffset = xOffser * transform.right;
            transform.localPosition = transform.localPosition + xVectorOffset;

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