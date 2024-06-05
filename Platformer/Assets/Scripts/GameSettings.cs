using JetBrains.Annotations;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameSettings : MonoBehaviour
{

    private static GameSettings singleton;
    [SerializeField] private GameObject highscorePrefab;
    [SerializeField] private GameObject highscoresUI;
    public bool newHighScore;
    [SerializeField] public SettingsJSON settings;
    [SerializeField] public EnemyStats enemyStats;
    [SerializeField] public List<Highscores> highscores;



    [Header("JSON")]

    private string settingsJSONPath;
    private string settingsJSONContent;
    private string highscoresJSONPath;
    private string highScoresJSONContent;

    [Header("Components")]
    [NonSerialized] public AudioSource music;

    [Header("Checks")]
    [NonSerialized] public bool gamePaused;

    [Header("Shop")]
    [NonSerialized] public int healthCost;
    [NonSerialized] public int armorCost;
    [NonSerialized] public int movementSpeedCost;
    [NonSerialized] public int jumpsCost;
    [NonSerialized] public int jumpForceCost;
    [NonSerialized] public int projectileDurationCost;
    [NonSerialized] public int projectileSpeedCost;
    [NonSerialized] public int fireRateCost;

    [NonSerialized] public int baseHealthCost;
    [NonSerialized] public int baseArmorCost;
    [NonSerialized] public int baseMovementSpeedCost;
    [NonSerialized] public int baseJumpsCost;
    [NonSerialized] public int baseJumpForceCost;
    [NonSerialized] public int baseProjectileDurationCost;
    [NonSerialized] public int baseProjectileSpeedCost;
    [NonSerialized] public int baseFireRateCost;

    [NonSerialized] public int maxHealthUpgrade;
    [NonSerialized] public int maxArmorUpgrade;
    [NonSerialized] public int maxMovementSpeedUpgrade;
    [NonSerialized] public int maxJumpsUpgrade;
    [NonSerialized] public int maxJumpForceUpgrade;
    [NonSerialized] public int maxProjectileDurationUpgrade;
    [NonSerialized] public int maxProjectileSpeedUpgrade;
    [NonSerialized] public int maxFireRateUpgrade;

    [NonSerialized] public int healthUpgradeBought;
    [NonSerialized] public int armorUpgradeBought;
    [NonSerialized] public int movementSpeedUpgradeBought;
    [NonSerialized] public int jumpsUpgradeBought;
    [NonSerialized] public int jumpForceUpgradeBought;
    [NonSerialized] public int projectileDurationUpgradeBought;
    [NonSerialized] public int projectileSpeedUpgradeBought;
    [NonSerialized] public int fireRateUpgradeBought;

    [NonSerialized] public float healthUpgradeValue;
    [NonSerialized] public float armorUpgradeValue;
    [NonSerialized] public float movementSpeedUpgradeValue;
    [NonSerialized] public int jumpsUpgradeValue;
    [NonSerialized] public float jumpForceUpgradeValue;
    [NonSerialized] public float projectileDurationUpgradeValue;
    [NonSerialized] public float projectileSpeedUpgradeValue;
    [NonSerialized] public float fireRateUpgradeValue;

    private void Awake()
    {
        if (singleton == null)
        {
            singleton = this;
        }
        else
        {
            Destroy(gameObject);
        };

        if (!Directory.Exists(Path.GetFullPath("./") + "Settings"))
        {
            Directory.CreateDirectory(Path.GetFullPath("./") + "Settings");
        }
        if (!Directory.Exists(Path.GetFullPath("./") + "Highscores"))
        {
            Directory.CreateDirectory(Path.GetFullPath("./") + "Highscores");
        }

        settingsJSONPath = Path.GetFullPath("./") + "Settings\\GameSettings.json";
        highscoresJSONPath = Path.GetFullPath("./") + "Highscores\\Highscores.json";
        highscores = new List<Highscores>();

        if (System.IO.File.Exists(settingsJSONPath))
        {
            LoadSettings();
        }
        else
        {
            SaveSettings();
        }
        if (System.IO.File.Exists(highscoresJSONPath))
        {
            LoadHighScores();
        }

        StartingBaseCosts();
        SetAllUpgradesBought();
        SetAllUpgradeValues();

        DontDestroyOnLoad(gameObject);
    }

    public void SaveSettings()
    {
        
        settingsJSONContent = JsonUtility.ToJson(settings, true);
        System.IO.File.WriteAllText(settingsJSONPath, settingsJSONContent);
    }

    private void LoadSettings()
    {
        settingsJSONContent = System.IO.File.ReadAllText(settingsJSONPath);
        JsonUtility.FromJsonOverwrite(settingsJSONContent, settings);
        settings.SetStatsBasedOnDifficulty();
        enemyStats.UpdateStats();
    }

    public void LoadHighScores()
    {
        if(highscores.Count == 0)
        {
            highScoresJSONContent = System.IO.File.ReadAllText(highscoresJSONPath);
            highscores = JsonConvert.DeserializeObject<List<Highscores>>(highScoresJSONContent);
        }

        int numberOfHighscores = 0;
        foreach(Highscores currentHighScore in highscores)
        {
            GameObject newHighscore = Instantiate(highscorePrefab) as GameObject;
            newHighscore.transform.SetParent(highscoresUI.transform, false);
            newHighscore.transform.localPosition = new Vector3(newHighscore.transform.localPosition.x, newHighscore.transform.localPosition.y - (numberOfHighscores * 60), newHighscore.transform.localPosition.z);
            newHighscore.GetComponent<HighscoreUI>().playerName.text = currentHighScore.playerName;
            newHighscore.GetComponent<HighscoreUI>().score.text = currentHighScore.score.ToString();
            numberOfHighscores += 1;
        }
    }

    public void SaveHighScore(int newScore, string newName)
    {
        if(highscores.Count == 0)
        {
            highscores.Add(new Highscores() { playerName = newName, score = newScore });
        }
        else
        {
            highscores = highscores.OrderByDescending(currentElement => currentElement.score).ToList();
            if (newScore > highscores[highscores.Count - 1].score)
            {
                highscores.Add(new Highscores() { playerName = newName, score = newScore });
                highscores = highscores.OrderByDescending(currentElement => currentElement.score).ToList();
                if (highscores.Count > 7)
                {
                    highscores.RemoveAt(highscores.Count - 1);
                }
            }
        }
        highScoresJSONContent = JsonConvert.SerializeObject(highscores, Formatting.Indented);
        System.IO.File.WriteAllText(highscoresJSONPath, highScoresJSONContent);
        newHighScore = true;


    }

    public void DestroyGeneratedHighscores()
    {
        while (highscoresUI.transform.childCount > 0)
        {
            DestroyImmediate(highscoresUI.transform.GetChild(0).gameObject);
        }
    }

    public void SetShopCosts()
    {
        healthCost = (int)(baseHealthCost * healthUpgradeBought * settings.difficulty);
        armorCost = (int)(baseArmorCost * armorUpgradeBought * settings.difficulty);
        movementSpeedCost = (int)(baseMovementSpeedCost * movementSpeedUpgradeBought * settings.difficulty);
        jumpsCost = (int)(baseJumpsCost * jumpsUpgradeBought * settings.difficulty);
        jumpForceCost = (int)(baseJumpForceCost * jumpForceUpgradeBought * settings.difficulty);
        projectileDurationCost = (int)(baseProjectileDurationCost * projectileDurationUpgradeBought * settings.difficulty);
        projectileSpeedCost = (int)(baseProjectileSpeedCost * projectileSpeedUpgradeBought * settings.difficulty);
        fireRateCost = (int)(baseFireRateCost * fireRateUpgradeBought * settings.difficulty);
    }

    private void StartingBaseCosts()
    {
        baseHealthCost = 500;
        baseArmorCost = 600;
        baseMovementSpeedCost = 1000;
        baseJumpsCost = 3000;
        baseJumpForceCost = 1200;
        baseProjectileDurationCost = 2000;
        baseProjectileSpeedCost = 1500;
        baseFireRateCost = 1700;
    }

    private void SetAllUpgradesBought()
    {
        healthUpgradeBought = 1;
        armorUpgradeBought = 1;
        movementSpeedUpgradeBought = 1;
        jumpsUpgradeBought = 1;
        jumpForceUpgradeBought = 1;
        projectileDurationUpgradeBought = 1;
        projectileSpeedUpgradeBought = 1;
        fireRateUpgradeBought = 1;
    }

    public void SetAllMaxUpgrades()
    {
        maxHealthUpgrade = (int)(10 - settings.difficulty);
        maxArmorUpgrade = (int)(8 - settings.difficulty);
        maxMovementSpeedUpgrade = (int)(10 - settings.difficulty);
        maxJumpsUpgrade = (int)(4 - settings.difficulty);
        maxJumpForceUpgrade = (int)(10 - settings.difficulty);
        maxProjectileDurationUpgrade = (int)(10 - settings.difficulty);
        maxProjectileSpeedUpgrade = (int)(10 - settings.difficulty);
        maxFireRateUpgrade = (int)(12 - settings.difficulty);
    }

    public void SetAllUpgradeValues()
    {
        healthUpgradeValue = (int)(50 / settings.difficulty);
        armorUpgradeValue = (int)(30 / settings.difficulty);
        movementSpeedUpgradeValue = (int)(5 / settings.difficulty);
        jumpsUpgradeValue = 1;
        jumpForceUpgradeValue = (int)(5 / settings.difficulty);
        projectileDurationUpgradeValue = (int)(3 / settings.difficulty);
        projectileSpeedUpgradeValue = (int)(3 / settings.difficulty);
        fireRateUpgradeValue = 0.02f / settings.difficulty;
    }
    
}
[Serializable]
public class Highscores
{
    [SerializeField] public int score;
    [SerializeField] public string playerName;
}

