using UnityEngine;

public class MouseMovement : MonoBehaviour
{
	public float mouseSensitivity = 100f;

	public Transform playerTransform;

	[SerializeField] float radius = 10f;
	
	private float xRotation = 0f;


	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
	}

	void Update()
	{
		float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
		float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

		xRotation -= mouseY;
		xRotation = Mathf.Clamp(xRotation, -90, 90);

		transform.localRotation = Quaternion.Euler(xRotation, 0, 0);
		playerTransform.Rotate(Vector3.up * mouseX);

		if (Input.GetMouseButtonDown(0))
		{
			bool couldFindCube = TryToGrabCube(out Cube cube);
			if (couldFindCube)
			{
				cube.ToggleGrabbed(playerTransform, transform, radius);
			}
		}
			
		
	}

	private bool TryToGrabCube(out Cube foundCube)
	{
		bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 150f);
		Debug.DrawRay(transform.position, transform.forward);
		if (hasHit)
        {
			foundCube = hit.transform.gameObject.GetComponent<Cube>();
			// pode dar null nessa merda, tratar sa merda
			//cubo = cube;

			hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;

			//foundCube = cube;
			return foundCube != null;
		}

		foundCube = null;
		return false;
	}

}
