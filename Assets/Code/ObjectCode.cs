using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCode : MonoBehaviour
{
    [SerializeField] public bool _isGrabbed, _isChrome;
    [SerializeField] protected Rigidbody _rb;
    [SerializeField] protected CapsuleCollider[] _capsuleColliders;

    private void Start() {
        _rb = GetComponent<Rigidbody>();
        _isGrabbed = false;
        _isChrome = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Container"))
        {
            gameObject.transform.SetParent(null, true);
            _isGrabbed = false;
            foreach (CapsuleCollider collision in _capsuleColliders) { collision.enabled = false; }
            GameManager.instance.AddScore(_isChrome);
            gameObject.SetActive(false);
        }
    }
}
