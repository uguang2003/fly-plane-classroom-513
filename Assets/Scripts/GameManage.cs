using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManage : MonoBehaviour
{
    private GameObject[] enemys;

    private int score;
    private int attackCount;
    private static Text txt;

    public GameObject CanvasOn;
    public GameObject CanvasOff;

    public bool isPause = false;

    void Start()
    {
        Time.timeScale = 0;
        Application.targetFrameRate = 120;
        txt = GameObject.Find("Score").GetComponent<Text>();
        enemys = Resources.LoadAll<GameObject>("Enemys");
        InvokeRepeating("CreateEnemy", 0, Random.Range(0.3f, 3f));
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            changeCanvas();
        }
    }

    private void CreateEnemy()
    {
        int num = Random.Range(0, enemys.Length);
        GameObject enemy = Instantiate(enemys[num], new Vector3(Random.Range(-2.8f, 2.8f), Random.Range(0.2f, 2.8f), -4.5f), Quaternion.identity);
        enemy.AddComponent<EnemyAI>();
        enemy.GetComponent<EnemyAI>().speed = Random.Range(2, 8);
        enemy.tag = "Enemy";
        Destroy(enemy, 40);
    }

    public void Score(int score)
    {
        this.score += score;
        txt.text = "得分: " + this.score + "\n" + "被打中次数: " + this.attackCount;
    }
    public void AttackCount(int attackCount)
    {
        this.attackCount += attackCount;
        txt.text = "得分: " + this.score + "\n" + "被打中次数: " + this.attackCount;
    }

    public void OnExitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void changeCanvas()
    {
        CanvasOn.SetActive(true);
        CanvasOff.SetActive(false);
        if (isPause)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

}
