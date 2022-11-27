using System;
using System.Collections;
using System.Collections.Generic;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Pool;

public class ItemSpawner : MonoBehaviour
{
    private FastRandom random = new FastRandom();

    [SerializeField] private GameObject[] objectsToSpawn;

    private Vector3 spawnPointVector3;
    [SerializeField] private Transform container;

    [SerializeField] private float startTimeBtwSpawns = 3f;
    [SerializeField] private float radius = 25f;
    private float timeBtwSpawns;

    // private void Awake()
    // {
    //     container = transform;
    //     pool = new ObjectPool<GameObject>(CreateFunc, actionOnGet: (obj) => obj.SetActive(true),
    //         actionOnRelease: (obj) => obj.SetActive(false), actionOnDestroy: (obj) => Destroy(obj), collectionCheck: true, defaultCapacity: 10, maxSize: 100);
    // }

    // private GameObject CreateFunc()
    // {
    //     var element = Instantiate(objectToSpawn, container);
    //
    //     return element;
    // }

    private void Start()
    {
        timeBtwSpawns = startTimeBtwSpawns;
        StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        while (timeBtwSpawns > 0)
        {
            timeBtwSpawns -= Time.deltaTime;
            yield return null;
        }

        timeBtwSpawns = startTimeBtwSpawns;
        foreach (var go in objectsToSpawn)
        {
            CreateElement(go);
        }

        StartCoroutine(Timer());
    }

    private void CreateElement(GameObject objectToCreate)
    {
        spawnPointVector3 = random.GetInsideCircle(radius);
        spawnPointVector3 += Player.playerTransform.position;

        var curItem = NightPool.Spawn(objectToCreate, spawnPointVector3);
        curItem.transform.SetParent(container);
    }
}