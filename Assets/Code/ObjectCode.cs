using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectCode : MonoBehaviour
{
    [SerializeField] public bool _isGrabbed, _isChrome;

    private void Start() { 
        _isGrabbed = false;
        _isChrome = false;
    }
}
