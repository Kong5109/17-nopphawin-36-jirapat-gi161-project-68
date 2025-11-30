using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI Settings")]
    [SerializeField] private GameObject _gameOverPanel; 
    [SerializeField] private GameObject _winPanel; 

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (_gameOverPanel != null) _gameOverPanel.SetActive(false);
        
        if (_winPanel != null) _winPanel.SetActive(false);
        
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
        
        if (_gameOverPanel != null) _gameOverPanel.SetActive(true);
    }

    public void WinGame()
    {
        if (IsGameOver) return; 
        IsGameOver = true;     

        Debug.Log("YOU WIN!");

        if (_winPanel != null)
        {
            _winPanel.SetActive(true);
        }
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}