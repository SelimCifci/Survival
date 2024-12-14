using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Firepoint : MonoBehaviour
{
    public Transform firepoint;
    
    private void Update()
    {
        int layerMask = 1 << 7;

        layerMask = ~layerMask;

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 250, layerMask))
        {
            firepoint.LookAt(hit.point);
        }
        else { firepoint.LookAt(transform.position + transform.forward*250); }
    }
}
