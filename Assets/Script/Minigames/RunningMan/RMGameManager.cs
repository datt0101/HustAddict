using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RMGameManager : MonoBehaviour
{
    public static RMGameManager instance;
    public int score;
    public Transform spawnPoint;
    public GameObject obstacle;
    private void Awake()
    {
        instance = this;
        score = 0;
    }
    private void Start()
    {
        InvokeRepeating("SpawnObstacle", 1f, 2f);
        InvokeRepeating("ScoreUp", 1f, 2f);
    }
    void SpawnObstacle()
    {
        float rand = Random.Range(-8, 8);
        GameObject Ob = Instantiate(obstacle);
        Ob.transform.position = new Vector3(spawnPoint.position.x, spawnPoint.position.y, spawnPoint.position.z + rand);
    }
    void ScoreUp()
    {
        score++;
        UI.instance.scoreText.text = "Score: " + score.ToString();
    }
    public void StopInvoke()
    {
        CancelInvoke();
    }
}
