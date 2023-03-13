using Aditionals;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    private Vector2 _spawnPoint;
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

    [SerializeField] private TMP_Text fps;

    private bool _timerOn = true;
    
    private bool _stupidFlag;
   
    private int _score;
    private float _time;
    public int Score => _score;

    private float _screenWidth;
    private float _screenHeight;

    private PointRandomizer _randomizer;

    private bool _inAnimation = true;

    [SerializeField] private GameObject crystal;
    [SerializeField] private GameObject sphere;
    [SerializeField] private GameObject triangle;

    private int _targetCounter;

    private SpawnHandler _spawnHandler;

    void Start()
    {
        //4 is maximum
        _spawnHandler = new SpawnHandler(2);
        
        _screenWidth = Camera.main.pixelWidth;
        _screenHeight = Camera.main.pixelHeight;
        
        _randomizer = new PointRandomizer(Camera.main, _screenHeight, _screenWidth, 0.9f, 0.6f, 0.2f, 0.8f);
        
        Application.targetFrameRate = 300;

        _spawnPoint = _randomizer.GetRandomPoint();
        
        shadow.transform.position = _spawnPoint;
        
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        
        shadowAnimator.SetBool("StartShadow",true);
        
        _spawnHandler.AddInPool(crystal);
    }

    public void ReduceTarget()
    {
        _targetCounter--;
        UpdateScore();
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
        SceneManager.LoadScene("TargetMode");
    }

    private void FixedUpdate()
    {
        if (!CheckBallPosition())
        {
            GameOver();
        }
    }

    public void UpdateScore()
    {
        _score += 1;
        if (_score == 10)
        {
            _spawnHandler.AddInPool(sphere);
            _spawnHandler.AddInPool(crystal);
        }

        if (_score == 20)
        {
            _spawnHandler.AddInPool(triangle);
            _spawnHandler.AddInPool(sphere);
        }

        if (_score == 30)
        {
            _spawnHandler.CountOfSpawn = 3;
        }
    }

    private void Update()
    {
        fps.text = $"{(int) (Time.frameCount / Time.time)} FPS";
        
        scoreText.GetComponent<TMP_Text>().text = Score.ToString();
        
        if (_timerOn)
        {
            _time += Time.deltaTime;
            var roundedTime = math.round(_time);
        
            var minutes = (int)roundedTime / 60;
            var textMinutes = minutes < 10 ? $"0{minutes}" : $"{minutes}";
        
            var seconds = math.round(roundedTime % 60);
            var textSeconds = seconds < 10 ? $"0{seconds}" : $"{seconds}";
        
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

        if (_targetCounter == 0 && !_inAnimation)
        {
            SpawnTargets();
        }
    }

    private void SpawnTargets()
    {
        var poolCount = _spawnHandler.Pool.Count;
        var countOfSpawn = _spawnHandler.CountOfSpawn;

        for (int i = 0; i < countOfSpawn; i++)
        {
            var randomTargetIndex = Random.Range(0, poolCount);
            var targetSpawnPoint = _randomizer.GetRandomPoint();

            var target = Instantiate(_spawnHandler.Pool[randomTargetIndex], targetSpawnPoint, new Quaternion(0, 0, 1, 1));

            var objTransform = target.GetComponent<ISpawning>().GetTransform();
            _randomizer.IncreaseExcludingZone(objTransform);

            target.GetComponent<ISpawning>().Spawn(this);
            _targetCounter++;
        }
        
        _randomizer.ResetExcludingZone();
        
    }
}

