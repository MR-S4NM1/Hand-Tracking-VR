using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    #region References
    public static GameManager instance;
    [SerializeField] protected Material _material;
    [SerializeField] protected GameObject _finalPanel;
    [SerializeField] protected GameObject _gamePanel;
    [SerializeField] protected TextMeshProUGUI _scoreText;
    [SerializeField] protected TextMeshProUGUI _timerText;
    [SerializeField] protected TextMeshProUGUI _finalText;
    #endregion

    #region Knobs
    [SerializeField] protected float _timeBeforeFinishing;
    #endregion

    #region RuntimeVariables
    Coroutine _timerCoroutine;
    [SerializeField] int _score;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        if (instance == null) instance = this;
        _timerCoroutine = StartCoroutine(TimerCoroutine());
        _finalPanel.SetActive(false);
        _gamePanel.SetActive(false);
        _scoreText.text = "Score: " + _score.ToString("0");
        _score = 0;
    }

    public void AddScore(bool isChromeType)
    {
        switch (isChromeType)
        {
            case true:
                _score += 2;
                _scoreText.text = "Score: " + _score;
                //print(_score);
                break;
            case false:
                _score++;
                _scoreText.text = "Score: " + _score;
                //print(_score);
                break;
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
        _gamePanel.SetActive(false);
        _finalPanel.SetActive(true);
        _finalText.text = $"Your score was: {_score}";
    }

    protected IEnumerator TimerCoroutine()
    {
        yield return new WaitForSeconds(3.0f);
        _gamePanel.SetActive(true);
        _timerText.text = "Time left: " + _timeBeforeFinishing.ToString("0");
        _scoreText.text = "Score: " + _score;

        while (_timeBeforeFinishing >= 0)
        {
            yield return new WaitForSeconds(1.0f);
            _timeBeforeFinishing--;
            _timerText.text = "Time left: " + _timeBeforeFinishing.ToString("0");
        }

        if(_timeBeforeFinishing <= 0.0f)
        {
            _timeBeforeFinishing = 0.0f;
            ActivateFinalCanvas();
        }
    }
}
