using System.Collections;
using UnityEngine;

public class Prism : MonoBehaviour
{
	[SerializeField] private Outline outline;



	private bool isBeingGrabbed = false;
	private Quaternion originalRotation;

	const float HeightMax = 10f;

	float radius;
	
	void Start()
	{
		originalRotation = transform.localRotation;
		outline.enabled = false;

		Vector3 pos = transform.position;
		radius = Vector2.Distance(new Vector2(pos.x, pos.z), Vector2.zero);
	}
	

	public void ToggleGrabbed(Transform playerAnchor, Transform cameraTransform)
	{
		if (isBeingGrabbed)
		{
			Drop();
			return;
		}

		outline.enabled = true;
		isBeingGrabbed = true;
		
		StartCoroutine(GetDraggedByPlayer(playerAnchor, cameraTransform));
	}

	public void DisablePrism()
    {
		isBeingGrabbed = false;
		Drop();

		GetComponent<Collider>().enabled = false;
    }


	private void Drop()
    {
		isBeingGrabbed = false;
		outline.enabled = false;
	}

	private IEnumerator GetDraggedByPlayer(Transform playerAnchor, Transform cameraTransform)
	{
		while (isBeingGrabbed) 
		{
			Vector3 newPosition = CalculateNewPosition(playerAnchor, cameraTransform);
			transform.position = newPosition;

			RotateTowardsPlayer(playerAnchor);

			yield return null;
		}
	}

	private Vector3 CalculateNewPosition(Transform playerTransform, Transform cameraTransform)
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

		Vector3 rotationVector = new Vector3(0, angleToPlayer + 90, 0);
		transform.localRotation = Quaternion.Euler(rotationVector);
	}
	
}
