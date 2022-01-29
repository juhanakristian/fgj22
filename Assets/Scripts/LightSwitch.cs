using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public GameObject SwitchObject;
    private float switchPosition = 0f;
    private Camera mainCamera;
    private Plane draggingPlane;
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        SwitchObject.transform.localRotation = Quaternion.Euler(switchPosition * -180f, 0, 0);
    }

    void OnMouseDown() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        draggingPlane = new Plane(mainCamera.transform.forward, transform.position);

        float distance;
        draggingPlane.Raycast(ray, out distance);
        offset = ray.GetPoint(distance);
    }

    void OnMouseDrag() {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        float distance;
        draggingPlane.Raycast(ray, out distance);

        Vector3 pos = ray.GetPoint(distance);
        float y = pos.y - offset.y;

        switchPosition = Mathf.Clamp(switchPosition + -y, 0f, 1f);

        offset = pos;
    }

    
}
