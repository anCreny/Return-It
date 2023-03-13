using UnityEngine;

public class Unwraper : MonoBehaviour
{
    private ParticleSystem _spawnEffect;

    private bool _hasSpawned;

    private void Awake()
    {
        _spawnEffect = GetComponentInChildren<ParticleSystem>();
    }

    private void Update()
    {
        if (!_hasSpawned && !_spawnEffect.isPlaying)
        {
            _hasSpawned = true;
            Destroy(_spawnEffect.gameObject);
        }
    }
}
