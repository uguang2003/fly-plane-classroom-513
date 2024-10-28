using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickMove : MonoBehaviour
{
    Joystick joystick;
    Joystick joystickRot;
    public float speed = 1.5f;
    public float rotSpeed = 30f;

    PlaneControl planeControl;

    public bool isPlay = false;

    void Start()
    {
        planeControl = GameObject.Find("Plane").GetComponent<PlaneControl>();

        GameObject canvas = GameObject.FindGameObjectWithTag("UI");
        joystick = canvas.transform.Find("JoyStick").GetComponent<Joystick>();
        joystickRot = canvas.transform.Find("JoyStickRot").GetComponent<Joystick>();
    }
    void Update()
    {
        Vector2 joystickInput = joystick.GetInputDirection();
        Vector2 joystickRotInput = joystickRot.GetInputDirection();
        Move(joystickInput);
        Rotate(joystickRotInput);
    }

    void Move(Vector2 joystickInput)
    {
        transform.Translate(new Vector3(
            0,
            0,
            joystickInput.y * Time.deltaTime * speed));
        transform.Rotate(0, joystickInput.x * Time.deltaTime * rotSpeed, 0);
        //如果joystickInput与上一帧的joystickInput不同，说明飞机正在移动
        if (isPlay)
        {
            if (joystickInput != Vector2.zero)
            {
                if (!planeControl.planeMoveAudio.isPlaying)
                {
                    planeControl.planeMoveAudio.Play();
                    planeControl.StopPlaneNotMoveAudio();
                }
            }
            else
            {
                if (!planeControl.planeNotMoveAudio.isPlaying &&
                    !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) &&
                    !Input.GetKey(KeyCode.I) && !Input.GetKey(KeyCode.J) && !Input.GetKey(KeyCode.K) && !Input.GetKey(KeyCode.L))
                {
                    planeControl.planeMoveAudio.Stop();
                    planeControl.PlayPlaneNotMoveAudio();
                }
            }
        }
    }

    void Rotate(Vector2 joystickRotInput)
    {
        //transform.Translate(new Vector3(
        //    joystickRotInput.x * Time.deltaTime * speed,
        //    //joystickRotInput.y * Time.deltaTime * speed,
        //    0,
        //    0));
        transform.Rotate(joystickRotInput.y * Time.deltaTime * (-rotSpeed),
            joystickRotInput.x * Time.deltaTime * rotSpeed,
            0);
    }

}
