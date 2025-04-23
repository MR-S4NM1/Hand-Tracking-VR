using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Content.Interaction;

public class CraneManager : MonoBehaviour
{
    [SerializeField] protected XRJoystick _joystick;
    [SerializeField] protected XRSlider _slider;
    [SerializeField] protected XRGripButton _gripButtonA;
    [SerializeField] protected XRGripButton _gripButonB;
    [SerializeField] protected float _craneMovementSpeed;
    [SerializeField] protected Rigidbody _rb;
    protected Animator _animator;
    protected Vector3 _input;
    protected Vector3 _direction;
    protected bool _grabButtonPressed;
    protected bool _yButtonPressed;
    protected bool _grabObject;

    void Start(){
        _rb = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void OnEnable(){
        _joystick.onValueChangeX.AddListener(UpdateX);
        _joystick.onValueChangeY.AddListener(UpdateZ);
        _slider.onValueChange.AddListener(UpdateY);
    }

    private void OnDisable(){
        _joystick.onValueChangeX.RemoveListener(UpdateX);
        _joystick.onValueChangeY.RemoveListener(UpdateZ);
        _slider.onValueChange.RemoveListener(UpdateY);
    }

    public void ModifyButtonInfo()
    {
        if (!_grabButtonPressed)
        {
            _grabObject = true;
            _animator.SetTrigger("Grab");
        }
        else
        {
            _grabObject = false;
            _animator.SetTrigger("Release");
        }
        _grabButtonPressed = !_grabButtonPressed;
    }

    public void ModifyYAxisOfTheCrane()
    {
        if (!_yButtonPressed) UpdateY(-1.0f);
        else UpdateY(1.0f);

        _yButtonPressed = !_yButtonPressed;
    }

    private void FixedUpdate(){
        _direction = new Vector3(_input.x, _input.y, _input.z);

        _rb.MovePosition(_rb.position + _direction.normalized * 
            _craneMovementSpeed * Time.fixedDeltaTime);

        if(_rb.position.y <= 2.5f)
        {
            _rb.position = new Vector3(_rb.position.x, 2.55f, _rb.position.z);
            _yButtonPressed = true;
            UpdateY(0.0f);
        }
        else if(_rb.position.y >= 5.5f)
        {
            _rb.position = new Vector3(_rb.position.x, 5.45f, _rb.position.z);
            _yButtonPressed = false;
            UpdateY(0.0f);
        }
        else if(_rb.position.z <= 3.0f)
        {
            _rb.position = new Vector3(_rb.position.x, _rb.position.y, 3.05f);
        }
        else if (_rb.position.z >= 10.0f)
        {
            _rb.position = new Vector3(_rb.position.x, _rb.position.y, 9.95f);
        }
        else if (_rb.position.x <= 7.0f)
        {
            _rb.position = new Vector3(7.05f, _rb.position.y, _rb.position.z);
        }
        else if (_rb.position.x >= 13.0f)
        {
            _rb.position = new Vector3(12.95f, _rb.position.y, _rb.position.z);
        }
    }

    protected void UpdateX(float p_xMovement){
        _input.x = p_xMovement;
    }

    protected void UpdateY(float p_yMovement){
        _input.y = p_yMovement;
    }

    protected void UpdateZ(float p_zMovement){
        _input.z = p_zMovement;
    }

    public bool GetGrabObject 
    {  
        get { return _grabObject; }
    }
}
