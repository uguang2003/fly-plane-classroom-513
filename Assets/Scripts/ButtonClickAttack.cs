using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClickAttack : MonoBehaviour
{

    private GameObject bullet;
    private Transform firePos;

    public int bulletNumber = 5;
    public float angel = 10;

    private AudioClip planeFireAudio;

    public float currentTime = 0.15f;

    private void Start()
    {
        planeFireAudio = Resources.Load<AudioClip>("PlaneFireAudio");
    }

    public void Attack()
    {
         Handheld.Vibrate();

        //≤•∑≈±¨’®“Ù–ß
        AudioSource.PlayClipAtPoint(planeFireAudio, gameObject.transform.position);

        bullet = Resources.Load<GameObject>("Bullet");
        firePos = GameObject.Find("Plane").transform.GetChild(0);

        GameObject tempBullet = Instantiate(bullet, firePos.position, firePos.rotation);
        tempBullet.AddComponent<BulletConterl>();
        tempBullet.name = "PlayerBullet";
        Destroy(tempBullet, 5f);
    }

    public void RangeAttack()
    {
        Handheld.Vibrate();
        //≤•∑≈±¨’®“Ù–ß
        AudioSource.PlayClipAtPoint(planeFireAudio, gameObject.transform.position);
        for (int i = -bulletNumber / 2; i < bulletNumber / 2 + 1; i++)
        {
            for (int j = -bulletNumber / 2; j < bulletNumber / 2 + 1; j++)
            {
                bullet = Resources.Load<GameObject>("Bullet");
                firePos = GameObject.Find("Plane").transform.GetChild(0);
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
