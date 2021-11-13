using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CarMover : MonoBehaviour
{
    [SerializeField] private Tilemap _map;
    [SerializeField] private TileBase _groundTile;
    [SerializeField] private float _maxCarSpeed;

    private Transform _selfTransform; // трансформ нашей машины
    private Vector3 _force; // ускорение машины
    private bool _isAccelerated;
    private bool _isReversed;

    private void Start()
    {
        _selfTransform = GetComponent<Transform>();
    }

    public float GetForce() // получить скорость машины для работы ИИ
    {
        return _force.magnitude; // возвращаем длину вектора
    }

    public void Accelerate()
    {
        if (Math.Abs(_force.x) < _maxCarSpeed && Math.Abs(_force.y) < _maxCarSpeed) // если зажата клавиша пробел и не превышена скорость (на максимальной скорости автоматически снижается скорость и делается поворот машины через кадр, после чего скорость снова увеличивается и так по кругу)
        {
            _force += (_selfTransform.up * Time.deltaTime) * 0.1f; // возвращаем нормализованный вектор движения вперед в глобальном пространстве в локальной системе координат нашей машины и прибавляем его к нашей силе движения (домножаем все это для снижения величины ускорения)
            _isAccelerated = true; // каждый кадр, пока мы зажимаем клавишу ускорения _isAccelerated = true
        }
        _isReversed = false; // когда машина ускоряется, она не движется назад
    }

    public void BrakeAndReverse()
    {
        if (_force == Vector3.zero || _isReversed) // если машина стоит на месте или едет назад
        {
            _force = (-_selfTransform.up * Time.deltaTime); // меняем силу на противоположную и двигаем на фиксированную скорость в обратном направлении
            _isAccelerated = true;
            _isReversed = true;
        }
        else
        if (_force != Vector3.zero && !_isReversed) // если машина в движении и не движется назад
        {
            _force = Vector3.Lerp(_force, Vector3.zero, Time.deltaTime * 2); // делаем торможение
        }
    }

    public void RotateRight()
    {
        if (_force.magnitude > 0.01) // если сила, с которой движется машина больше по модулю, чем некий эпсилон (проверяем, чтобы машина не могла крутиться на месте)
        {
            if (!_isReversed)
                _selfTransform.Rotate(0, 0, -1.5f); // поворачиваемся на 1.5 градуса вправо
            else
                _selfTransform.Rotate(0, 0, 1.5f); // поворачиваемся на 1.5 градуса вправо (при движении машины задним ходом)
        }
    }

    public void RotateLeft()
    {
        if (_force.magnitude > 0.01)
        {
            if (!_isReversed)
                _selfTransform.Rotate(0, 0, 1.5f); // поворачиваемся на 1.5 градуса влево
            else
                _selfTransform.Rotate(0, 0, -1.5f); // поворачиваемся на 1.5 градуса влево (при движении машины задним ходом)
        }
    }

    private void LateUpdate() // данный Update проверяется уже после нажатия на клавишу пользователем или обработки команды движения ИИ
    {
        if (!_isAccelerated) // если _isAccelerated != true
        {
            _force = Vector3.Lerp(_force, Vector3.zero, Time.deltaTime); // делаем плавное торможение с помощью линейной интерполяции векторов на единицу времени каждого кадра Time.deltaTime (примерно 50% расстояния между точками будет преодолеваться за кадр)
        }

        if (_groundTile == _map.GetTile(new Vector3Int((int)_selfTransform.position.x, (int)_selfTransform.position.y, (int)_selfTransform.position.z))) // если позиция машины в данном кадре равна позиции тайла травы
            _force *= 0.95f; // уменьшаем силу на 5% каждый кадр (при движении по траве, минимальную силу ускорения каждый кадр умножаем на 0.95)

        _selfTransform.position += _force; // двигаем нашу машину с помощью операции сложения векторов (наша позиция изменяется каждый кадр на величину _force).

        _isAccelerated = false; // если машину не ускорили в этом кадре, то она замедляется и _isAccelerated = false
    }
}
