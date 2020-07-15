using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public static class PrefKeys
{
    public const string ExistsLevelNumber = "ExistsLevelNumber";
    public const string PlayerPrefUnlockLevel = "UnlockLevel";
    public const string AssetsPath = "Assets/Levels/";

}

[Serializable]
public class PointData
{
    public Vector3 position;
    public Quaternion rotation;

    public PointData(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}

[CreateAssetMenu(fileName = "New Level", menuName = "Car Game/Create New Level")]
public class Level : ScriptableObject
{
    public int levelNumber;

    public List<PointData> startPoints = new List<PointData>();
    public List<PointData> targetPoints = new List<PointData>();
    public List<PointData> obstacles = new List<PointData>();
}