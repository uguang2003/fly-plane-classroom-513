using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ��׿������
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

    //��ת���Ƕ�
    public int yRotationMinLimit = -20;
    public int yRotationMaxLimit = 80;
    //��ת�ٶ�
    public float xRotationSpeed = 60.0f;
    public float yRotationSpeed = 60.0f;
    //��ת�Ƕ�
    private float xRotation = 0.0f;
    private float yRotation = 0.0f;

    public void OnDrag(PointerEventData evenntData)
    {
        xRotation -= evenntData.delta.x * xRotationSpeed * 0.02f;
        yRotation += evenntData.delta.y * yRotationSpeed * 0.02f;
        //MC.Rotate(new Vector3(-(evenntData.delta.y + MC.rotation.x), evenntData.delta.x + MC.rotation.y, 0));
        yRotation = ClampValue(yRotation, yRotationMinLimit, yRotationMaxLimit);//��������ڽ�β
        //ŷ����ת��Ϊ��Ԫ��                                                                        
        Quaternion rotation = Quaternion.Euler(-yRotation, -xRotation + 180, 0);
        MC.rotation = rotation;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        yRotation = 0;
        xRotation = 0;
        //��������ͷ����ΪPlane�ķ���
        MC.rotation = plane.rotation;
    }

    float ClampValue(float value, float min, float max)//������ת�ĽǶ�
    {
        if (value < -360) value += 360;
        if (value > 360) value -= 360;
        return Mathf.Clamp(value, min, max);
        //����value��ֵ��min��max֮�䣬 ���valueС��min������min�� ���value����max������max�����򷵻�value
    }

}

