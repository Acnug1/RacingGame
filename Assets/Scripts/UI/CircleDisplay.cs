using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CircleDisplay : MonoBehaviour
{
    [SerializeField] private GameFinishTrigger _gameFinishTrigger;
    [SerializeField] private TMP_Text _currentCircleText;
    [SerializeField] private TMP_Text _circlesNumberText;

    private void OnEnable()
    {
        _gameFinishTrigger.CircleChanged += OnCircleChanged;
    }

    private void OnDisable()
    {
        _gameFinishTrigger.CircleChanged -= OnCircleChanged;
    }

    private void Start()
    {
        _circlesNumberText.text = _gameFinishTrigger.CirclesNumber.ToString();
    }

    private void OnCircleChanged(int _currentCircle)
    {
        _currentCircleText.text = _currentCircle.ToString();
    }
}
