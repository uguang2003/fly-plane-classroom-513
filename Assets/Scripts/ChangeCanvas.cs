using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeCanvas : MonoBehaviour 
{
    public GameObject CanvasOn;
    public GameObject CanvasOff;

    public bool isPause = false;
    public bool isRebegin = false;
    public bool isMovePlane = false;

    PlaneControl planeControl;
    JoystickMove joystickMove;

    private void Start()
    {
        planeControl = GameObject.Find("Plane").GetComponent<PlaneControl>();
        joystickMove = GameObject.Find("Plane").GetComponent<JoystickMove>();

    }

    public void changeCanvas()
    {
        if (isMovePlane)
        {
            joystickMove.isPlay = true;
            planeControl.PlayPlaneNotMoveAudio();
        }else
        {
            joystickMove.isPlay = false;
            planeControl.StopPlaneNotMoveAudio();
        }

        if (CanvasOn.IsUnityNull())
        {

        }
        else
        {
            CanvasOn.SetActive(true);
        }

        CanvasOff.SetActive(false);
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }

        if (isRebegin)
        {
            SceneManager.LoadScene("SampleScene");
        }
    }



}
