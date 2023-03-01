using UnityEngine;

public class RhombusTarget : MonoBehaviour
{
    private float _durability = -1;
    
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public bool SetDurability(float value)
    {
        if (_durability == -1)
        {
            _durability = value;
            return true;
        }

        return false;

    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _durability--;
        if (_durability == 0)
        {
            BreakYourself();
            return;
        }
        var ball = col.gameObject.GetComponent<Ball>();
        
        var impulse = col.relativeVelocity;
        ball.Rigidbody.AddForce(impulse * -1 * 0.7f, ForceMode2D.Impulse);
    }

    private void BreakYourself()
    {
        Destroy(gameObject);
    }
}
