using UnityEngine;
using System;
using System.Collections.Generic;

public enum CarType
{
    Live,
    Shadow
}

public enum CarStates
{
    Playing,
    Success,
    Fail
}

[Serializable]
public class Car
{
    public int carId;
    public CarType type;
    public CarStates states;

    public List<StepInTime> steps;

    public Car(int _carId, CarType _type)
    {
        carId = _carId;
        type = _type;
        steps = new List<StepInTime>();
    }

}

public class StepInTime
{
    public Vector3 position;
    public Quaternion rotation;

    public StepInTime(Vector3 position, Quaternion rotation)
    {
        this.position = position;
        this.rotation = rotation;
    }
}
