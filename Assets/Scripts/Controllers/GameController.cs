using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance = null;
    private UIController _uiController;

    public int numberOfCarInLevel = 8;
    public int currentLevelNumber = 1;
    public int maxLevel = 2;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
        
        // Fixed frame rate to low CPU load 
        // Application.targetFrameRate = 30;
        // QualitySettings.vSyncCount = 0;
    }

    private void Start()
    {
        _uiController = GetComponent<UIController>();

        LevelController.instance.LoadLevel(ReadCurrentLevel());
        SetTextForLevelNo();
    }

    private Level ReadCurrentLevel()
    {
        Debug.Log("Reading Level...");
        var level = (Level) AssetDatabase.LoadAssetAtPath($"{PrefKeys.AssetsPath}Level_{currentLevelNumber}.asset", typeof(Level));
        return level;
    }
    
    
    public void SetTextForCarNo(int carNo)
    {
        _uiController.DisplayCarNo(carNo,numberOfCarInLevel);
    }

    public void SetTextForLevelNo()
    {
        _uiController.DisplayLevelNo(currentLevelNumber);
    }

    public void LoadNextLevel()
    {
        if (currentLevelNumber + 1 <= maxLevel)
        {
            currentLevelNumber += 1;
            LevelController.instance.DestroyAllCars();
            PlayerPrefs.SetInt(PrefKeys.PlayerPrefUnlockLevel, currentLevelNumber);
            _uiController.DisplayLevelNo(currentLevelNumber);
            _uiController.DisplayCarNo(1, numberOfCarInLevel);
            _uiController.DisplayLevelUp();
            LevelController.instance.LoadLevel(ReadCurrentLevel());
        }
        else
        {
            _uiController.DisplayGameFinishText();
        }
    }

}
