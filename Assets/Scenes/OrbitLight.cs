using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitLight : MonoBehaviour
{
    public Transform pivot;
    public float sensitivity = 3f;
    public float distance = 5f;

    float xRot = 0f;
    float yRot = 0f;

    Quaternion adjustmentRotation;
    Quaternion rotation;

    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Set distance to the current distance of the target
            distance = Vector3.Distance(transform.position, pivot.position);

            // Set the x and y rotation to the new rotation relative to the pivot
            Vector3 pivotToHere = transform.position - pivot.position;
            Vector3 tempVec = Vector3.ProjectOnPlane(pivotToHere, Vector3.up);
            if (pivotToHere.x > 0f)
                yRot = Vector3.Angle(Vector3.forward, tempVec) + 180f;
            else
                yRot = -Vector3.Angle(Vector3.forward, tempVec) + 180f;

            if (pivotToHere.y > 0f)
                xRot = Vector3.Angle(tempVec, pivotToHere);
            else
                xRot = -Vector3.Angle(tempVec, pivotToHere);

            rotation = Quaternion.Euler(xRot, yRot, 0);
            adjustmentRotation = Quaternion.FromToRotation(rotation * Vector3.forward, transform.forward);

        }

        if (Input.GetMouseButton(0))
        {
            xRot -= Input.GetAxis("Mouse Y") * Time.deltaTime * sensitivity;
            yRot += Input.GetAxis("Mouse X") * Time.deltaTime * sensitivity;

            rotation = Quaternion.Euler(xRot, yRot, 0);

            transform.position = pivot.position - rotation * (Vector3.forward * distance);

            transform.rotation = Quaternion.LookRotation(adjustmentRotation * (rotation * Vector3.forward), Vector3.up);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
