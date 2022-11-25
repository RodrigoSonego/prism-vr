using System.Collections;
using UnityEngine;

public class Prism : MonoBehaviour
{
    [SerializeField] private Outline outline;

    private bool isBeingGrabbed = false;
    private Quaternion originalRotation;

    void Start()
    {
        originalRotation = transform.localRotation;

        if (outline is null) { return; }

        outline.enabled = false;
    }

    public void ToggleGrabbed(Transform playerAnchor, Transform cameraAnchor)
    {
        if (isBeingGrabbed)
        {
            isBeingGrabbed = false;
            outline.enabled = false;
            return;
        }

        Vector2 player2dPos = new(playerAnchor.position.x, playerAnchor.position.z);
        Vector2 prism2dPos = new(transform.position.x, transform.position.z);

        float radius = Vector2.Distance(player2dPos, prism2dPos);

        outline.enabled = true;

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
        Vector3 directionToPlayer = (playerTransform.forward - transform.position).normalized;
        float angleToPlayer = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;

        Vector3 rotationVector = new Vector3(0, angleToPlayer, 0);
        transform.localRotation = Quaternion.Euler(rotationVector + originalRotation.eulerAngles);
    }
}
