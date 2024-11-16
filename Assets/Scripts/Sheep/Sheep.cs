using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Sheep : MonoBehaviour
{
    private Vector2 _startPosition;
    public event Action GameOver;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void Dead()
    {
        GameOver?.Invoke();
    }

    public void Reset()
    {
        transform.position = _startPosition;
    }
}
