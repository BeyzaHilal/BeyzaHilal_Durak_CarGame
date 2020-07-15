using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    public enum LevelState
    {
        Preparing,
        Playing,
        CarFail,
        CarSuccess,
        WaitUserInput,
        LevelCompleted
    }

    public static LevelController instance = null;
    public LevelState levelState;
    private ObjectSpawner _objectSpawner;

    public float carForwardSpeed = 100;
    public float carRotateSpeed = 1;

    public Level level;
    public GameObject currentCar;
    private CarController _currentCarController;
    private int _currentCarNo;

    public List<GameObject> shadows;

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

        _objectSpawner = GetComponent<ObjectSpawner>();
    }

    private void OnValidate()
    {
        if (_currentCarController != null)
        {
            _currentCarController.speed = carForwardSpeed;
            _currentCarController.rotationSpeed = carRotateSpeed;
        }
    }
    
    public void LoadLevel(Level level)
    {
        if (level == null || level.startPoints.Count == 0) return;
        
        this.level = level;
        levelState = LevelState.Preparing;
        _objectSpawner.LoadCarSpawnPoints(level);
        
        _currentCarNo = 1; 
        shadows = new List<GameObject>();
        PrepareCar();
    }


    void Update()
    {
        switch (levelState)
        {
            case LevelState.WaitUserInput:
                if (Input.anyKey)
                {
                    // Time.timeScale = 1.0f;
                    levelState = LevelState.Playing;
                }
                break;

            case LevelState.CarSuccess:
                levelState = LevelState.Preparing;
                _currentCarController.UpdateCarState(CarStates.Success);
                GetNextCar();
                RelocateShadows();
                break;

            case LevelState.CarFail:
                levelState = LevelState.Preparing;
                _currentCarController.UpdateCarState(CarStates.Fail);
                ReloadCurrentCar();
                RelocateShadows();
                break;
        }
    }

    private void PrepareCar()
    {
        SetCurrentCar(_objectSpawner.CreateCar(_currentCarNo));
        GameController.instance.SetTextForCarNo(_currentCarNo);
        // Time.timeScale = 0.0f;
        levelState = LevelState.WaitUserInput;
    }

    private void GetNextCar()
    {
        if (!IsLevelCompleted())
        {
            _currentCarController.BeAShadow();
            shadows.Add(currentCar);
            _currentCarNo += 1;
            SetCurrentCar(_objectSpawner.CreateCar(_currentCarNo));
            GameController.instance.SetTextForCarNo(_currentCarNo);
            // Time.timeScale = 0.0f;
            levelState = LevelState.WaitUserInput;
        }
        else
        {
            levelState = LevelState.LevelCompleted;
            GameController.instance.LoadNextLevel();
        }
    }

    private void ReloadCurrentCar()
    {
        _currentCarController.Replay();
        // Time.timeScale = 0.0f;
        levelState = LevelState.WaitUserInput;
    }

    private void RelocateShadows()
    {
        foreach (GameObject car in shadows)
        {
            car.GetComponent<CarController>().Relocate();
        }
    }
    
    public void DestroyAllCars()
    {
        foreach (GameObject car in shadows)
        {
            Destroy(car);
        }
        Destroy(currentCar);
    }

    private bool IsLevelCompleted()
    {
        return _currentCarNo == GameController.instance.numberOfCarInLevel;
    }

    private void SetCurrentCar(GameObject car)
    {
        currentCar = car;
        _currentCarController = currentCar.GetComponent<CarController>();
    }

    public void StartDriveRotating(int direction)
    {
        _currentCarController.carRotateDirection = direction;
    }
    
    public void CarReachToTarget()
    {
        levelState = LevelState.CarSuccess;
    }

    public void CarCrash()
    {
        levelState = LevelState.CarFail;
    }
}
