using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material fadedMaterial;
    [SerializeField] private MeshRenderer mainMesh;

    private bool activated = false;

    public void ActivateLight()
    {
        mainMesh.material = activatedMaterial;
    }

    public void DeactivateLight()
    {
        mainMesh.material = fadedMaterial;
    }
}
