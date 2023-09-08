using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Car : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float moveSpeedMultiplier = 1f;
    [SerializeField] float turnSpeed = 10f;


    int steerValue;

    void Start()
    {
        
    }


    void Update()
    {
        IncrementMoveSpeedOverTime();
        MoveForward();
        Rotation();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            SceneManager.LoadScene(0);
        }
    }


    void Rotation()
    {
        transform.Rotate(0f, turnSpeed * steerValue * Time.deltaTime, 0f);
    }

    public void Steer(int value)
    {
        steerValue = value;
    }


    void MoveForward()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    void IncrementMoveSpeedOverTime()
    {
        moveSpeed += moveSpeedMultiplier * Time.deltaTime;
    }
}
