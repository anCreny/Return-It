
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Vector2 _spawnPoint = new (0, 3);
    [SerializeField]
    private Animator shadowAnimator;
    [SerializeField]
    private Animator ballAnimator;
    [SerializeField] 
    private GameObject ball;
    [SerializeField] 
    private GameObject shadow;

    [SerializeField] 
    private GameObject scoreText;
    private static GameManager _singleton;
    private bool _stupidFlag;
   
    private int _score;
    private float _time;
    public int Score => _score;

    public static GameManager Singleton
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = new GameManager();
            }

            return _singleton;
        }
    }

    public int GetScore()
    {
        return _score;
    }
    void Start()
    {
        _singleton = this;
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

    public void UpdateScore()
    {
        _score += 1;
    }

    private void Update()
    {
        _time += Time.deltaTime;
        scoreText.GetComponent<TMP_Text>().text = Score.ToString();
    }
}

