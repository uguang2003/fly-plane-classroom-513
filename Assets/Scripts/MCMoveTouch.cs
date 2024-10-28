using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 安卓控制器
/// </summary>

public class MCMoveTouch : MonoBehaviour, IDragHandler, IEndDragHandler
{

    Transform MC;
    Transform plane;

    void Start()
    {
        MC = GameObject.Find("MC").transform;
        plane = GameObject.Find("Plane").transform;
    }

    //旋转最大角度
    public int yRotationMinLimit = -20;
    public int yRotationMaxLimit = 80;
    //旋转速度
    public float xRotationSpeed = 60.0f;
    public float yRotationSpeed = 60.0f;
    //旋转角度
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public void OnDrag(PointerEventData evenntData)
    {
        xRotation -= evenntData.delta.x * xRotationSpeed * 0.02f;
        yRotation += evenntData.delta.y * yRotationSpeed * 0.02f;
        //MC.Rotate(new Vector3(-(evenntData.delta.y + MC.rotation.x), evenntData.delta.x + MC.rotation.y, 0));
        yRotation = ClampValue(yRotation, yRotationMinLimit, yRotationMaxLimit);//这个函数在结尾
        //欧拉角转化为四元数                                                                        
        Quaternion rotation = Quaternion.Euler(-yRotation, -xRotation + 180, 0);
        MC.rotation = rotation;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        yRotation = 0;
        xRotation = 0;
        //设置摄像头方向为Plane的方向
        MC.rotation = plane.rotation;
    }

    float ClampValue(float value, float min, float max)//控制旋转的角度
    {
        if (value < -360) value += 360;
        if (value > 360) value -= 360;
        return Mathf.Clamp(value, min, max);
        //限制value的值在min和max之间， 如果value小于min，返回min。 如果value大于max，返回max，否则返回value
    }

}

