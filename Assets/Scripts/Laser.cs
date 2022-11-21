using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
	const float TAU = 6.28318530f;

	[SerializeField] private Transform origin;
	[SerializeField] private LineRenderer lineRenderer;

	[SerializeField] private int maxPoints = 200;

	[SerializeField] private float slope = 0f;
	[SerializeField] private float height = 1.5f;

	void Start()
	{
		//RenderSpriral(origin.transform.position, 0, isClockwise: true);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(origin.position, 0.1f);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.05f);

		Gizmos.color = Color.white;

		lineRenderer.positionCount = maxPoints;
		RenderSpriral(origin.transform.position, 0, isClockwise: true);
	}

	void RenderSpriral(Vector3 origin, int initialIndex, bool isClockwise)
	{
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
		Vector2 originPos2D = new Vector2(origin.x, origin.z);

		int clockwiseModifier = isClockwise ? 1 : -1;

		float ang2 = Mathf.Atan2(originPos2D.x, originPos2D.y);

		float startAng = -ang2 + TAU * 0.25f;

		float dist = Vector2.Distance(pos2D, originPos2D);

		for (int i = 0; i < maxPoints - initialIndex; ++i)
		{
            float ang = startAng + (i) / 100f * TAU * clockwiseModifier;
			float unit = i * height / 100f;

			int positionIndex = i + initialIndex;

			// ax + b
			Vector3 offset = new(Mathf.Cos(ang) * dist, (unit * slope) + origin.y, Mathf.Sin(ang) * dist);
			Vector3 pointPosition = transform.position + offset;

			bool hasHit = false;
			if (positionIndex > 0)
			{
				Vector3 directionToCurrent = pointPosition - lineRenderer.GetPosition(positionIndex - 1);
				hasHit = Physics.Raycast(pointPosition, directionToCurrent.normalized, 0.15f, LayerMask.NameToLayer("Prism")); ;
			}

			lineRenderer.SetPosition(positionIndex, pointPosition);

			if (hasHit)
			{
				Vector3 lastPosition = lineRenderer.GetPosition(positionIndex - 1);
				RenderSpriral(lastPosition, positionIndex+1, !isClockwise);
				break;
			}
		}
	}
}
