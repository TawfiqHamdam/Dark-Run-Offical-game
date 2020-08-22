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
        }
        if (CrossPlatformInputManager.GetButton("Vertical"))
        {
            ZThrow = CrossPlatformInputManager.GetAxis("Vertical");
            float zOffset = ZThrow * controllSpeed * Time.deltaTime;
            float rawPosZ = transform.localPosition.z + zOffset;
            Vector3 zVectorOffset = rawPosZ * transform.forward;

            transform.localPosition = transform.localPosition + zVectorOffset;
        }
    }
}// 
//ok so, it was working fine like 1 hour ago when I tested it. What changed now?