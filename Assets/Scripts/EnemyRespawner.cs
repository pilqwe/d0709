using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyRespawner : MonoBehaviour
{
    public GameObject[] Enemies;

    float[] arrPosx = { -2f, 0f, 2f };

    [SerializeField]

    float spawnInterval = 0.5f;
    float moveSpeed = 5f;

    public Transform spawnPosition; // 적이 생성될 위치

    int curretEnemyIndex = 0; // 현재 생성된 적의 인덱스

    int spawncount = 0;


    void Start()
    {
        StartCoroutine("EnemyRoutine");

    }

    IEnumerator EnemyRoutine()
    {
        yield return new WaitForSeconds(3);

        while (true)
        {
            for (int i = 0; i < arrPosx.Length; i++)
            {
                SpawnEnemy(arrPosx[i], curretEnemyIndex, moveSpeed);
            }

            spawncount++;

            if (spawncount % 2 == 0)
            {
                curretEnemyIndex++;
                if (curretEnemyIndex >= Enemies.Length)
                {
                    curretEnemyIndex = Enemies.Length - 1; // 인덱스가 배열 길이를 초과하지 않도록 초기화
                }
                moveSpeed += 2;
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Update is called once per frame
    void SpawnEnemy(float posX, int index, float moveSpeed)
    {
        Vector3 spawnPos = new Vector3(posX, spawnPosition.position.y, spawnPosition.position.z);

        GameObject enemyObject = Instantiate(Enemies[index], spawnPos, Quaternion.identity);
        Enemy enemy = enemyObject.GetComponent<Enemy>();
        enemy.SetMoveSpeed(moveSpeed); // 적의 이동 속도 설정
    }



}
