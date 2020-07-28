using UnityEngine;

public class CameraFlollower : MonoBehaviour
{

    [SerializeField] Transform Taget;
    [SerializeField] Vector3 offset;

    [SerializeField] float SmothTime = 0.25f;

    void LateUpdate()
    {
        Vector3 WantPos = Taget.position + offset;
        Vector3 SmothedPos = Vector3.Lerp(transform.position);
        transform.position = WantPos;
    }
}
