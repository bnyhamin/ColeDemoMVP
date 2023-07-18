using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public float sensitivity = 3f;
    public float maxYAngle = 80f;
    public float minYAngle = -80f;
    public float distance = 5f;

    private float rotationX = 0f;
    private float collisionOffset = 0.2f;

    void LateUpdate()
    {
        if (target == null)
            return;
        Vector3 currentRotation = transform.eulerAngles;


        float desiredRotationY = target.eulerAngles.y;
        rotationX -= Input.GetAxis("Mouse Y") * sensitivity;
        rotationX = Mathf.Clamp(rotationX, minYAngle, maxYAngle);

        Quaternion desiredRotation = Quaternion.Euler(rotationX, desiredRotationY, 0f);
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotation, Time.deltaTime * sensitivity);

        Vector3 desiredPosition = target.position - transform.forward * distance;

        RaycastHit hit;
        if (Physics.Linecast(target.position, desiredPosition, out hit))
        {
            Vector3 adjustedPosition = hit.point + hit.normal * collisionOffset;
            transform.position = adjustedPosition;
        }
        else
        {
            transform.position = desiredPosition;
        }
    }
}
