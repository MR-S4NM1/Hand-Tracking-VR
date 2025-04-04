using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CraneManager : MonoBehaviour
{
    [SerializeField] protected XRJoystick _joystick;
    [SerializeField] protected XRSlider _slider;
    [SerializeField] protected float _craneMovementSpeed;
    [SerializeField] protected Rigidbody _rb;
    protected Vector3 _input;
    Vector3 _direction;

    void Start(){
        _rb = GetComponent<Rigidbody>();
    }

    private void OnEnable(){
        _joystick.onValueChangeX.AddListener(UpdateX);
        _joystick.onValueChangeX.AddListener(UpdateZ);
        _slider.onValueChange.AddListener(UpdateY);
    }

    private void OnDisable(){
        _joystick.onValueChangeX.RemoveListener(UpdateX);
        _joystick.onValueChangeX.RemoveListener(UpdateZ);
        _slider.onValueChange.RemoveListener(UpdateY);
    }

    private void FixedUpdate(){


        _direction = new Vector3(_input.x, _input.y, _input.z);
        _rb.MovePosition(_rb.position + _direction.normalized * 
            _craneMovementSpeed * Time.fixedDeltaTime);
    }

    protected void UpdateX(float p_xMovement){
        _input.x = p_xMovement;
    }

    protected void UpdateY(float p_yMovement)
    {
        _input.y = p_yMovement;
    }

    protected void UpdateZ(float p_zMovement){
        _input.z = p_zMovement;
    }
}
