using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    [SerializeField] private Transform _target, _selfTransform;

    private void LateUpdate() // сдвигаемся после того, как сдвинется машина (данный update будет вызываться после update движения машины)
    {
        _selfTransform.position = Vector3.Lerp(_selfTransform.position, _target.position + new Vector3(0, 0, -10), 0.2f); // камера следует за целью (игроком) каждый кадр на 20% и постепенно догоняет игрока
    }
}
