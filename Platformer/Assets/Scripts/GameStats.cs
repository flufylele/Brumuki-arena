using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int enemiesKilled;
    public int shotsFired;
    public float timeSruvived;
    public int gold;
    public int score;
    public int levelsCleared;

    private void Start()
    {
        score = 0;
        levelsCleared = 1;
    }

    public void ResetStats()
    {
        enemiesKilled = 0;
        shotsFired = 0;
        timeSruvived = 0;
        gold = 0;
        score = 0;
        levelsCleared = 1;
    }
}
