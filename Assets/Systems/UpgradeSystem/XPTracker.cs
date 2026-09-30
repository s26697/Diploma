using System;
using UnityEngine;

public class XPTracker
{
    public event Action<int> OnLevelUp; 

    private float baseXpToLevel;
    private float currentXpToLevel;
    private float currentXP;
    private int currentLevel;

    private float levelMultiplier;

    public XPTracker(float baseXpToLevel, float levelMultiplier = 1.25f)
    {
        this.baseXpToLevel = baseXpToLevel;
        this.levelMultiplier = levelMultiplier;

        currentXpToLevel = baseXpToLevel;
    }

    public void AddXP(float amount)
    {
        currentXP += amount;

        while (currentXP >= currentXpToLevel)
        {
            currentXP -= currentXpToLevel;
            LevelUp();
        }
    }

    private void LevelUp()
    {
        currentLevel++;
        currentXpToLevel = baseXpToLevel * Mathf.Pow(levelMultiplier, currentLevel);

        OnLevelUp?.Invoke(currentLevel);
    }

    public int CurrentLevel => currentLevel;
    
}