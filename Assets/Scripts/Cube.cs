using System.Collections;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private bool isBeingGrabbed = false;

    public void ToggleGrabbed(Transform playerAnchor, Transform cameraAnchor, float radius)
    {
        if(isBeingGrabbed)
        {
            isBeingGrabbed = false;
            return;
        }

        isBeingGrabbed = true;
        StartCoroutine(GetDraggedByPlayer(playerAnchor, cameraAnchor, radius));
    }



    IEnumerator GetDraggedByPlayer(Transform playerAnchor, Transform cameraAnchor, float radius)
    {
        while(isBeingGrabbed) 
        {
            Vector3 newPosition = CalculateNewPosition(playerAnchor, cameraAnchor, radius);
            transform.position = newPosition;

            RotateTowardsPlayer(playerAnchor);

            yield return null;
        }
    }

    private Vector3 CalculateNewPosition(Transform playerTransform, Transform cameraTransform, float radius)
    {
        float cameraRotation = cameraTransform.localRotation.eulerAngles.x;
        float ang = -cameraRotation * Mathf.Deg2Rad;
        float height = Mathf.Tan(ang) * radius;

        Vector3 newPos = new Vector3(playerTransform.forward.x * radius, height, playerTransform.forward.z * radius);

        return newPos;
    }

    private void RotateTowardsPlayer(Transform playerTransform)
    {
        Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, angleToPlayer, 0));
    }
}
