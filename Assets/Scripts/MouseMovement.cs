using UnityEngine;

public class MouseMovement : MonoBehaviour
{
	[SerializeField] private Transform playerTransform;
	const float MouseSensitivity = 300f;

	
	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float timeSpeed = MouseSensitivity * Time.deltaTime;
		Vector3 rotation = timeSpeed * new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0);

		Quaternion playerRotation = Quaternion.Euler(playerTransform.eulerAngles + rotation);
		playerRotation.x = Mathf.Clamp(playerRotation.x, -90, 90);

		playerTransform.rotation = playerRotation;


		if (Input.GetMouseButtonDown(0))
		{
			bool couldFindCube = TryToGrabCube(out Prism cube);
			if (couldFindCube)
			{
				cube.ToggleGrabbed(playerTransform);
			}
		}
		
	}

	private bool TryToGrabCube(out Prism foundCube)
	{
		Vector3 forward = transform.forward;
		Vector3 pos = transform.position;
		bool hasHit = Physics.Raycast(pos, forward, out RaycastHit hit, 150f);
		Debug.DrawRay(pos, forward);
		if (hasHit)
        {
			foundCube = hit.transform.gameObject.GetComponent<Prism>();
			return foundCube != null;
		}

		foundCube = null;
		return false;
	}

}
