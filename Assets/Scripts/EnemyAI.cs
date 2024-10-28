using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class EnemyAI : MonoBehaviour
{
    public int speed;
    private GameObject bulletPrefab;
    private Transform firePos;

    void Start()
    {
        bulletPrefab = Resources.Load<GameObject>("Bullet");
        firePos = transform.GetChild(0);
        InvokeRepeating("Attack", 0, Random.Range(0.8f, 2.2f));
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
    
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed * 0.2f);
    }
    private void Attack()
    {
        GameObject tempBullet = Instantiate(bulletPrefab, firePos.position, firePos.rotation);
        tempBullet.AddComponent<BulletConterl>();
        tempBullet.name = "EnemyBullet";
    }

    public void OnTriggerEnter(Collider collision)
    {
        if (collision.tag == "EnemyWall")
        {
            Destroy(gameObject);
        }
        if (collision.tag == "Player")
        {
            GameObject.Find("GameManage").GetComponent<GameManage>().AttackCount(1);
            Destroy(gameObject);
        }


    }

}
