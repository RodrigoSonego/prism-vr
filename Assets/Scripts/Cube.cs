using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cube : MonoBehaviour, IPointerClickHandler
{
    const float TAU = Mathf.PI * 2;

    [SerializeField] private float distanceToPlayer = 10f;
    [SerializeField] private Camera playerCamera;

    private bool selected = false;

    private Cube spawnedCube;

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = !selected;
        print(Vector3.Distance(transform.position,playerCamera.transform.position));
        
        
        spawnedCube = Instantiate(this);

        print("dist do cubo: " + Vector3.Distance(spawnedCube.transform.position, transform.position));
    }

    //macaquisse
    void Update()
    {
        if(selected == false) { return; }

        UpdateCubePos();
        
    }

    private void UpdateCubePos()
    {
        if(spawnedCube is null) { return; }

        float angle = transform.rotation.eulerAngles.y * Mathf.Deg2Rad;
        spawnedCube.transform.position = new Vector3(Mathf.Sin(angle) * distanceToPlayer, transform.position.y, Mathf.Cos(angle) * distanceToPlayer);
        spawnedCube.transform.LookAt(transform);
    }
}
