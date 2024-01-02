using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevelManager : MonoBehaviour
{
    [SerializeField] private int startingLevel = 1;
    [SerializeField] private int startingExperience = 0;
    [SerializeField] private PlayerData playerData;

    private int currentLevel;
    private int currentExperience;
    private void Awake()
    {
        currentLevel = startingLevel;
        currentExperience = startingExperience;
    }

    private void OnEnable()
    {
        GameEventsManager.instance.playerEvents.onExperienceGained += ExperienceGained;
    }

    private void OnDisable() 
    {
        GameEventsManager.instance.playerEvents.onExperienceGained -= ExperienceGained;
    }

    private void Start()
    {
        GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
        GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExperience);
    }

    private void ExperienceGained(int experience){
        currentExperience += experience;
        // check if we're ready to level up
        while (currentExperience >= playerData.expToLevelUp) 
        {
            currentExperience -= playerData.expToLevelUp;
            currentLevel++;
            GameEventsManager.instance.playerEvents.PlayerLevelChange(currentLevel);
        }
        GameEventsManager.instance.playerEvents.PlayerExperienceChange(currentExperience);
    }
}
