using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float maxHealth;
    public float maxPatrolDistance;
    public float movementSeed;
    public float movementSeedAggro;
    public float viewDistance;
    public float contactDamage;
    public int goldGained;
    public int scoreGained;
    [SerializeField] private GameSettings gameSettings;
    [SerializeField] private GameStats gameStats;
    [SerializeField] private SettingsJSON settings;


    void Start()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        maxHealth = (20 * settings.difficulty * (1 + (float)gameStats.levelsCleared/3));
        maxPatrolDistance = (10 * settings.difficulty * (1 + (float)gameStats.levelsCleared/3));
        movementSeed = (4 * settings.difficulty * (1 + (float)gameStats.levelsCleared/3));
        movementSeedAggro = (7 * settings.difficulty * (1 + (float)gameStats.levelsCleared/3));
        viewDistance = (15 * settings.difficulty * (1 + (float)gameStats.levelsCleared/3));
        contactDamage = (5 * settings.difficulty * gameStats.levelsCleared);
        goldGained = (int)(50 / settings.difficulty);
        scoreGained = (int)(100 * settings.difficulty);
    }
}
