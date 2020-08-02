using UnityEngine;

[RequireComponent(typeof(PlayerMoter))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask MovementMask;

    Camera cam;
    PlayerMoter moter;

    void Start()
    {
        cam = Camera.main;
        moter = GetComponent<PlayerMoter>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 1000, MovementMask))
            {
                moter.MoveToPoint(hit.point);
            }
        }
    }

}
