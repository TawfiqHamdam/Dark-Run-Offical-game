using UnityEngine;

public class CameraFlollower : MonoBehaviour
{

    [SerializeField] Transform Target;
    [SerializeField] Vector3 offset;

    [SerializeField] float SmothTime = 0.25f;

    void FixedUpdate()
    {
        Vector3 WantPos = Target.position + offset;
        Vector3 SmothedPos = Vector3.Lerp(transform.position, WantPos, SmothTime);
        transform.position = SmothedPos;

        transform.LookAt(Target);
    }
}
