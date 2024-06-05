using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MainMenuController : MonoBehaviour
{
    private GameObject mainMenu;
    private GameObject settings;
    private GameObject controls;
    private GameObject highscores;
    [SerializeField] private AudioSource music;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private EnemyStats enemyStats;
    private SettingsJSON playerStats;
    [SerializeField] private Slider volume;
    [SerializeField] private Slider difficulty;
    [SerializeField] private Toggle enemiesHealthbar;
    [SerializeField] private Toggle playerHealthBar;
    
    private void Awake()
    {
        
        gameSettings.music = GameObject.Find("Music").GetComponent<AudioSource>();
        enemyStats = GameObject.Find("EnemyStats").GetComponent<EnemyStats>();
        playerStats = GameObject.Find("StatSettings").GetComponent<SettingsJSON>();
        volume.value = gameSettings.settings.volume;
        music.volume = gameSettings.settings.volume;
        difficulty.value = gameSettings.settings.difficulty;
        enemiesHealthbar.isOn = gameSettings.settings.enemiesHealthbar;
        playerHealthBar.isOn = gameSettings.settings.playerHealthBar;
    }

    private void Start()
    {
        mainMenu = GameObject.Find("Main menu");
        settings = GameObject.Find("Settings");
        controls = GameObject.Find("Controls");
        highscores = GameObject.Find("Highscores");
        mainMenu.SetActive(true);
        settings.SetActive(false);
        controls.SetActive(false);
        highscores.SetActive(false);
    }

    public void StartGame()
    {
        enemyStats.UpdateStats();
        playerStats.SetStatsBasedOnDifficulty();
        SceneManager.LoadScene("Level1");
        
    }

    public void Settings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void MainMenu()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        controls.SetActive(false);
        highscores.SetActive(false);
    }

    public void MainMenuSettings()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        gameSettings.SaveSettings();
    }

    public void Controls()
    {
        mainMenu.SetActive(false);
        controls.SetActive(true);
    }

    public void Highscores()
    {
        mainMenu.SetActive(false);
        highscores.SetActive(true);
        if(gameSettings.newHighScore)
        {
            gameSettings.DestroyGeneratedHighscores();
            gameSettings.LoadHighScores();
            gameSettings.newHighScore = false;  
        }
    }

    public void Volume()
    {
        music.volume = volume.value;
        gameSettings.settings.volume = volume.value;
    }

    public void Difficulty()
    {
        gameSettings.settings.difficulty = difficulty.value;
        gameSettings.settings.SetStatsBasedOnDifficulty();
        gameSettings.SetAllUpgradeValues();
    }

    public void EnemiesHealthbar()
    {
        gameSettings.settings.enemiesHealthbar = enemiesHealthbar.isOn;
    }

    public void PlayerHealthBar()
    {
        gameSettings.settings.playerHealthBar = playerHealthBar.isOn;
    }


    public void Quit()
    {
        Application.Quit();
    }


}
