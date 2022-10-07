using UnityEngine;

public class MouseMovement : MonoBehaviour
{
	public float mouseSensitivity = 100f;

	public Transform playerTransform;

	[SerializeField] float radius = 10f;
	
	private float xRotation = 0f;

	Cube cubo;

	void Start()
	{
		Cursor.lockState = CursorLockMode.Locked;
		print(transform.forward * radius);
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

			Cube cube = TryToGrabCube();
			if (cube != null)
			{
				cube.ToggleGrabbed(playerTransform, transform, radius);
			}
		}
			
		
	}

	private Cube TryToGrabCube()
	{
		bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 150f);
		Debug.DrawRay(transform.position, transform.forward);
		if (hasHit)
        {
			Cube cube = hit.transform.gameObject.GetComponent<Cube>();
			cubo = cube;

			hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;

			return cube;
		}

		return null;
	}



#if UNITY_EDITOR_WIN
	private void OnDrawGizmos()
    {
		//Vector3 direction = transform.forward;
		//bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 15f, 1);
		Gizmos.color = Color.blue;
		Gizmos.DrawLine(transform.position, transform.position + transform.forward * 10f);

		Gizmos.color = Color.yellow;
		Gizmos.DrawLine(transform.position, playerTransform.forward * 10);
			
		if(cubo != null)
        {
			Gizmos.color = Color.red;
			Gizmos.DrawLine(transform.position, cubo.transform.position);

			Gizmos.color = Color.green;
			Gizmos.DrawLine(playerTransform.forward * 10, cubo.transform.position);
		}

	}

#endif
}
