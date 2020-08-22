using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMoter))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask MovementMask;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float controllSpeed = 20f;

    Camera cam;
    PlayerMoter moter;

    float ZThrow, YThrow;

    void Start()
    {
        cam = Camera.main;
        moter = GetComponent<PlayerMoter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();

    }

    private void Movement()
    {
        if (CrossPlatformInputManager.GetButton("Horizontal"))
        {
            YThrow = CrossPlatformInputManager.GetAxis("Horizontal");

            float YOffset = YThrow * rotationSpeed * Time.deltaTime;

            transform.Rotate(0, YOffset, 0);

<<<<<<< HEAD
            if (CrossPlatformInputManager.GetButton("Vertical"))
            {
                ZThrow = CrossPlatformInputManager.GetAxis("Vertical");
                float zOffset = ZThrow * controllSpeed * Time.deltaTime;
=======
        }

        if (CrossPlatformInputManager.GetButton("Vertical"))
        {
            ZThrow = CrossPlatformInputManager.GetAxis("Vertical");
            float zOffset = ZThrow * controllSpeed * Time.deltaTime;
>>>>>>> 16f1bdb2d08fc4769d0bfe0db7b5d1e48eea4f3a

                float rawPosZ = transform.localPosition.z + zOffset;

                Vector3 zVectorOffset = ZThrow * transform.forward;

                transform.localPosition = transform.localPosition + zVectorOffset;
            }
        }
    }
}
