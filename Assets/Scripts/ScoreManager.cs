using UnityEngine;
using UnityEngine.UI; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    public int CurrentScore { get; private set; }
    public int HighScore { get; private set; }

    [Header("UI Settings")]
    [SerializeField] private Text _scoreText;      
    [SerializeField] private Text _highScoreText;  

    private void Start()
    {
       
        LoadHighScore();
        ResetScore();
    }

    

    public void AddScore(int amount)
    {
        CurrentScore += amount;
        
        
        UpdateScoreUI();

        
        if (CurrentScore > HighScore)
        {
            HighScore = CurrentScore;
            UpdateHighScoreUI();
            SaveHighScore();
        }
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        UpdateScoreUI();
    }

    public void SaveHighScore()
    {
        PlayerPrefs.SetInt("HighScore", HighScore);
        PlayerPrefs.Save();
    }

    public void LoadHighScore()
    {
        HighScore = PlayerPrefs.GetInt("HighScore", 0);
        UpdateHighScoreUI();
    }
    
    
    private void UpdateScoreUI()
    {
        if (_scoreText != null)
        {
            _scoreText.text = "Score: " + CurrentScore;
        }
    }

    private void UpdateHighScoreUI()
    {
        if (_highScoreText != null)
        {
            _highScoreText.text = "High Score: " + HighScore;
        }
    }
}