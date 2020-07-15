using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject carPrefab;
    public GameObject startPointPrefab;
    public GameObject targetPointPrefab;
    public GameObject obstaclePrefab;
    
    public List<GameObject> obstacles;
    
    private Level _level;
    private GameObject _currentEntrance;
    private GameObject _currentExit;

    public void LoadCarSpawnPoints(Level level)
    {
        if (_level != null) ClearOldLevelData();
        
        _level = level;
        CreateObstacle();

        _currentEntrance = Instantiate(startPointPrefab);
        _currentEntrance.SetActive(false);
        
        _currentExit = Instantiate(targetPointPrefab);
        _currentExit.SetActive(false);
    }

    private void ClearOldLevelData()
    {
        ClosePreviousPoints();
        obstacles.RemoveAll(o =>
        {
            Destroy(o);
            return true;
        });
        _level = null;
    }

    public GameObject CreateCar(int carNo)
    {
        int index = carNo - 1;
        if (index - 1 >= 0) ClosePreviousPoints();
        ShowCurrentPoints(index);

        GameObject car = Instantiate(carPrefab, _level.startPoints[index].position,
            _level.startPoints[index].rotation);
        car.GetComponent<CarController>().SetCar(new Car(carNo, CarType.Live));
        return car;

    }

    private void CreateObstacle()
    {
        _level.obstacles.ForEach((pointData =>
        {
            obstacles.Add(Instantiate(obstaclePrefab, pointData.position, pointData.rotation));
        }));
    }
    
    private void ShowCurrentPoints(int index)
    {
        _currentEntrance.transform.position = _level.startPoints[index].position;
        _currentEntrance.transform.rotation = _level.startPoints[index].rotation;
        _currentEntrance.SetActive(true);
        
        _currentExit.transform.position = _level.targetPoints[index].position;
        _currentExit.transform.rotation = _level.targetPoints[index].rotation;
        _currentExit.SetActive(true);
    }
    
    private void ClosePreviousPoints()
    {
        _currentEntrance.SetActive(false);
        _currentExit.SetActive(false);
    }
    

}
