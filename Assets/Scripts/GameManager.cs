using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour
{
   
    public static GameManager Instance;

    [Header("UI Settings")]
    [SerializeField] private GameObject _gameOverPanel; 

    
    public bool IsGameOver { get; private set; }

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

    private void Start()
    {
        
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(false);
        }
        
        IsGameOver = false;
    }

    private void Update()
    {
        if (IsGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void GameOver()
    {
        if (IsGameOver) return; 

        IsGameOver = true;
        Debug.Log("GAME OVER!");
        
        if (_gameOverPanel != null)
        {
            _gameOverPanel.SetActive(true);
        }

      
        WaveManager waveManager = FindObjectOfType<WaveManager>();
        if (waveManager != null) waveManager.enabled = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}