using System;
using Aditionals;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Core : MonoBehaviour, ICollisionNeeded
{
    private BlackHoleHandler _blackHoleHandler;
    private ParticleSystem _soul;
    private Light2D _light;
    private Rigidbody2D _rigidbody;
    private Collider2D _collider;

    private TargetManager _targetManager;
    private void Awake()
    {
        _blackHoleHandler = GetComponentInChildren<BlackHoleHandler>();
        _light = GetComponentInChildren<Light2D>();
        _soul = GetComponentInChildren<ParticleSystem>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _collider = GetComponentInChildren<Collider2D>();

        _targetManager = GetComponentInParent<TargetManager>();
    }

    public void HasSpawned()
    {
        _targetManager.CoreSpawned();
        _collider.enabled = true;
    }

    private void Update()
    {
        var currentPosOnScreen = Camera.main.WorldToScreenPoint(transform.position);
        if (currentPosOnScreen.y < -10)
        {
            _targetManager.ReduceTarget();
            Destroy(gameObject);
        }
    }

    public void DetectCollision(Collision2D collision)
    {
        GetComponent<Animator>().StopPlayback();
        Destroy(GetComponent<Animator>());
        Destroy(_collider);
        GetComponentInChildren<SpriteRenderer>().color = Color.gray;
        _targetManager.TouchCore();
        _light.enabled = false;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForceAtPosition(collision.relativeVelocity * 0.2f, collision.contacts[0].point, ForceMode2D.Impulse);
        Destroy(_soul);
    }
}
