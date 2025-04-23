using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObj : MonoBehaviour
{
    [SerializeField] protected CraneManager _craneManager;

    private void OnTriggerStay(Collider other)
    {
        if (_craneManager.GetGrabObject)
        {
            if (other.CompareTag("Object"))
            {
                other.transform.SetParent(transform, true);
                other.transform.position = transform.position;
                other.GetComponent<Rigidbody>().isKinematic = true;
                other.GetComponent<Rigidbody>().useGravity = false;
            }
        }
        else
        {
            if (other.CompareTag("Object"))
            {
                other.transform.SetParent(null);
                other.GetComponent<Rigidbody>().isKinematic = false;
                other.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
