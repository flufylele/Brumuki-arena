using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class InGameUiController : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] private GameObject diedUI;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private TextMeshProUGUI enemiesKilled;
    [SerializeField] private TextMeshProUGUI shotsFired;
    [SerializeField] private TextMeshProUGUI timeSurvived;
    [SerializeField] private TextMeshProUGUI playerNameHighscore;
    [SerializeField] private TextMeshProUGUI goldText;
    [SerializeField] private TextMeshProUGUI scoreText;
    private GameStats gameStats;
    private SettingsJSON playerStats;

    private void Awake()
    {
        gameStats = GameObject.Find("Stats").GetComponent<GameStats>();
        gameSettings = GameObject.Find("Game settings").GetComponent<GameSettings>();
        playerStats = GameObject.Find("StatSettings").GetComponent<SettingsJSON>();
        diedUI.SetActive(false);
        pauseMenu.SetActive(false);
        goldText.text = gameStats.gold.ToString();
        scoreText.text = "Score: " + gameStats.score.ToString();
    }



    private void Update()
    {
        gameStats.timeSruvived += Time.deltaTime;
    }

    public void MainMenu()
    {
        gameStats.ResetStats();
        playerStats.SetStatsBasedOnDifficulty();
        SceneManager.LoadScene("Main menu");
        gameSettings.gamePaused = false;
        Time.timeScale = 1;
    }

    

    public void DiedMenu()
    {
        gameSettings.music.Stop();
        diedUI.SetActive(true);
        shotsFired.text = gameStats.shotsFired.ToString();
        enemiesKilled.text = gameStats.enemiesKilled.ToString();
        timeSurvived.text = gameStats.timeSruvived.ToString();
    }

    public void PauseMenu()
    {
        if (gameSettings.gamePaused == false)
        {
            Time.timeScale = 0;
            gameSettings.music.Pause();
            pauseMenu.SetActive(true);
            gameSettings.gamePaused = true;
        }
        else
        {
            Time.timeScale = 1;
            gameSettings.music.Play();
            pauseMenu.SetActive(false);
            gameSettings.gamePaused = false;
        }
    }

    public void SubmitHighscore()
    {
        if(playerNameHighscore.text.Length > 1)
        {
           gameSettings.SaveHighScore(gameStats.score, playerNameHighscore.text);
           MainMenu();
        }
        
    }
}
