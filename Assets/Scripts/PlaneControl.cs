using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public float speed = 1.5f;//�����ƶ��ٶ�
    public float rotSpeed = 30f;//����ƫ���ٶ�


    private GameObject bullet;
    private Transform firePos;

    public int bulletNumber = 5;
    public float angel = 10;

    private AudioClip planeFireAudio;
    public AudioSource planeMoveAudio;
    public AudioSource planeNotMoveAudio;

    // ��Ļ���ʱ��
    public float currentTime = 0.15f;
    // ��Ļ��ʱ
    private float invokeTime;

    void Start()
    {
        invokeTime = currentTime;
        planeFireAudio = Resources.Load<AudioClip>("PlaneFireAudio");
        planeMoveAudio = gameObject.AddComponent<AudioSource>();
        planeNotMoveAudio = gameObject.AddComponent<AudioSource>();
        planeMoveAudio.clip = Resources.Load<AudioClip>("PlaneMoveAudio");
        planeNotMoveAudio.clip = Resources.Load<AudioClip>("PlaneNotMoveAudio");
        planeMoveAudio.volume = 0.1f;
        planeNotMoveAudio.volume = 0.1f;

        planeNotMoveAudio.loop = true;

        bullet = Resources.Load<GameObject>("Bullet");
        firePos = this.transform.GetChild(0);

    }

    public void PlayPlaneNotMoveAudio()
    {
        planeNotMoveAudio.Play();
    }
    public void StopPlaneNotMoveAudio()
    {
        planeNotMoveAudio.Stop();
    }

    // Time.deltaTime Ϊÿһ֡��ʱ�䣬Ϊ�̶�ֵ��
    void Update()
    {
        //ǰ��
        if (Input.GetKey(KeyCode.W))
        {
            float translation = speed;
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }

        //����
        if (Input.GetKey(KeyCode.S))
        {
            float translation = -speed;
            transform.Translate(Vector3.back * Time.deltaTime * speed);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.S))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }
        //��ת
        if (Input.GetKey(KeyCode.J))
        {
            float rotation = -rotSpeed;
            transform.Translate(Vector3.left * Time.deltaTime * speed);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.J))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }
        //��ת
        if (Input.GetKey(KeyCode.L))
        {
            float rotation = rotSpeed;
            transform.Translate(Vector3.right * Time.deltaTime * speed);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.L))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }
        //̧ͷ
        if (Input.GetKey(KeyCode.I))
        {
            float rotation = -rotSpeed;
            transform.Rotate(rotation * Time.deltaTime, 0, 0);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.I))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }
        //����
        if (Input.GetKey(KeyCode.K))
        {
            float rotation = rotSpeed;
            transform.Rotate(rotation * Time.deltaTime, 0, 0);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.K))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }
        //��෭��
        if (Input.GetKey(KeyCode.A))
        {
            float rotation = -rotSpeed;
            transform.Rotate(0, rotation * Time.deltaTime, 0);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }
        //�Ҳ෭��
        if (Input.GetKey(KeyCode.D))
        {
            float rotation = rotSpeed;
            transform.Rotate(0, rotation * Time.deltaTime, 0);
            if (!planeMoveAudio.isPlaying)
            {
                planeMoveAudio.Play();
                planeNotMoveAudio.Stop();
            }
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            planeMoveAudio.Stop();
            planeNotMoveAudio.Play();
        }

        if (Input.GetKey(KeyCode.Space))
        {
            // ��������ʱ���м�ʱ
            invokeTime += Time.deltaTime;
            // ���ʱ������Զ���ʱ���ִ��
            if (invokeTime - currentTime > 0)
            {
                // ʵ����һ�κ��ʱ����
                Attack();
                invokeTime = 0;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            // ��������ʱ���м�ʱ
            invokeTime += Time.deltaTime;
            // ���ʱ������Զ���ʱ���ִ��
            if (invokeTime - currentTime > 0)
            {
                // ʵ����һ�κ��ʱ����
                RangeAttack();
                invokeTime = 0;
            }
        }

    }

    public void Attack()
    {
        //���ű�ը��Ч
        AudioSource.PlayClipAtPoint(planeFireAudio, gameObject.transform.position);
        GameObject tempBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        tempBullet.AddComponent<BulletConterl>();
        tempBullet.name = "PlayerBullet";
        Destroy(tempBullet, 5f);
    }

    public void RangeAttack()
    {
        //���ű�ը��Ч
        AudioSource.PlayClipAtPoint(planeFireAudio, gameObject.transform.position);
        for (int i = -bulletNumber / 2; i < bulletNumber / 2 + 1; i++)
        {
            for (int j = -bulletNumber / 2; j < bulletNumber / 2 + 1; j++)
            {
                GameObject tempBullet = Instantiate(bullet, firePos.position, firePos.rotation);
                tempBullet.transform.Rotate(new Vector3(j, 0, i), angel *
                    ((i == 0 && j == 0) ? 0 :
                    ((i == 0 ? (j < 0 ? -1 : 1) : i) * (j == 0 ? (i < 0 ? -1 : 1) : j))));
                tempBullet.AddComponent<BulletConterl>();
                tempBullet.name = "PlayerBullet";
                Destroy(tempBullet, 5f);
            }
        }
    }

}
