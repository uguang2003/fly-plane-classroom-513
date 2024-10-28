using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlaneControl : MonoBehaviour
{
    public float speed = 1.5f;//控制移动速度
    public float rotSpeed = 30f;//控制偏移速度


    private GameObject bullet;
    private Transform firePos;

    public int bulletNumber = 5;
    public float angel = 10;

    private AudioClip planeFireAudio;
    public AudioSource planeMoveAudio;
    public AudioSource planeNotMoveAudio;

    // 弹幕间隔时间
    public float currentTime = 0.15f;
    // 弹幕计时
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

    // Time.deltaTime 为每一帧的时间，为固定值。
    void Update()
    {
        //前进
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

        //后退
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
        //左转
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
        //右转
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
        //抬头
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
        //俯身
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
        //左侧翻身
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
        //右侧翻身
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
            // 按键按下时进行计时
            invokeTime += Time.deltaTime;
            // 间隔时间大于自定义时间才执行
            if (invokeTime - currentTime > 0)
            {
                // 实例化一次后计时归零
                Attack();
                invokeTime = 0;
            }
        }

        if (Input.GetKey(KeyCode.R))
        {
            // 按键按下时进行计时
            invokeTime += Time.deltaTime;
            // 间隔时间大于自定义时间才执行
            if (invokeTime - currentTime > 0)
            {
                // 实例化一次后计时归零
                RangeAttack();
                invokeTime = 0;
            }
        }

    }

    public void Attack()
    {
        //播放爆炸音效
        AudioSource.PlayClipAtPoint(planeFireAudio, gameObject.transform.position);
        GameObject tempBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        tempBullet.AddComponent<BulletConterl>();
        tempBullet.name = "PlayerBullet";
        Destroy(tempBullet, 5f);
    }

    public void RangeAttack()
    {
        //播放爆炸音效
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
