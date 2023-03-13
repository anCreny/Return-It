using System;
using Aditionals;
using Unity.VisualScripting;
using UnityEngine;

public class TargetManager : MonoBehaviour, ISpawning
{
    private BlackHoleHandler _blackHoleHandler;
    private Core _core;
    private GameManager _gameManager;

    private void Awake()
    {
        _core = GetComponentInChildren<Core>();
        _blackHoleHandler = GetComponentInChildren<BlackHoleHandler>();
        transform.eulerAngles = Vector3.zero;
    }

    private void Update()
    {
        if (_core.IsDestroyed() && _blackHoleHandler.ReadyToRemove)
        {
            Destroy(gameObject);
        }
    }

    public void TouchCore()
    {
        _blackHoleHandler.CoreTouched();
    }

    public void ReduceTarget()
    {
        _gameManager.ReduceTarget();
    }

    public void CoreSpawned()
    {
        _blackHoleHandler.Spawn();
    }

    public void Spawn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public ObjectTransform GetTransform()
    {
        return new ObjectTransform(transform);
    }
}
