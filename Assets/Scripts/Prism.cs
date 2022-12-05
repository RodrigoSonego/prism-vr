using System.Collections;
using UnityEngine;
// ReSharper disable Unity.InefficientPropertyAccess

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
	

	public void ToggleGrabbed(Transform playerAnchor)
	{
		if (isBeingGrabbed)
		{
			isBeingGrabbed = false;
			outline.enabled = false;
			return;
		}

		outline.enabled = true;
		isBeingGrabbed = true;
		
		StartCoroutine(GetDraggedByPlayer(playerAnchor));
	}

	private IEnumerator GetDraggedByPlayer(Transform playerAnchor)
	{
		while (isBeingGrabbed) 
		{
			Vector3 newPosition = CalculateNewPosition(playerAnchor);
			transform.position = newPosition;

			RotateTowardsPlayer(playerAnchor);

			yield return null;
		}
	}

	private Vector3 CalculateNewPosition(Transform playerTransform)
	{
		float ang = playerTransform.localRotation.eulerAngles.x * Mathf.Deg2Rad;
		float height = Mathf.Clamp(Mathf.Tan(ang) * radius, -HeightMax, HeightMax);

		Vector3 forward = playerTransform.forward;
		Vector3 newPos = new Vector3(forward.x * radius, -height, forward.z * radius);

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
