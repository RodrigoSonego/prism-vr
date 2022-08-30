using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	const float TAU = 6.28318530f;

	[SerializeField] private Transform cylinder;

	[SerializeField] private float slope = 0f;
	[SerializeField] private float height = 1.5f;
	





	void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.05f);

		Gizmos.color = Color.white;

		float xScale = cylinder.localScale.x * 0.5f;


		int max = 100;
		for (int i = 0; i < max; i++)
		{
			var ang = (i / (float)(max-1)) * TAU;
			// ax + b
			Vector3 offset = new Vector3(Mathf.Cos(ang) * xScale, i * slope / max, Mathf.Sin(ang) * xScale);
			Gizmos.DrawSphere(transform.position + offset, 0.05f);
		}
		
	}
}
