using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMoter))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask MovementMask;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float controllSpeed = 1f;
    [SerializeField] float ShiftSpeed = 15f;

    [SerializeField] Transform rotationHelper;

    Camera cam;
    PlayerMoter moter;

    float ZThrow, YThrow, XThrow;
    float xAxisClamp = 0;

    void Start()
    {
        cam = Camera.main;
        moter = GetComponent<PlayerMoter>();
    }

    private void Update()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

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