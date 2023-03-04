using Aditionals;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RhombusTarget : MonoBehaviour, ISpawning
{
    [SerializeField] private RhombusLightBehavior light;
    
    private int _durability;

    private GameManager _gameManager;

    private float _border;

    private Rigidbody2D _rigidbody;

    private Collision2D _ballContact;

    private Light2D _light2D;

    private void Awake()
    {
        _light2D = GetComponentInChildren<Light2D>();
        _durability = 2;
        _border = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void SetLastBallContact(Collision2D collision)
    {
        _ballContact = collision;
    }

    public void Spawn(GameManager gameManager)
    {
        _gameManager = gameManager;
    }


    public void ReduceDurability()
    {
        _durability--;
        if (_durability == 0)
        {
            BreakYourself();
        }
        else if (_durability == 1)
        {
            light.ReduceLight();
            _light2D.intensity = 0.1f;
        }
    }

    private void Update()
    {
        if (gameObject.transform.position.y < _border)
        {
            _gameManager.ReduceTarget();
            Destroy(gameObject);
        }
    }

    private void BreakYourself()
    {
        light.TurnOffLight();
        Destroy(_light2D);
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForceAtPosition(_ballContact.relativeVelocity * 0.2f, _ballContact.contacts[0].point, ForceMode2D.Impulse);
    }
}
