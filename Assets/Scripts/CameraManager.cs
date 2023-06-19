using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public GameObject playerObj;
    public float smoothTime = 0.3F;
    public float yOffset;
    public float zOffset;

    private Vector3 velocity = Vector3.zero;
    private float posX;

    //camera follow player object
    void Update()
    {
        Vector3 targetPosition = new Vector3(posX, yOffset, playerObj.transform.position.z - zOffset);

        if (targetPosition.z > transform.position.z)
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }


    
}
