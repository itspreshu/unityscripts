/*
    Smooth Camera Follow for 2D platformer Unity.
*/

using UnityEngine;

public class CameraFollow : MonoBehaviour {

    // The camera position relative to the target. 
    public Vector3 offset;

    // Smoothness of the camera movement.
    [Range(0f, 1f)]
    public float smoothness;
    public Transform target;
    public bool isLookingAt = false;
    
    private Vector3 desiredPosition;
    private Vector3 smoothPosition;

    private void LateUpdate()
    {
        desiredPosition = target.position + offset;
        smoothPosition = Vector3.Lerp(transform.position, desiredPosition, smoothness);
        transform.position = smoothPosition;
        if (isLookingAt)
        {
            transform.LookAt(target);
        }
    }
}
