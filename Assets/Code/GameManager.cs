using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance;
    [SerializeField] protected Material _material;
    [SerializeField] protected GameObject _finalPanel;
    #endregion

    #region Knobs
    [SerializeField] protected float _timeBeforeFinishing;
    #endregion

    #region RuntimeVariables
    Coroutine _timerCoroutine;
    int _score;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        if (instance == null) instance = this;
        _timerCoroutine = StartCoroutine(TimerCoroutine());
        _finalPanel.SetActive(false);
        _score = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.GetMask("Wood"))
        {
            switch (other.gameObject.GetComponent<ObjectCode>()._isChrome)
            {
                case true:
                    _score += 2;
                    break;
                case false:
                    _score++;
                    break;
            }
            other.gameObject.SetActive(false);
        }
    }
    #endregion

    public void ChangeMaterial(GameObject p_log)
    {
        p_log.GetComponent<MeshRenderer>().material = _material;
        p_log.GetComponent<ObjectCode>()._isChrome = true;
    }

    protected void ActivateFinalCanvas()
    {
        _finalPanel.SetActive(true);
    }

    protected IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(3.0f);

        while(_timeBeforeFinishing >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            _timeBeforeFinishing--;
        }

        if(_timeBeforeFinishing <= 0.0f)
        {
            ActivateFinalCanvas();
        }
    }
}
