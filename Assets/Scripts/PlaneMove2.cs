using UnityEngine;
using System.Collections;

public class PlaneMove2 : MonoBehaviour
{

    float speed = 2.0f;   //�ƶ��ٶ�
    float rotationSpeed = 60.0f;  //��ת�ٶ�
    void Update()
    {
        // ʹ�����·��������W��S��������ǰ������
        float translation = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        //ʹ�����ҷ��������A��D��������������ת
        float rotation = Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;

        transform.Translate(0, 0, translation); //����Z���ƶ�
        transform.Rotate(0, rotation, 0); //��Y����ת
    }

}
