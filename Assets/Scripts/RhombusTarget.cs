using UnityEngine;

public class RhombusTarget : MonoBehaviour
{
    private int _durability = -1;

    private GameManager _gameManager;

    public void Spawn(int durability, GameManager gameManager)
    {
        _durability = durability;
        _gameManager = gameManager;
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
        _gameManager.ReduceTarget();
        Destroy(gameObject);
    }
}
