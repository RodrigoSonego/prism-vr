using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
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

		RenderSpriral(origin.transform.position, 0, isClockwise: true);
	}

	void RenderSpriral(Vector3 origin, int initialIndex, bool isClockwise)
    {
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
		Vector2 originPos2D = new Vector2(origin.x, origin.z);

		int clockwiseModifier = isClockwise ? 1 : -1;

		float ang2 = Mathf.Atan2(originPos2D.x, originPos2D.y);
		print($"-> {ang2}");

		float startAng = -ang2 + TAU * 0.25f;

		float dist = Vector2.Distance(pos2D, originPos2D);

		int max = 100;
		for (int i = 0; i < max - initialIndex; i++)
		{
			var ang = startAng + (i / (float)(max - 1)) * TAU * clockwiseModifier;
			float unit = i / (float)max;

			// ax + b
			Vector3 offset = new Vector3(Mathf.Cos(ang) * dist, (unit * slope) + origin.y, Mathf.Sin(ang) * dist);

			var rotatedVector = Quaternion.AngleAxis(90, Vector3.down * clockwiseModifier) * offset;
			var hasHit = Physics.Raycast(transform.position + offset, rotatedVector.normalized * 0.15f, 0.15f, LayerMask.NameToLayer("Prism")); ;

			Gizmos.color = hasHit ? Color.red : Color.white;
			Gizmos.DrawSphere(transform.position + offset, 0.05f);

			Gizmos.color = hasHit ? Color.red : Color.blue;
			Gizmos.DrawRay(transform.position + offset, rotatedVector.normalized * 0.15f);

			if (hasHit)
            {
				RenderSpriral(transform.position + offset, i, !isClockwise);
				break;
            }
		}
	}
}
