using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private Vector2 _spawnPoint = new (0, 3);
    [FormerlySerializedAs("shadow")] [SerializeField]
    private Animator shadowAnimator;
    [FormerlySerializedAs("ball")] [SerializeField]
    private Animator ballAnimator;
    [SerializeField] 
    private GameObject ball;
    [SerializeField] 
    private GameObject shadow;

    private bool _stupidFlag;
    void Start()
    {
        _spawnPoint = GetRandomPoint();
        shadow.transform.position = _spawnPoint;
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        shadowAnimator.SetBool("StartShadow",true);
    }

    private void FixedUpdate()
    {
        if (_stupidFlag && !ballAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ball.transform.localScale = new Vector3(1, 1, 1);
            _stupidFlag = false;
        }

        if (!ballAnimator.GetBool("StartFall") && !shadowAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shadow"))
        {
            ball.transform.position = _spawnPoint;
            ballAnimator.SetBool("StartFall",true);
            shadow.transform.position = new Vector3(100, 100, 100);
            _stupidFlag = true;
        }
    }

    private Vector2 GetRandomPoint()
    {
        var width = Camera.main.pixelWidth;
        var height = Camera.main.pixelHeight;

        var randomScreenPoint = new Vector2(Random.Range(width * 0.1f, width * 0.9f), Random.Range(height * 0.6f, height * 0.9f));
        return Camera.main.ScreenToWorldPoint(randomScreenPoint);
    }

    private void Update()
    {
        
    }
}
