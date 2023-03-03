using UnityEngine;

public class RhombusTarget : MonoBehaviour
{
    [SerializeField] private RhombusLightBehavior light;
    
    private int _durability;

    private GameManager _gameManager;

    private float _border;

    private Rigidbody2D _rigidbody;

    private Collision2D _ballContact;

    private void Awake()
    {
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
        _rigidbody.bodyType = RigidbodyType2D.Dynamic;
        _rigidbody.AddForceAtPosition(_ballContact.relativeVelocity * 0.2f, _ballContact.contacts[0].point, ForceMode2D.Impulse);
    }
}
