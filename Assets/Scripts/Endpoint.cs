using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Endpoint : MonoBehaviour
{
    public bool IsReached { get; private set; }

    public event UnityAction Reached;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (IsReached) // если мы уже проехали endpoint, то игнорируем вызов события Reached
            return;

        if (collision.TryGetComponent(out Player player))
        {
            IsReached = true; // когда игрок коснулся триггера, то меняем флаг "достигнут" на true
            Reached?.Invoke();
        }
    }

    public void ReachNextLap()
    {
        IsReached = false;
    }
}
