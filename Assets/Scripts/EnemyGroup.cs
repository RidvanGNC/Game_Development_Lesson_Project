using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroup : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private EnemyController enemyPrefab;

    [Header("Setting")]
    [SerializeField] private int amount;
    [SerializeField] private float radius;
    [SerializeField] private float angel;
    void Start()
    {
        EnemyGenerate();
    }

    void Update()
    {
        
    }

    private void EnemyGenerate()
    {
        for (int i = 0; i < amount; i++)
        {
            Vector3 enemyLocalPosition = PlayerRunnerLocalPosition(i);
            Vector3 enemyWorldPosition = transform.TransformPoint(enemyLocalPosition);
            Instantiate(enemyPrefab, enemyWorldPosition, Quaternion.identity, transform);
        }
    }

    private Vector3 PlayerRunnerLocalPosition(int index)
    {
        float x = radius * Mathf.Sqrt(index) * Mathf.Cos(Mathf.Deg2Rad * index * angel);
        float z = radius * Mathf.Sqrt(index) * Mathf.Sin(Mathf.Deg2Rad * index * angel);
        return new Vector3(x, 0, z);
    }
}
