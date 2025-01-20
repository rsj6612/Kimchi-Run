using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public enum GameState
    {
        Intro,
        Playing,
        Dead
    }
    public static GameManager Instance; // 싱글톤

    public GameState State = GameState.Intro;
    
    public float PlayStartTime;

    public int lives = 3;
    
    [Header("References")]
    public GameObject IntroUI;
    public GameObject DeadUI;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldenSpawner;
    public Player player;
    public TMP_Text scoreText;


    void Awake(){
        if(Instance == null)
        {
            Instance = this;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        IntroUI.SetActive(true);
    }

    float CalculateScore(){
        return Time.time - PlayStartTime;
    }

    void SaveHighScore(){
        int score = Mathf.FloorToInt(CalculateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore"); // 저장 기능
        if (score > currentHighScore){
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }

    int GetHighScore(){
        return PlayerPrefs.GetInt("highScore");
    }

    public float CalculateGameSpeed(){
        if (State != GameState.Playing){
            return 5f;
        }
        float speed = 8f + (0.5f * Mathf.Floor(CalculateScore() / 10f)); // 10초마다 0.5씩 속도가 올라감
        return MathF.Min(speed, 30f); // 최소값 반환
    }

    // Update is called once per frame
    void Update()
    {
        if (State == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalculateScore());
        } else if (State == GameState.Dead || State == GameState.Intro){
            scoreText.text = "High Score: " + GetHighScore();
        }
        if (State == GameState.Intro && Input.GetKeyDown(KeyCode.Space))
        {
            State = GameState.Playing;
            IntroUI.SetActive(false);
            EnemySpawner.SetActive(true);
            FoodSpawner.SetActive(true);
            GoldenSpawner.SetActive(true);
            PlayStartTime = Time.time;
        }

        if (State == GameState.Playing && lives == 0)
        {
            player.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldenSpawner.SetActive(false);
            DeadUI.SetActive(true);
            SaveHighScore(); // 하이스코어 저장 호출
            State = GameState.Dead;
        }

        if (State == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
}
