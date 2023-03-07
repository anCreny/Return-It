using System;
using Aditionals;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour, ISpawning
{
    private GameManager _gameManager;

    private FollowingBehaviour _following;

    private void Awake()
    {
        _following = GetComponentInChildren<FollowingBehaviour>();
    }


    public void Spawn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public ObjectTransform GetTransform()
    {
        return new ObjectTransform(_following.transform);
    }

    public void HasTouched()
    {
        Destroy(gameObject);
        _gameManager.UpdateScore();
        _gameManager.ReduceTarget();
    }
}
