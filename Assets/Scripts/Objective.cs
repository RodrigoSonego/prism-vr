using System.Collections;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material fadedMaterial;
    [SerializeField] private MeshRenderer mainMesh;

    private bool activated = false;

    void Start()
    {
        StartCoroutine(BlinkLight());
    }

    public void ActivateWithLaser()
    {
        mainMesh.material = activatedMaterial;

        StopAllCoroutines();
    }

    IEnumerator BlinkLight()
    {
        while (true)
        {
            mainMesh.material = activated ? fadedMaterial : activatedMaterial;

            activated = !activated;

            yield return new WaitForSeconds(1.5f);
        }
    }
}
