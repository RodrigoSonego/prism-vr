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
		if (Input.GetMouseButtonDown(0))
		{
			//print("desired: " + newCuboPos.y);
			//float height = radius * Mathf.Tan(xRotation);
			//print("height: "+height);

			CanGrabCube();
		}
        if (cubo)
        {
			cubo.transform.position = new Vector3(newCuboPos.x, 1, newCuboPos.z - 10);
		}
			
		
	}

	GameObject cubo;
	private bool CanGrabCube()
	{
		print("tenta");
		bool hasHit = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 150f);
		Debug.DrawRay(transform.position, transform.forward);
		if (hasHit)
        {
			cubo = hit.transform.gameObject;
			Debug.LogError("hitando o cubo");
			hit.transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
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
		
	}

#endif
}
