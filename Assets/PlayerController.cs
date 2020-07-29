using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float XPos;
    [SerializeField] float NXPos;
    [SerializeField] float controllSpeed = 7.85f;
    [SerializeField] float JumpH = 4;
    [SerializeField] float JumpForse = 2.5f;
    [SerializeField] float Jumpspeed = 4f;

    private Vector3 Jump;
    private Rigidbody Rigg;
    private bool IsGrounded;
    private bool IsMoveing = true;


    void Start()
    {
        Jump = new Vector3(0f, JumpH, 0f);
        Rigg = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (IsMoveing)
        {
            Jumping();
            Movement();
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        PlayerIsGrounded(other);

    }

    private void PlayerIsGrounded(Collision other)
    {
        if (other.gameObject.tag == "floor")
        {
            IsGrounded = true;
            controllSpeed = 12f;
        }
    }

    private void Jumping()
    {
        if (Input.GetKeyDown(KeyCode.Space) && IsGrounded)
        {
            controllSpeed = Jumpspeed;
            IsGrounded = false;
            Rigg.AddForce(Jump * JumpForse, ForceMode.Impulse);

        }
    }

    private void Movement()
    {
        float Sideways = CrossPlatformInputManager.GetAxis("Horizontal");
        float XOffset = Sideways * controllSpeed * Time.deltaTime;

        float rawPosx = transform.localPosition.x + XOffset;
        float OffsetByX = Mathf.Clamp(rawPosx, NXPos, XPos);

        transform.localPosition = new Vector3(OffsetByX, transform.localPosition.y, transform.localPosition.z);
    }
}



