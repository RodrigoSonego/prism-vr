using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
	const float TAU = 6.28318530f;

	[SerializeField] private Transform origin;

	[SerializeField] private float slope = 0f;
	[SerializeField] private float height = 1.5f;

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(origin.position, 0.1f);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.05f);

		Gizmos.color = Color.white;

		//float xScale = origin.localScale.x * 0.5f;

		Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
		Vector2 originPos2D = new Vector2(origin.position.x, origin.position.z);

		float ang2 = Mathf.Atan2(originPos2D.x, originPos2D.y);
		print($"-> {ang2}");

		float dist = Vector2.Distance(pos2D, originPos2D);

		float startAng = -ang2 + TAU * 0.25f;

		int max = 100;
		for (int i = 0; i < max; i++)
		{
			var ang = startAng + (i / (float)(max-1)) * TAU;
			float unit = i / (float) max;

			// ax + b
			Vector3 offset = new Vector3(Mathf.Cos(ang) * dist, (unit * slope) + origin.transform.position.y , Mathf.Sin(ang) * dist);

			var rotatedVector = Quaternion.AngleAxis(90, Vector3.down) * offset;
			var hasHit = Physics.Raycast(transform.position + offset, rotatedVector.normalized * 0.15f, 0.15f, LayerMask.NameToLayer("Prism")); ;

			Gizmos.color = hasHit ? Color.red : Color.white;
			Gizmos.DrawSphere(transform.position + offset, 0.05f);

			Gizmos.color = hasHit ? Color.red : Color.blue;
			Gizmos.DrawRay(transform.position + offset, rotatedVector.normalized * 0.15f);

		}
		
	}
}
