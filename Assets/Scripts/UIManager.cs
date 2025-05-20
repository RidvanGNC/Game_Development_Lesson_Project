using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelCompletePanel;
    [SerializeField] private Slider progressBar;
    [SerializeField] private TextMeshProUGUI levelText;

    void Start()
    {
        progressBar.value = 0f;

        levelText.text = "Level - " + (ChunkManager.instance.GetLevels() + 1).ToString();

        GameManager.OnGameStateChanged += GameStateChangedCallBack;
    }

    public void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallBack;
    }

    void FixedUpdate()
    {
        UpdateProgessBar();
    }

    private void GameStateChangedCallBack(GameManager.GameState gameState)
    {
        if (gameState == GameManager.GameState.GameOver)
        {
            Show_GameOver_Panel();
        }
        else if (gameState == GameManager.GameState.LevelComplete)
        {
            Show_LevelComplete_Panel();
        }
    }

    public void Play_Button_Pressed()
    {
        GameManager.instance.SetGameState(GameManager.GameState.Game);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(false);
    }

    public void Restart_Button_Pressed()
    {
        SceneManager.LoadScene(0);
    }

    public void LevelComplete_Button_Pressed()
    {
        SceneManager.LoadScene(0);
    }

    public void Show_GameOver_Panel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }

    public void Show_LevelComplete_Panel()
    {
        gamePanel.SetActive(false);
        levelCompletePanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    public void UpdateProgessBar()
    {
        if (!GameManager.instance.isGameState())
            return;

        float progress = PlayerController.instance.transform.position.z / ChunkManager.instance.GetFinishZ();
        progressBar.value = progress;
    }
}
