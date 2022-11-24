using System;
using System.Collections;
using NTC.Global.Pool;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    [FormerlySerializedAs("enemy")] [SerializeField]
    private GameObject[] enemies; // массив типов врагов

    private int countSpawnEnemies = 1;
    private int maxCountSpawnEnemies = 15;

    [SerializeField] private Transform container;
    private Vector3 spawnPoint; // точка спавна
    private GameObject curEnemy; //Текущий враг
    private GameObject parentPoint; // родительский объект, используется для позиционирования точек
    private GameObject targetAroundRotate; //Цель, вокруг которого будет произведено вращение
    private float startPositionY; // стартовая позиция по Y (transform.position)
    private Vector3 v3Start; // стартовая позиция (Все координаты)

    private const int PointsResetLimit = 30; // через какое кол-во спавнов обнуляется точка спавна
    private int resetCounter; //нумератор для обнуления

    [SerializeField] private float angle; //угол поворота для вращения спавнера
    public float startTimeBtwSpawns; //промежуток спавна врагов
    private float timeBtwSpawns; //уменьшающиеся время спавна врагов (таймер)

    private FastRandom random = new FastRandom();

    [SerializeField] private int minuteCountToAvoidPreviousEnemy;
    [SerializeField] private int countOfEnemiesForRandom;
    [SerializeField] private int countOfAvoidEnemies;
    private float zAngleEnemy;

    public TimeManager timeManagerScr;

    private const float DecreaseTimeArithmetic = -0.05f;
    private const float DecreaseTimeGeometric = 0.9f;

    [FormerlySerializedAs("typeOfTimeProgress")]
    public TypeOfTimeDecrease typeOfTimeDecrease;

    public enum TypeOfTimeDecrease
    {
        Arithmetic,
        Geometric
    };

    private int poolDestroyIndex;
    [SerializeField] private float startTimeToDestroyPool;
    [SerializeField] private float timeToDestroyPool;
    private const int SecondsInMinute = 60;

    private void Start()

    {
        startPositionY = transform.localPosition.y; //присвоение стартовой позиции по Y (transform.position)
        v3Start = new Vector3(0, startPositionY, 0); // стартовая позиция (Все координаты)
        targetAroundRotate = Player.playerGameObject;

        StartCoroutine(EnemySpawnTimer()); //заупскаем таймер первый раз(спусковой, дальше он сам себя будет вызывать)
        StartCoroutine(DestroyPoolTimer());
    }

    private void IncreaseCountSpawnEnemies()
    {
        if (countSpawnEnemies < maxCountSpawnEnemies)
        {
            countSpawnEnemies++;
        }
    }

    private void CheckTimeToSpawnDecrease()
    {
        if (timeManagerScr.minuteCounter == timeManagerScr.gameTime.Minute) return;
        switch (typeOfTimeDecrease)
        {
            case TypeOfTimeDecrease.Arithmetic:
                if (startTimeBtwSpawns > DecreaseTimeArithmetic)
                {
                    startTimeBtwSpawns += DecreaseTimeArithmetic;
                }
                else
                {
                    IncreaseCountSpawnEnemies();
                }

                break;

            case TypeOfTimeDecrease.Geometric:
                if (startTimeBtwSpawns > 0.1f)
                {
                    startTimeBtwSpawns *= DecreaseTimeGeometric;
                }
                else
                {
                    IncreaseCountSpawnEnemies();
                }

                break;
            default:
                Debug.LogError("Не выставлено значение прогрессии времени спавна");
                throw new ArgumentOutOfRangeException();
        }

        timeManagerScr.minuteCounter++;
    }

    private GameObject GetRandomEnemyType()
    {
        countOfAvoidEnemies = timeManagerScr.minuteCounter / minuteCountToAvoidPreviousEnemy;
        //var index = TrueRandom.Rnd() % (countOfEnemiesForRandom) + countOfAvoidEnemies;
        var index = random.Range(0, countOfEnemiesForRandom) + countOfAvoidEnemies;

        if (index + 1 <= countOfAvoidEnemies)
        {
            index = countOfAvoidEnemies;
        }

        if (index >= enemies.Length)
        {
            index = enemies.Length - 1;
        }

        return enemies[index];
    }

    // ReSharper disable Unity.PerformanceAnalysis
    // ReSharper disable once FunctionRecursiveOnAllPaths
    private IEnumerator EnemySpawnTimer() //корутина таймера
    {
        timeBtwSpawns = startTimeBtwSpawns; //обнуляем таймер создания крипа
        while (timeBtwSpawns > 0) //если время больше 0, то уменьшаем его по чуть-чуть
        {
            timeBtwSpawns -= Time.deltaTime; //само уменьшение таймера
            yield return null; //продолжить выполнение после этого кадра
        }

        for (var i = 1; i <= countSpawnEnemies; i++)
        {
            SpawnEnemies();
        }

        CheckPointReset();
        CheckTimeToSpawnDecrease();

        StartCoroutine(EnemySpawnTimer()); //перезапуск таймера спавна
    }

    private void SpawnEnemies()
    {
        spawnPoint = gameObject.transform.position;
        zAngleEnemy = random.Range(0f, 350f); // рандомный добавочный угол через быстрый рандом для поворота врага

        //curEnemy = NightPool.Spawn(GetRandomEnemyType(), spawnPoint);
        curEnemy = NightPool.Spawn(GetRandomEnemyType(), spawnPoint, Quaternion.Euler(0f, 0f, zAngleEnemy));

        curEnemy.transform.SetParent(container);

        RotatePoint();
    }

    private void RotatePoint()
    {
        var angleRandom =
            random.Range(-40f, 40f); // рандомный добавочный угол через быстрый рандом для вращения точки спавна
        transform.RotateAround(targetAroundRotate.transform.position, Vector3.forward,
            angle + angleRandom); // вращение точки спавна
    }

    private void CheckPointReset()
    {
        resetCounter++; //глобальный счетчик +1

        if (resetCounter != PointsResetLimit) return; //проверка счетчика
        transform.localPosition = v3Start;
        resetCounter %= PointsResetLimit; //обнуление счетчика
    }

    private IEnumerator DestroyPoolTimer()
    {
        startTimeToDestroyPool = (minuteCountToAvoidPreviousEnemy + 2) * SecondsInMinute;
        timeToDestroyPool = startTimeToDestroyPool; //обнуляем таймер 

        while (timeToDestroyPool > 0) //если время больше 0, то уменьшаем его по чуть-чуть
        {
            timeToDestroyPool -= Time.deltaTime; //само уменьшение таймера
            yield return null; //продолжить выполнение после этого кадра
        }

        DestroyEnemyPool();
    }

    private void DestroyEnemyPool()
    {
        if (poolDestroyIndex == enemies.Length) return;
        
        print(poolDestroyIndex);
        var gameObjectOfPoolBeingDestroyed = NightPool.Spawn(enemies[poolDestroyIndex]);
        NightPool.DestroyPool(gameObjectOfPoolBeingDestroyed);
        Destroy(gameObjectOfPoolBeingDestroyed);
        poolDestroyIndex++;

        if (poolDestroyIndex >= enemies.Length) return;
        StartCoroutine(DestroyPoolTimer());
    }
}