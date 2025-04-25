using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;

public enum States
{
    NONE,
    NOT_SHOOTING,
    SHOOTING
}

[RequireComponent(typeof(LineRenderer))]
public class LaserCode : MonoBehaviour
{
    #region References
    [SerializeField] protected Transform _laserOrigin;
    [SerializeField] protected Transform _laserDirRef;
    [SerializeField] protected LineRenderer _laser;
    #endregion

    #region Knobs
    [SerializeField] protected float _laserDuration;
    [SerializeField] protected float _laserDistanceRange;
    #endregion

    #region RuntimeVariables
    [SerializeField] protected States _state;
    [SerializeField] protected States _previousState;
    [SerializeField] protected Vector3 _laserDirection;
    RaycastHit _raycastHit;
    Coroutine _coroutine;
    #endregion
    protected void Start()
    {
        _state = States.NOT_SHOOTING;
        _laserDirection = (_laserDirRef.position - _laserOrigin.position).normalized;
    }

    protected void ChangeState(States p_state)
    {
        _previousState = _state;

        if(p_state != _previousState)
        {
            switch (p_state)
            {
                case States.SHOOTING:
                    ShootLaser();
                    break;
                case States.NOT_SHOOTING:
                    _state = States.NOT_SHOOTING;
                    break;
            }
        }
    }

    public void StartShootingLaser()
    {
        ChangeState(States.SHOOTING);
    }

    protected void ShootLaser()
    {
        _state = States.SHOOTING;
        _coroutine = StartCoroutine(ShootingCoolDown());
    }

    protected IEnumerator ShootingCoolDown()
    {
        _laser.SetPosition(0, _laserOrigin.position);

        if (Physics.Raycast(_laserOrigin.position, _laserDirection,
            out _raycastHit, LayerMask.GetMask("Wood")))
        {
            _laser.SetPosition(1, _raycastHit.point);
            //TODO: Make that the Game Manager changes the hitObjectsMaterial
            GameManager.instance.ChangeMaterial(_raycastHit.collider.gameObject);
        }
        else
        {
            _laser.SetPosition(1, _laserOrigin.position +
                (_laserDirection * _laserDistanceRange));
        }
        _laser.enabled = true;
        yield return new WaitForSeconds(_laserDuration);
        _laser.enabled = false;
        ChangeState(States.NOT_SHOOTING);
    }
}
