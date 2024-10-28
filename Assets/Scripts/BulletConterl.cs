using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]

public class BulletConterl : MonoBehaviour
{

    private GameObject baozha;
    private AudioClip baozhaAudio;

    void Start()
    {
        baozha = Resources.Load<GameObject>("EX");
        baozhaAudio = Resources.Load<AudioClip>("BaoZhaAudio");
        GetComponent<BoxCollider>().isTrigger = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
    void Update()
    {
        transform.Translate(Vector3.up * Time.deltaTime * 5f);
    }

    public void OnTriggerEnter(Collider collision)
    {
        switch (collision.tag)
        {
            case "Player":
                if (gameObject.name == "EnemyBullet")
                {
                    GameObject.Find("GameManage").GetComponent<GameManage>().AttackCount(1); 
                    //Destroy(gameObject);
                }
                break;
            case "Enemy":
                if (gameObject.name == "PlayerBullet")
                {
                    #if UNITY_ANDROID
                    // 在安卓平台下执行的代码
                        Handheld.Vibrate();
                    #endif

                    GameObject.Find("GameManage").GetComponent<GameManage>().Score(1);
                    GameObject tempBaozha = Instantiate(baozha, gameObject.transform.position, gameObject.transform.rotation);
                    //播放爆炸音效
                    AudioSource.PlayClipAtPoint(baozhaAudio, gameObject.transform.position);
                    Destroy(gameObject);
                    Destroy(collision.gameObject);
                    Destroy(tempBaozha, 3);
                }
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            case "EnemyWall":
                Destroy(gameObject);
                break;
            default:
                /*if (this.name != collision.name)
                {
                    Destroy(gameObject);
                }*/
                break;
        }
    }
}
