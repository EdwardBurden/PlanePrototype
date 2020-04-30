using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterCamera : MonoBehaviour
{
    public Camera Cam;
    public GameObject CameraPosiion;
    public GameObject FighterModel;
    public Fighter Fighter;
    public float LookLength;

    public void Update()
    {
        if (!Fighter.RotationLock)
        {
            this.transform.position = Vector3.Lerp(this.transform.position, CameraPosiion.transform.position, Time.deltaTime);
            this.transform.LookAt((FighterModel.transform.position + (FighterModel.transform.forward * LookLength)));
        }
    }
}
