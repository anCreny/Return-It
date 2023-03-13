using System.Collections.Generic;
using UnityEngine;


public class BlackHore : MonoBehaviour
{
    private BlackHoleHandler _blackHoleHandler;
    
    private ParticleSystem _spawnEffect;
    private ParticleSystem _deadEffect;

    private Rigidbody2D _rigidbody;
    private SpriteMask _spriteMask;
    private CircleCollider2D _collider;

    private bool _spawnEffectEnded = true;
    private bool _deadEffectEnded = true;
    private bool _coreHasTouched;
    private bool _hasTouched;

    [SerializeField] private List<GameObject> unwraps = new();

    private float _screenHeight;

    public bool HasTouched => _hasTouched;

    private void Awake()
    {
        _screenHeight = Camera.main.pixelHeight;

        _blackHoleHandler = GetComponentInParent<BlackHoleHandler>();
        
        var effects = GetComponentsInChildren<ParticleSystem>();
        foreach (var effect in effects)
        {
            if (effect.gameObject.name == "SpawnEffect")
            {
                _spawnEffect = effect;
            }
            else
            {
                _deadEffect = effect;
            }
        }

        _deadEffect.Stop();
        _spawnEffect.Stop();
        
        _rigidbody = GetComponent<Rigidbody2D>();
        _spriteMask = GetComponent<SpriteMask>();
        _collider = GetComponent<CircleCollider2D>();
    }

    public void Play()
    {
        _spawnEffectEnded = false;
        _spawnEffect.Play();
    }

    private void Update()
    {
        if (_coreHasTouched)
        {
            var currentPositionOnScreen = Camera.main.WorldToScreenPoint(transform.position);
            if (currentPositionOnScreen.y < -10 || currentPositionOnScreen.y > _screenHeight + 10)
            {
                Destroy();
            }

            if (!_spawnEffectEnded)
            {
                Destroy();
            }
        }
        
        if (!_spawnEffectEnded && !_spawnEffect.isPlaying && !_coreHasTouched)
        {
            _spriteMask.enabled = true;
            _collider.enabled = true;
            _spawnEffectEnded = true;
        }

        if (!_deadEffectEnded && !_deadEffect.isPlaying)
        {
            Destroy();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (!_coreHasTouched)
        {
            _collider.enabled = false;
            _spriteMask.enabled = false;
            _deadEffectEnded = false;
            _deadEffect.Play();
            _hasTouched = true;
        }
    }

    public void TouchCore()
    {
        Destroy(GetComponent<Collider2D>());
        _coreHasTouched = true;
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForce(transform.up * 10, ForceMode2D.Impulse);
    }

    public void TouchWall()
    {
        _rigidbody.velocity = Vector2.zero;
        var randomUnwrapIndex = Random.Range(0, unwraps.Count);
        var rotation = new Quaternion(0, 0, 0, 0);
        var obj = Instantiate(unwraps[randomUnwrapIndex], transform.position, rotation);
        obj.transform.eulerAngles = new Vector3(0, 0, Random.Range(-180, 180));
        Destroy(gameObject);
    }
}
