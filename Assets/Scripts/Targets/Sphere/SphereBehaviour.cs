using System;
using Aditionals;
using Unity.VisualScripting;
using UnityEngine;

public class SphereBehaviour : MonoBehaviour, ISpawning
{
    private GameManager _gameManager;
    private FollowingBehaviour _following;
    private MovingCircle _circle;
    private Collider2D _collider;
    
    [SerializeField] private ParticleSystem spawnEffect;
    [SerializeField] private ParticleSystem deSpawnEffect;

    [SerializeField] private GameObject sphereTexture;
    
    private Animator _spawn;

    private bool _hasTouched;
    private bool _spawned;

    private void Awake()
    {
        deSpawnEffect.Stop();
        _collider = GetComponentInChildren<Collider2D>();
        _following = GetComponentInChildren<FollowingBehaviour>();
        _circle = GetComponentInChildren<MovingCircle>();
        _spawn = GetComponent<Animator>();

        _collider.enabled = false;
    }

    private void Update()
    {
        if (!_spawned && !spawnEffect.isPlaying)
        {
            _spawn.SetBool("hasBeenSpawned", true);
            _circle.TurnOn();
            _collider.enabled = true;
            _spawned = true;
        }

        if (_hasTouched && !deSpawnEffect.isPlaying)
        {
            Destroy(gameObject);
        }
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
        _hasTouched = true;
        deSpawnEffect.gameObject.transform.position = sphereTexture.transform.position;
        Destroy(sphereTexture);
        Destroy(_following.gameObject);
        deSpawnEffect.Play();
        _gameManager.ReduceTarget();
    }
}
