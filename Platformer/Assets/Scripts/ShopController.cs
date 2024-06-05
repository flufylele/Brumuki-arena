using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private GameSettings gameSettings;
    private SettingsJSON playerStats;
    private GameStats stats;
    private EnemyStats enemyStats;

    [SerializeField] private TextMeshProUGUI goldText;

    [SerializeField] private TextMeshProUGUI healthCostText;
    [SerializeField] private TextMeshProUGUI armorCostText;
    [SerializeField] private TextMeshProUGUI movementSpeedCostText;
    [SerializeField] private TextMeshProUGUI jumpsCostText;
    [SerializeField] private TextMeshProUGUI jumpForceCostText;
    [SerializeField] private TextMeshProUGUI projectileDurationCostText;
    [SerializeField] private TextMeshProUGUI projectileSpeedCostText;
    [SerializeField] private TextMeshProUGUI fireRateCostText;

    [SerializeField] private TextMeshProUGUI currentHealthValueText;
    [SerializeField] private TextMeshProUGUI currentArmorValueText;
    [SerializeField] private TextMeshProUGUI currentMovementSpeedValueText;
    [SerializeField] private TextMeshProUGUI currentJumpsValueText;
    [SerializeField] private TextMeshProUGUI currentJumpForceValueText;
    [SerializeField] private TextMeshProUGUI currentProjectileDurationValueText;
    [SerializeField] private TextMeshProUGUI currentProjectileSpeedValueText;
    [SerializeField] private TextMeshProUGUI currentFireRateValueText;

    [SerializeField] private Button healthUpgradeButton;
    [SerializeField] private Button armorUpgradeButton;
    [SerializeField] private Button movementSpeedUpgradeButton;
    [SerializeField] private Button jumpsUpgradeButton;
    [SerializeField] private Button jumpForceUpgradeButton;
    [SerializeField] private Button projectileDurationUpgradeButton;
    [SerializeField] private Button projectileSpeedUpgradeButton;
    [SerializeField] private Button fireRateUpgradeButton;


    void Start()
    {
        gameSettings = GameObject.Find("Game settings").GetComponent<GameSettings>();
        playerStats = GameObject.Find("StatSettings").GetComponent<SettingsJSON>();
        stats = GameObject.Find("Stats").GetComponent<GameStats>();
        enemyStats = GameObject.Find("EnemyStats").GetComponent<EnemyStats>();
        gameSettings.SetShopCosts();
        gameSettings.SetAllMaxUpgrades();
        SetCostTexts();
        SetCostsButtonsHighlightColor();
        SetInitialStatsValues();
    }

    private void SetCostTexts()
    {
        if (gameSettings.healthUpgradeBought < gameSettings.maxHealthUpgrade)
            healthCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.healthCost.ToString();
        else
            healthCostText.text = "MAXED OUT";

        if (gameSettings.armorUpgradeBought < gameSettings.maxArmorUpgrade)
            armorCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.armorCost.ToString();
        else
            armorCostText.text = "MAXED OUT";

        if (gameSettings.movementSpeedUpgradeBought < gameSettings.maxMovementSpeedUpgrade)
            movementSpeedCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.movementSpeedCost.ToString();
        else
            movementSpeedCostText.text = "MAXED OUT";

        if (gameSettings.jumpsUpgradeBought < gameSettings.maxJumpsUpgrade)
            jumpsCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.jumpsCost.ToString();
        else
            jumpsCostText.text = "MAXED OUT";

        if (gameSettings.jumpForceUpgradeBought < gameSettings.maxJumpForceUpgrade)
            jumpForceCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.jumpForceCost.ToString();
        else
            jumpForceCostText.text = "MAXED OUT";

        if (gameSettings.projectileDurationUpgradeBought < gameSettings.maxProjectileDurationUpgrade)
            projectileDurationCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.projectileDurationCost.ToString();
        else
            projectileDurationCostText.text = "MAXED OUT";

        if (gameSettings.projectileSpeedUpgradeBought < gameSettings.maxProjectileSpeedUpgrade)
            projectileSpeedCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.projectileSpeedCost.ToString();
        else
            projectileSpeedCostText.text = "MAXED OUT";

        if (gameSettings.fireRateUpgradeBought < gameSettings.maxFireRateUpgrade)
            fireRateCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.fireRateCost.ToString();
        else
            fireRateCostText.text = "MAXED OUT";

        }

    private void SetInitialStatsValues()
    {
        currentHealthValueText.text = "Health: " + playerStats.maxHealth.ToString();
        currentArmorValueText.text = "Armor: " + playerStats.maxArmor.ToString();
        currentMovementSpeedValueText.text = "Movement speed: " + playerStats.speed.ToString();
        currentJumpsValueText.text = "Jumps: " + playerStats.maxNumberOfJumps.ToString();
        currentJumpForceValueText.text = "Jump force: " + playerStats.jumpForce.ToString();
        currentProjectileDurationValueText.text = "Projectile duration: " + playerStats.maxProjectileDuration.ToString();
        currentProjectileSpeedValueText.text = "Projectile speed: " + playerStats.projectileSpeed.ToString();
        currentFireRateValueText.text = "Fire rate: " + playerStats.fireRate.ToString();
        goldText.text = "Gold: " + stats.gold.ToString();
    }
    private void SetCostsButtonsHighlightColor()
    {
        ColorBlock color;

        if(gameSettings.healthUpgradeBought < gameSettings.maxHealthUpgrade)
        {
            color = healthUpgradeButton.colors;
            if (stats.gold >= gameSettings.healthCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            healthUpgradeButton.colors = color;
        }

        if (gameSettings.armorUpgradeBought < gameSettings.maxArmorUpgrade)
        {
            color = armorUpgradeButton.colors;
            if (stats.gold >= gameSettings.armorCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            armorUpgradeButton.colors = color;
        }

        if (gameSettings.movementSpeedUpgradeBought < gameSettings.maxMovementSpeedUpgrade)
        {
            color = movementSpeedUpgradeButton.colors;
            if (stats.gold >= gameSettings.movementSpeedCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            movementSpeedUpgradeButton.colors = color;
        }

        if (gameSettings.jumpsUpgradeBought < gameSettings.maxJumpsUpgrade)
        {
            color = jumpsUpgradeButton.colors;
            if (stats.gold >= gameSettings.jumpsCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            jumpsUpgradeButton.colors = color;
        }

        if (gameSettings.jumpForceUpgradeBought < gameSettings.maxJumpForceUpgrade)
        {
            color = jumpForceUpgradeButton.colors;
            if (stats.gold >= gameSettings.jumpForceCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            jumpForceUpgradeButton.colors = color;
        }

        if (gameSettings.projectileDurationUpgradeBought < gameSettings.maxProjectileDurationUpgrade)
        {
            color = projectileDurationUpgradeButton.colors;
            if (stats.gold >= gameSettings.projectileDurationCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            projectileDurationUpgradeButton.colors = color;
        }

        if (gameSettings.projectileSpeedUpgradeBought < gameSettings.maxProjectileSpeedUpgrade)
        {
            color = projectileSpeedUpgradeButton.colors;
            if (stats.gold >= gameSettings.projectileSpeedCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            projectileSpeedUpgradeButton.colors = color;
        }

        if (gameSettings.fireRateUpgradeBought < gameSettings.maxFireRateUpgrade)
        {
            color = fireRateUpgradeButton.colors;
            if (stats.gold >= gameSettings.fireRateCost) color.pressedColor = Color.green;
            else color.pressedColor = Color.red;
            fireRateUpgradeButton.colors = color;
        }
    }

    public void UpgradeHealth()
    {

        if(stats.gold >= gameSettings.healthCost && gameSettings.healthUpgradeBought < gameSettings.maxHealthUpgrade)
        {
            
            playerStats.maxHealth += gameSettings.healthUpgradeValue;
            currentHealthValueText.text = "Health: " + playerStats.maxHealth.ToString();
            gameSettings.healthUpgradeBought += 1;
            stats.gold -= gameSettings.healthCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.healthCost = (int)(gameSettings.baseHealthCost * gameSettings.healthUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.healthUpgradeBought < gameSettings.maxHealthUpgrade) 
                healthCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.healthCost.ToString();
            else
            {
                ColorBlock color;
                healthCostText.text = "MAXED OUT";
                color = healthUpgradeButton.colors;
                color.pressedColor = Color.red;
                healthUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void UpgradeArmor()
    {
        if (stats.gold >= gameSettings.armorCost && gameSettings.armorUpgradeBought < gameSettings.maxArmorUpgrade)
        {
            playerStats.maxArmor += gameSettings.healthUpgradeValue;
            currentArmorValueText.text = "Armor: " + playerStats.maxArmor.ToString();
            gameSettings.armorUpgradeBought += 1;
            stats.gold -= gameSettings.armorCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.armorCost = (int)(gameSettings.baseArmorCost * gameSettings.armorUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.armorUpgradeBought < gameSettings.maxArmorUpgrade)
                armorCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.armorCost.ToString();
            else
            {
                ColorBlock color;
                armorCostText.text = "MAXED OUT";
                color = armorUpgradeButton.colors;
                color.pressedColor = Color.red;
                armorUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }
    public void UpgradeMovementSpeed()
    {
        if (stats.gold >= gameSettings.movementSpeedCost && gameSettings.movementSpeedUpgradeBought < gameSettings.maxMovementSpeedUpgrade)
        {
            playerStats.speed += gameSettings.movementSpeedUpgradeValue;
            currentMovementSpeedValueText.text = "Movement speed: " + playerStats.speed.ToString();
            gameSettings.movementSpeedUpgradeBought += 1;
            stats.gold -= gameSettings.movementSpeedCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.movementSpeedCost = (int)(gameSettings.baseMovementSpeedCost * gameSettings.movementSpeedUpgradeBought * gameSettings.settings.difficulty);
            
            if (gameSettings.movementSpeedUpgradeBought < gameSettings.maxMovementSpeedUpgrade) 
                movementSpeedCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.movementSpeedCost.ToString();
            else
            {
                ColorBlock color;
                movementSpeedCostText.text = "MAXED OUT";
                color = movementSpeedUpgradeButton.colors;
                color.pressedColor = Color.red;
                movementSpeedUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void UpgradeJumps()
    {
        if (stats.gold >= gameSettings.jumpsCost && gameSettings.jumpsUpgradeBought < gameSettings.maxJumpsUpgrade)
        {
            playerStats.maxNumberOfJumps += gameSettings.jumpsUpgradeValue;
            currentJumpsValueText.text = "Jumps: " + playerStats.maxNumberOfJumps.ToString();
            gameSettings.jumpsUpgradeBought += 1;
            stats.gold -= gameSettings.jumpsCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.jumpsCost = (int)(gameSettings.baseJumpsCost * gameSettings.jumpsUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.jumpsUpgradeBought < gameSettings.maxJumpsUpgrade)
                jumpsCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.jumpsCost.ToString();
            else
            {
                ColorBlock color;
                jumpsCostText.text = "MAXED OUT";
                color = jumpsUpgradeButton.colors;
                color.pressedColor = Color.red;
                jumpsUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void UpgradeJumpForce()
    {
        if (stats.gold >= gameSettings.jumpForceCost && gameSettings.jumpForceUpgradeBought < gameSettings.maxJumpForceUpgrade)
        {
            playerStats.jumpForce += gameSettings.jumpForceUpgradeValue;
            currentJumpForceValueText.text = "Jump force: " + playerStats.jumpForce.ToString();
            gameSettings.jumpForceUpgradeBought += 1;
            stats.gold -= gameSettings.jumpForceCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.jumpForceCost = (int)(gameSettings.baseJumpForceCost * gameSettings.jumpForceUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.jumpForceUpgradeBought < gameSettings.maxJumpForceUpgrade)
                jumpForceCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.jumpForceCost.ToString();
            else
            {
                ColorBlock color;
                jumpForceCostText.text = "MAXED OUT";
                color = jumpForceUpgradeButton.colors;
                color.pressedColor = Color.red;
                jumpForceUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void UpgradeProjectileDuration()
    {
        if (stats.gold >= gameSettings.projectileDurationCost && gameSettings.projectileDurationUpgradeBought < gameSettings.maxProjectileDurationUpgrade)
        {
            playerStats.maxProjectileDuration += gameSettings.projectileDurationUpgradeValue;
            currentProjectileDurationValueText.text = "Projectile duration: " + playerStats.maxProjectileDuration.ToString();
            gameSettings.projectileDurationUpgradeBought += 1;
            stats.gold -= gameSettings.projectileDurationCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.projectileDurationCost = (int)(gameSettings.baseProjectileDurationCost * gameSettings.projectileDurationUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.projectileDurationUpgradeBought < gameSettings.maxProjectileDurationUpgrade)
                projectileDurationCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.projectileDurationCost.ToString();
            else
            {
                ColorBlock color;
                projectileDurationCostText.text = "MAXED OUT";
                color = projectileDurationUpgradeButton.colors;
                color.pressedColor = Color.red;
                projectileDurationUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void UpgradeProjectileSpeed()
    {
        if (stats.gold >= gameSettings.projectileSpeedCost && gameSettings.projectileSpeedUpgradeBought < gameSettings.maxProjectileSpeedUpgrade)
        {
            playerStats.projectileSpeed += gameSettings.projectileSpeedUpgradeValue;
            currentProjectileSpeedValueText.text = "Projectile speed: " + playerStats.projectileSpeed.ToString();
            gameSettings.projectileSpeedUpgradeBought += 1;
            stats.gold -= gameSettings.projectileSpeedCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.projectileSpeedCost = (int)(gameSettings.baseProjectileSpeedCost * gameSettings.projectileSpeedUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.projectileSpeedUpgradeBought < gameSettings.maxProjectileSpeedUpgrade)
                projectileSpeedCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.projectileSpeedCost.ToString();
            else
            {
                ColorBlock color;
                projectileSpeedCostText.text = "MAXED OUT";
                color = projectileSpeedUpgradeButton.colors;
                color.pressedColor = Color.red;
                projectileSpeedUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void UpgradeFireRate()
    {
        if (stats.gold >= gameSettings.fireRateCost && gameSettings.fireRateUpgradeBought < gameSettings.maxFireRateUpgrade)
        {
            playerStats.fireRate -= gameSettings.fireRateUpgradeValue;
            currentFireRateValueText.text = "Fire rate: " + playerStats.fireRate.ToString();
            gameSettings.fireRateUpgradeBought += 1;
            stats.gold -= gameSettings.fireRateCost;
            goldText.text = "Gold: " + stats.gold.ToString();
            gameSettings.fireRateCost = (int)(gameSettings.baseFireRateCost * gameSettings.fireRateUpgradeBought * gameSettings.settings.difficulty);
            if (gameSettings.fireRateUpgradeBought < gameSettings.maxFireRateUpgrade)
                fireRateCostText.text = "Upgrade<br>Gold cost<br>" + gameSettings.fireRateCost.ToString();
            else
            {
                ColorBlock color;
                fireRateCostText.text = "MAXED OUT";
                color = fireRateUpgradeButton.colors;
                color.pressedColor = Color.red;
                fireRateUpgradeButton.colors = color;
            }
            SetCostsButtonsHighlightColor();
        }
    }

    public void NextLevel()
    {
        stats.levelsCleared += 1;
        enemyStats.UpdateStats();
        if (stats.levelsCleared % 2 == 1) SceneManager.LoadScene("Level1");
        else SceneManager.LoadScene("Level2");
    }
}
