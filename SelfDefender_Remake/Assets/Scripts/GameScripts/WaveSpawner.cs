using System.Collections;
using UnityEngine;
using UnityEngine.UI;


public class WaveSpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    public Transform SpawnPoint;
    public Text WaveText;

    public float timeBetweenWaves = 5f;
    private float countdown = 5f;
    [HideInInspector]public int waveIndex = 0;


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (countdown <= 0)
        {
           StartCoroutine(SpawnWave(EnemyPrefab));
            countdown = timeBetweenWaves;
        }
        countdown -= Time.deltaTime;
        WaveText.text = string.Format("Wave {1} - {0:0}", countdown, waveIndex+1);
    }

    IEnumerator SpawnWave(GameObject prefab)
    {
        waveIndex++;
        for (int i = 0; i < waveIndex; i++)
        {
            SpawnEnemy(prefab);
            yield return new WaitForSeconds(1f);
        }
    }

    void SpawnEnemy(GameObject prefab)
    {
        Instantiate(prefab, SpawnPoint.position, Quaternion.identity);
    }
}