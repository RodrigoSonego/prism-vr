using System.Collections;
using UnityEngine;

public class Objective : MonoBehaviour
{
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material fadedMaterial;
    [SerializeField] private MeshRenderer mainMesh;
    [Space]
    [SerializeField] private ParticleSystem darkParticles;
    [SerializeField] private ParticleSystem activatedParticles;

    private bool activated = false;

    void Start()
    {
        //StartCoroutine(BlinkLight());

        darkParticles.Play();
        activatedParticles.Stop();
    }

    public void ActivateWithLaser()
    {
        mainMesh.material = activatedMaterial;

        darkParticles.Stop();
        activatedParticles.Play();

        //StopAllCoroutines();
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
