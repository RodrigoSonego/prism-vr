using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Objective : MonoBehaviour
{
    [SerializeField] private Material activatedMaterial;
    [SerializeField] private Material fadedMaterial;
    [SerializeField] private MeshRenderer mainMesh;
    [Space]
    [SerializeField] private ParticleSystem darkParticles;
    [SerializeField] private ParticleSystem activatedParticles;
    [SerializeField] private ParticleSystem burstParticles;


    public Action OnFullyCharge;


    public static Objective instance;

    void Start()
    {
        instance = this;

        GoDark();
        burstParticles.Stop();
    }

    public void OnLaserHit()
    {
        mainMesh.material = activatedMaterial;

        darkParticles.Stop();
        activatedParticles.Play();

        StartCoroutine(WaitToFullyCharge());
    }

    public void OnLaserContactLost()
    {
        GoDark();
        StopAllCoroutines();
    }


    private void GoDark()
    {
        darkParticles.Play();
        activatedParticles.Stop();
        mainMesh.material = fadedMaterial;
    }

    IEnumerator WaitToFullyCharge()
    {
        yield return new WaitForSeconds(1.5f);

        if(OnFullyCharge != null)
        {
            OnFullyCharge();
            burstParticles.Play();
        }

        Level.instance.LoadNextLevel();
    }
}
