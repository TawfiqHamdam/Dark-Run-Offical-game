using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(PlayerMoter))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask MovementMask;
    [SerializeField] float rotationSpeed = 10f;
    [SerializeField] float controllSpeed = 1f;

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
        if (CrossPlatformInputManager.GetButton("Vertical"))
        {
            ZThrow = CrossPlatformInputManager.GetAxis("Vertical");
            float zOffset = ZThrow * controllSpeed * Time.deltaTime;
            //float rawPosZ = transform.localPosition.z + zOffset;
            Vector3 zVectorOffset = zOffset * transform.forward;//this seems wrong. Just put zOffset here
//let me try this it wont work
            transform.localPosition = transform.localPosition + zVectorOffset;
        }//it works for me let me see
    }
}// so lets fix the bugs right now i removed the virtical cause we dont need it
//I would suggest to keep it until we fix the bugs. I will remove it later
//I need to figure out why it suddenly became bugged okay ping me when you do