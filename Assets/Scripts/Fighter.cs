using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public GameObject Model;
    public float ForwardSpeed;
    public bool RotationLock;
    public float PitchAngle = 20.0f;

    private float PitchSpeed;
    public float DefaultPitchSpeed = 1;
    public float PowerPitchSpeed = 1;

    private Vector3 ForwardBeforeLock;
    public float ForwardAcceleration;
    public float MaxSpeed;
    public float MinSpeed;

    void Start()
    {
        RotationLock = false;
        PitchSpeed = DefaultPitchSpeed;
    }

    void Update()
    {
        HandleInput();
        if (!RotationLock)
        {
            this.transform.position += Model.transform.forward * Time.deltaTime * ForwardSpeed;
            ForwardBeforeLock = Model.transform.forward;
        }
        else
        {
            this.transform.position += ForwardBeforeLock * Time.deltaTime * ForwardSpeed;
        }
    }

    private void Switch()
    {
        RotationLock = !RotationLock;
        if (RotationLock)
            PitchSpeed = PowerPitchSpeed;
        else
            PitchSpeed = DefaultPitchSpeed;
    }


    private void HandleYaw()
    {

        float angle = Model.transform.localEulerAngles.z;
        angle %= 360;
        angle = angle > 180 ? angle - 360 : angle;
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            Vector3 eulers = Vector3.zero;
            if (Input.GetKey(KeyCode.A))
            {
                eulers = Model.transform.localEulerAngles + new Vector3(0, -PitchSpeed * Time.deltaTime, 0);
                if (angle < PitchAngle)
                    eulers += new Vector3(0, 0, Time.deltaTime * PitchSpeed);

            }
            else
            {
                if (Input.GetKey(KeyCode.D))
                {
                    eulers = Model.transform.localEulerAngles + new Vector3(0, PitchSpeed * Time.deltaTime, 0);
                    if (angle > -PitchAngle)
                        eulers -= new Vector3(0, 0, Time.deltaTime * PitchSpeed);

                }
            }
            Model.transform.rotation = Quaternion.RotateTowards(Model.transform.rotation, Quaternion.Euler(eulers), PitchSpeed * Time.deltaTime);
        }
        else
        {
            Vector3 eulers = new Vector3(Model.transform.localEulerAngles.x, Model.transform.localEulerAngles.y, 0);
            Model.transform.rotation = Quaternion.RotateTowards(Model.transform.rotation, Quaternion.Euler(eulers), PitchSpeed * Time.deltaTime);
        }
    }

    private void HandlePitch()
    {
        float angle = Model.transform.localEulerAngles.x;
        Vector3 eulers = Vector3.zero;
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow))
        {
            if (Input.GetKey(KeyCode.UpArrow))
                eulers = new Vector3(-PitchSpeed * Time.deltaTime, 0, 0);
            else
            {
                if (Input.GetKey(KeyCode.DownArrow))
                    eulers = new Vector3(PitchSpeed * Time.deltaTime, 0, 0);
            }
            Model.transform.Rotate(eulers);
        }
    }

    private void HandleRoll()
    {

    }

    private void HandleSpeed()
    {
        if (Input.GetKey(KeyCode.W))
        {
            if (ForwardSpeed + ForwardAcceleration * Time.deltaTime <= MaxSpeed)
                ForwardSpeed += ForwardAcceleration * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.S))
        {
            if (ForwardSpeed - ForwardAcceleration * Time.deltaTime >= MinSpeed)
                ForwardSpeed -= ForwardAcceleration * Time.deltaTime;
        }

    }


    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Switch();
        }

        HandleSpeed();
        HandleYaw();
        HandlePitch();
        HandleRoll();


    }
}
