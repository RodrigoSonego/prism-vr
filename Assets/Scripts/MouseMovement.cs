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


		Vector3 newCuboPos = transform.forward * radius;


		if (Input.GetMouseButton(0))
		{

			CanGrabCube();
			if (cubo != null)
			{
				float ang = -xRotation * Mathf.Deg2Rad;
				float h = Mathf.Tan(ang) * radius;

				cubo.transform.position = new Vector3(playerTransform.forward.x * radius, h, playerTransform.forward.z * radius);
			}
		}
			
		
	}

	GameObject cubo;
	private bool CanGrabCube()
	{
		bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 150f);
		Debug.DrawRay(transform.position, transform.forward);
		if (hasHit)
        {
			cubo = hit.transform.gameObject;

			hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;

			return hasHit;
		}
		else if (cubo != null)
        {
			cubo.GetComponent<MeshRenderer>().material.color = Color.red;
        }
		return hasHit;
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
