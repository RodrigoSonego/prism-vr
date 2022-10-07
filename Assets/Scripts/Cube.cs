using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour
{
    private bool isBeingGrabbed = false;

    public void GetGrabbed(Transform playerAnchor, Transform cameraAnchor, float radius)
    {
        isBeingGrabbed = true;
        StartCoroutine(GetDraggedByPlayer(playerAnchor, cameraAnchor, radius));
    }

    IEnumerator GetDraggedByPlayer(Transform playerAnchor, Transform cameraAnchor, float radius)
    {
        while(isBeingGrabbed) 
        {
            float cameraRotation = cameraAnchor.localRotation.eulerAngles.x;
            float ang = -cameraRotation * Mathf.Deg2Rad;
            float height = Mathf.Tan(ang) * radius;

            transform.position = new Vector3(playerAnchor.forward.x * radius, height, playerAnchor.forward.z * radius);

            Vector3 directionToPlayer = (playerAnchor.position - transform.position).normalized;
            float angleToPlayer = Mathf.Atan2(directionToPlayer.x, directionToPlayer.z) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(new Vector3(0, angleToPlayer, 0));

            yield return null;
        }
    }
}
