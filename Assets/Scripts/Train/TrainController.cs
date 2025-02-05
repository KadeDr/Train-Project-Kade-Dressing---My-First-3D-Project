using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class TrainController : Singleton<TrainController>
{
    
    [SerializeField] float moveForce;
    [SerializeField] float jumpForce;
    [SerializeField] float maxSpeed;
    [SerializeField] float xOffset;
    [SerializeField] float yOffset;
    [SerializeField] float zOffset;
    [SerializeField] float floatStrength;

    float moveInput;

    Rigidbody myrigidbody;

    protected override void Awake()
    {
        base.Awake();
        myrigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        PlayerInput();
    }

    void FixedUpdate()
    {
        Move();
    }

    void PlayerInput()
    {
        moveInput = Controls.Instance.trainMoveValue;
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Swap") return;
        for (int i = 0; i < other.gameObject.transform.parent.childCount; i++)
        {
            if (CheckChildName(other, i.ToString()))
            {
                SwapPlayers(other, i);
                break;
            }
        }
    }

    // Check the name of the child
    bool CheckChildName(Collider other, string childPos)
    {
        if (other.gameObject.name == childPos) {return false;} // Return false because we want the other child
        else {return true;} // Return true because we want the other child
    }

    void SwapPlayers(Collider other, int childPos)
    {
        Controls.Instance.currentPlayerObject = other.gameObject.transform.parent.GetChild(childPos).GetChild(0).gameObject.name;
        print(Controls.Instance.currentPlayerObject);
        CameraFollow.Instance.objectToFollow = other.gameObject;
        for (int i = 0; i < Controls.Instance.player.transform.childCount; i++)
        {
            if (CheckChildName2(Controls.Instance.player.transform.GetChild(i)))
            {
                CameraFollow.Instance.objectToFollow = Controls.Instance.player.transform.GetChild(i).gameObject;
                break;
            }
        }
    }

    bool CheckChildName2(Transform other)
    {
        if (other.name == Controls.Instance.currentPlayerObject) {return true;}
        else {return false;}
    }

    public void Jump()
    {
        if (Controls.Instance.currentPlayerObject != "Train") return;
        myrigidbody.AddRelativeForce(Vector3.up * jumpForce, ForceMode.Force);
    }

    void Move()
    {
        myrigidbody.AddRelativeForce(Vector3.right * moveInput * moveForce, ForceMode.Force);
        print("Moving!");
        print(moveInput);

        if (myrigidbody.velocity.magnitude > maxSpeed)
        {
            myrigidbody.velocity = myrigidbody.velocity.normalized * maxSpeed;
        }
    }
}
