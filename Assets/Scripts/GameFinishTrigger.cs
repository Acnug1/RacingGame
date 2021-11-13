using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameFinishTrigger : MonoBehaviour
{
    [SerializeField] private UnityEvent _finished; // создаем событие _finished
    [SerializeField] private int _circlesNumber;

    private Endpoint[] _points;
    private int _currentCircle = 1;

    public int CirclesNumber => _circlesNumber;

    public event UnityAction<int> CircleChanged;

    private void OnValidate()
    {
        if (_circlesNumber < 1)
            _circlesNumber = 1;
    }

    private void OnEnable()
    {
        _points = gameObject.GetComponentsInChildren<Endpoint>(); // находим все компоненты Endpoint дочерних к нам объектов и записываем эти объекты в массив

        foreach (var point in _points)
        {
            point.Reached += OnEndPointReached;
        }
    }

    private void OnDisable()
    {
        foreach (var point in _points)
        {
            point.Reached -= OnEndPointReached;
        }
    }

    private void Start()
    {
        CircleChanged?.Invoke(_currentCircle); // вызываем событие 1 раз в начале, для отображение информации о 1-м круге
    }

    private void OnEndPointReached()
    {
        foreach (var point in _points)
            if (point.IsReached == false) // пока не достигнем все endpoint на круге не выполняем программу дальше
                return;

        _currentCircle++; // считаем текущий круг

        if (_currentCircle > _circlesNumber) // если текущий круг больше, чем общее количество кругов
            _finished?.Invoke(); // вызываем событие по завершению заезда
        else
        {
            foreach (var point in _points)
                point.ReachNextLap(); // когда достигнуты все поинты на круге сбрасываем флаг у каждого endpoint

            CircleChanged?.Invoke(_currentCircle); // иначе вызываем событие по отображению оставшихся кругов
        }
    }
}
