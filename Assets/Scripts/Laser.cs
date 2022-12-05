using UnityEngine;


[ExecuteAlways]
public class Laser : MonoBehaviour
{
	const float TAU = 6.28318530f;

	[Header("External")]
	[SerializeField] private Transform origin;
	
	[Space(8)]
	[Header("Internal")]
	[SerializeField] private LineRenderer lineRenderer;
	[SerializeField] private LayerMask collidableLayers;

	[Range(0, 10_000)] [SerializeField] private int maxPoints = 200;

	[SerializeField] private float slope = 0f;
	[SerializeField] private float height = 1.5f;


	void Update()
	{
		lineRenderer.positionCount = maxPoints;
		RenderSpriral(origin.transform.position, 0, isClockwise: true);
	}

	void OnDrawGizmos()
	{
		Gizmos.color = Color.cyan;
		Gizmos.DrawSphere(origin.position, 0.1f);

		Gizmos.color = Color.red;
		Gizmos.DrawSphere(transform.position, 0.05f);

		Gizmos.color = Color.white;
	}


	void RenderSpriral(Vector3 origin, int initialIndex, bool isClockwise)
	{
		Vector2 pos2D = new Vector2(transform.position.x, transform.position.z);
		Vector2 originPos2D = new Vector2(origin.x, origin.z);

		int clockwiseModifier = isClockwise ? 1 : -1;

		float ang2 = Mathf.Atan2(originPos2D.x, originPos2D.y);

		float startAng = -ang2 + TAU * 0.25f;

		float dist = Vector2.Distance(pos2D, originPos2D);

		for (int i = 0; i < lineRenderer.positionCount - initialIndex; ++i)
		{
			float ang = startAng + (i) / 100f * TAU * clockwiseModifier;
			float unit = i * height / 100f;

			int positionIndex = i + initialIndex;

			// ax + b
			Vector3 offset = new(Mathf.Cos(ang) * dist, (unit * slope) + origin.y, Mathf.Sin(ang) * dist);
			Vector3 pointPosition = transform.position + offset;

			bool hasHit = false;
			RaycastHit hit = new();
			if (positionIndex > 0)
			{
				Vector3 directionToCurrent = pointPosition - lineRenderer.GetPosition(positionIndex - 1);

				hasHit = Physics.Raycast(pointPosition, directionToCurrent.normalized, out hit, 0.15f, collidableLayers);
			}

			lineRenderer.SetPosition(positionIndex, pointPosition);

			if (hasHit == false) { continue; }

			lineRenderer.positionCount++;
			lineRenderer.SetPosition(positionIndex + 1, hit.point);

			if (HasHitPrism(hit))
			{
				RenderSpriral(pointPosition, positionIndex + 2, !isClockwise);
				break;
			}

			if (HasHitAbsorb(hit))
			{
				lineRenderer.positionCount = positionIndex + 2;
				break;
			}

			if (HasHitObjective(hit))
            {
				Objective objective = hit.transform.gameObject.GetComponent<Objective>();

				objective.ActivateLight();

				lineRenderer.positionCount = positionIndex + 2;
				break;
            }
		}
	}

	bool HasHitPrism(RaycastHit hit)
	{
		return hit.transform.gameObject.layer == LayerMask.NameToLayer("Prism");
	}	

	bool HasHitAbsorb(RaycastHit hit)
	{
	   return hit.transform.gameObject.layer == LayerMask.NameToLayer("Absorb");
	}

	bool HasHitObjective(RaycastHit hit)
    {
		return hit.transform.gameObject.layer == LayerMask.NameToLayer("Objective");
    }

}
