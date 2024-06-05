using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SettingsJSON : MonoBehaviour
{
    [Header("Stats")]
    [NonSerialized] public float maxHealth;
    [NonSerialized] public float maxArmor;

    [Header("Jumping")]
    [NonSerialized] public float jumpForce;
    [NonSerialized] public int maxNumberOfJumps;
    [NonSerialized] public float jumpHeight;

    [Header("Moving")]
    [NonSerialized] public float speed;

    [Header("Down velocity")]
    [NonSerialized] public float fallForce;

    [Header("Shooting")]
    [NonSerialized] public float maxProjectileDuration;
    [NonSerialized] public float projectileSpeed;
    [NonSerialized] public float fireRate;

    [Header("Settings")]
    [SerializeField] public float volume;
    [SerializeField] public float difficulty;
    [SerializeField] public bool enemiesHealthbar;
    [SerializeField] public bool playerHealthBar;


    private void Awake()
    {
        fallForce = 0.75f;
        jumpHeight = 0.3f;

    }

    public void SetStatsBasedOnDifficulty()
    {
        switch (difficulty)
        {
            case 1:
                {
                    maxHealth = 150;
                    maxArmor = 50;
                    speed = 15;
                    maxNumberOfJumps = 2;
                    jumpForce = 20;
                    maxProjectileDuration = 3;
                    projectileSpeed = 3;
                    fireRate = 0.3f;
                }
                break;
            case 2:
                {
                    maxHealth = 80;
                    maxArmor = 20;
                    speed = 15;
                    maxNumberOfJumps = 1;
                    jumpForce = 15;
                    maxProjectileDuration = 4;
                    projectileSpeed = 2.5f;
                    fireRate = 0.3f;
                }
                break;
            case 3:
                {
                    maxHealth = 50;
                    maxArmor = 0;
                    speed = 10;
                    maxNumberOfJumps = 1;
                    jumpForce = 15;
                    maxProjectileDuration = 2;
                    projectileSpeed = 2;
                    fireRate = 0.4f;
                }
                break;
        }
    }
}
