
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
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

    [SerializeField] 
    private TMP_Text time;

    private bool _timerOn = true;
    
    private bool _stupidFlag;
   
    private int _score;
    private float _time;
    public int Score => _score;

    private float _screenWidth;
    private float _screenHeight;

    private bool _inAnimation = true;

    
    
    void Start()
    {
        Application.targetFrameRate = 300;

        _screenWidth = Camera.main.pixelWidth;
        _screenHeight = Camera.main.pixelHeight;

        _spawnPoint = GetRandomPoint();
        shadow.transform.position = _spawnPoint;
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        shadowAnimator.SetBool("StartShadow",true);
    }
    
    private bool CheckBallPosition()
    {
        var ballPosition = ball.transform.position;
        var relativeToCameraBallPosition = Camera.main.WorldToScreenPoint(ballPosition);
        if (!_inAnimation && (relativeToCameraBallPosition.y < 0 || relativeToCameraBallPosition.x < -5 || relativeToCameraBallPosition.x > _screenWidth + 5))
        {
            return false;
        }

        return true;
    }
    
    private void GameOver()
    {
        SceneManager.LoadScene("Control");
    }

    private void FixedUpdate()
    {
        if (!CheckBallPosition())
        {
            GameOver();
        }
    }

    private Vector2 GetRandomPoint()
    {
        var randomScreenPoint = new Vector2(Random.Range(_screenWidth * 0.1f, _screenWidth * 0.9f), Random.Range(_screenHeight * 0.6f, _screenHeight * 0.9f));
        return Camera.main.ScreenToWorldPoint(randomScreenPoint);
    }

    public void UpdateScore()
    {
        _score += 1;
    }

    private void Update()
    {
        scoreText.GetComponent<TMP_Text>().text = Score.ToString();
        
        if (_timerOn)
        {
            _time += Time.deltaTime;
            var roundedTime = math.round(_time);
        
            var minutes = (int)roundedTime / 60;
            var textMinutes = minutes < 10 ? $"0{minutes} " : $" {minutes}";
        
            var seconds = math.round(roundedTime % 60);
            var textSeconds = seconds < 10 ? $"0{seconds} " : $" {seconds}";
        
            time.text = $"{textMinutes} : {textSeconds}";
        }
        
        if (_stupidFlag && !ballAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling"))
        {
            ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            ball.transform.localScale = new Vector3(1, 1, 1);
            _stupidFlag = false;
            _inAnimation = false;
        }

        if (!ballAnimator.GetBool("StartFall") && !shadowAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shadow"))
        {
            ball.transform.position = _spawnPoint;
            ballAnimator.SetBool("StartFall",true);
            shadow.transform.position = new Vector3(100, 100, 100);
            _stupidFlag = true;
        }
    }
}

