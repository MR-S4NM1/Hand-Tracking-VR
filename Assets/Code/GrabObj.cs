using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabObj : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Object")){
            other.transform.SetParent(transform, true);
            other.transform.position = transform.position;
            other.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
