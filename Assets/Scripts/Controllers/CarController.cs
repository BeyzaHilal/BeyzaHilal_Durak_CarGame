using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]private Car _car;
    public Material shadowMaterial;
    
    public float speed;
    public float rotationSpeed;

    private Rigidbody _rigidbody;
    
    private int _stepCount;
    private int _numberOfSteps;

    public int carRotateDirection;
    
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        speed = LevelController.instance.carForwardSpeed;
        rotationSpeed = LevelController.instance.carRotateSpeed;
    }

    private void OnEnable()
    {
        if (_car.type == CarType.Shadow)
        {
            _stepCount = 0;
            _numberOfSteps = _car.steps.Count;
        }   
    }

    void FixedUpdate()
    {
        if (LevelController.instance.levelState == LevelController.LevelState.Playing)
        {
            switch (_car.states)
            {
                case CarStates.Playing:
                    // Car is driving by the user or shadow. 
                    switch (_car.type)
                    {
                        case CarType.Live:
                            // Move Forward
                            _rigidbody.velocity = transform.forward * speed;
                            
                            // User Left/Right Control
                            UserControlByKeyboard();

                            if (carRotateDirection != 0)
                            {
                                transform.Rotate(Vector3.up * carRotateDirection, rotationSpeed);
                            }

                            // Store steps
                            _car.steps.Add(new StepInTime(transform.position, transform.rotation));

                            break;

                        case CarType.Shadow:
                            if (_stepCount < _numberOfSteps)
                            {
                                transform.position = _car.steps[_stepCount].position;
                                transform.rotation = _car.steps[_stepCount].rotation;

                                _stepCount++;
                            }

                            break;
                    }

                    break;
                case CarStates.Success:
                    _rigidbody.velocity = Vector3.zero;
                    break;
                case CarStates.Fail:
                    _rigidbody.velocity = Vector3.zero;
                    break;
            }
        }
        else
        {
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }

    public void SetCar(Car _car)
    {
        this._car = _car;
    }

    public void UpdateCarState(CarStates state)
    {
        _car.states = state;
    }

    public void UpdateCarType(CarType type)
    {
        _car.type = type;
        gameObject.tag = "Shadow";
    }

    public void BeAShadow()
    {
        Relocate();
        UpdateCarType(CarType.Shadow);
        UpdateCarState(CarStates.Playing);
        UpdateMaterials();
    }

    private void UpdateMaterials()
    {
        foreach (Transform part in gameObject.transform)
        {
            if (part.gameObject.GetComponent<MeshRenderer>() != null)
            { 
                part.gameObject.GetComponent<MeshRenderer>().material = shadowMaterial; 
            }
        }
    }

    public void Replay()
    {
        Relocate();
        if (_car.type == CarType.Live) _car.steps.Clear();
        UpdateCarState(CarStates.Playing);
    }

    public void Relocate()
    {
        gameObject.SetActive(false);
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = _car.steps[0].position;
        transform.rotation = _car.steps[0].rotation;
        gameObject.SetActive(true);
    }

    private void UserControlByKeyboard()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, rotationSpeed);
        }
        
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.up * -1, rotationSpeed);
        }
    }
    
    private void OnCollisionEnter(Collision other)
    {
        if (_car.type == CarType.Live)
        {
            if (other.gameObject.CompareTag("Obstacles") || other.gameObject.CompareTag("Shadow"))
            {
                LevelController.instance.CarCrash();
            }

            if (other.gameObject.CompareTag("Target"))
            {
                LevelController.instance.CarReachToTarget();
            }
        }
    }
}
