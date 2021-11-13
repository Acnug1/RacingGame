using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CarMover))]

public class AIController : MonoBehaviour
{
    [SerializeField] private Transform[] _waypoints; // определим массив waypoints

    private Transform _selfTransform; // позиция машины ИИ
    private CarMover _car; // компонент для движения машины
    private int _currentPoint; // текущая точка с которой мы работаем

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
        _car = GetComponent<CarMover>();
    }

    private void Update()
    {
        Transform current = _waypoints[_currentPoint]; // точка, которой ИИ будет двигаться
        Debug.DrawLine(_selfTransform.position, current.position, Color.red); // вектор от машины ИИ до его цели

        Vector3 direction = _selfTransform.position - current.position; // берем направление до точки, к которой нам нужно ехать
        float dot = Vector3.Dot(direction, _selfTransform.right); // находим скалярное произведение (угол между векторами) направления и нашего вектора вправо (|a| * |b| * cos a, где a - угол между векторами)
                                                                  // затем считаем косинус этого угла. Если угол будет равен примерно 90 градусам (cos 90 градусов = 0) машина поедет вперед. Это и будет наше искомое значение

        if (dot < 0) // если скалярное произведение меньше 0 градусов, то поворачиваем направо
        {
            ChangeAccelerate();
            _car.RotateRight(); // вращаем машину вправо
        }
        else if (dot > 0) // если скалярное произведение больше 0 градусов, то поворачиваем налево
        {
            ChangeAccelerate();
            _car.RotateLeft(); // вращаем машину влево
        }

        if (dot > -1f && dot < 1f) // если угол небольшой мы даем газу и пытаемся довернуться на ходу
        {
            _car.Accelerate();
        }

        if (Vector3.Distance(_selfTransform.position, current.position) < Random.Range(5f, 6f) && Vector3.Distance(_selfTransform.position, current.position) > Random.Range(2f, 3f)) // немного оттормаживаемся заранее на опеределенной дистанции до цели
        {
            _car.BrakeAndReverse();
        }

        if (Vector3.Distance(_selfTransform.position, current.position) < 2 && _waypoints.Length != 1) // если дистанция между позицией машины ИИ и текущей точкой, к которой стремится ИИ меньше некой величины
        {
            _currentPoint++; // переключаем точку, к которой стремится ИИ на следующую
            if (_currentPoint >= _waypoints.Length) // если мы вышли за набор доступных нам точек
            {
                _currentPoint = 0; // то обнуляем текущую точку в 0
            }
        }
    }

    private void ChangeAccelerate()
    {
        float force = _car.GetForce(); // получаем значение силы с которой движется машина каждый кадр

        if (force <= 0.01) // если мы не смотрим на точку и стоим на месте даем разгон
        {
            _car.Accelerate();
        } 
    }
}
