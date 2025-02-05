using System.Collections;
using JetBrains.Annotations;
using Unity.IO.LowLevel.Unsafe;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class Controls : Singleton<Controls> 
{
    PlayerInputs playerInputs;
    [HideInInspector] public float trainMoveValue {get; private set;}
    [HideInInspector] public float rocketThrustValue {get; private set;}
    [HideInInspector] public float rocketTorqueValue {get; private set;}
    public GameObject player;
    public string currentPlayerObject;

    protected override void Awake()
    {
        base.Awake();
        playerInputs = new PlayerInputs();
    }

    void OnEnable()
    {
        playerInputs.Enable();
        playerInputs.Rocket.Thrust.Enable();
        playerInputs.Rocket.Turn.Enable();
        playerInputs.Train.Move.Enable();
    }

    void AdjustCameraSettings(float xOffset, float yOffset, float zOffset, float floatStrength)
    {
        CameraFollow.Instance.xOffset = xOffset;
        CameraFollow.Instance.yOffset = yOffset;
        CameraFollow.Instance.zOffset = zOffset;
        CameraFollow.Instance.floatStrength = floatStrength;
    }

    void Start()
    {
        Train();
    }

    void Update()
    {
        Rocket();
        TrainUpdate();
        Debug.Log(trainMoveValue);
    }

    void Rocket()
    {
        if (currentPlayerObject != "Rocket") {DisableRocket(); return;}
        AdjustCameraSettings(0, 2, -20, 0.1f);
        rocketThrustValue = playerInputs.Rocket.Thrust.ReadValue<float>();
        rocketTorqueValue = playerInputs.Rocket.Turn.ReadValue<float>();
    }

    void DisableRocket()
    {
        rocketThrustValue = 0;
        rocketTorqueValue = 0;
    }

    void Train()
    {
        playerInputs.Train.Jump.performed += _ => TrainController.Instance.Jump();
    }

    void TrainUpdate()
    {
        if (currentPlayerObject != "Train") {DisableTrain(); return;}
        AdjustCameraSettings(3.5f, 3, -7, 0.025f);
        trainMoveValue = playerInputs.Train.Move.ReadValue<float>();
    }

    void DisableTrain()
    {
        trainMoveValue = 0;
    }

}