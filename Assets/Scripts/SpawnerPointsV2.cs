using System;
using System.Collections;
using NTC.Global.Pool;
using UnityEngine;

public class SpawnerPointsV2 : MonoBehaviour
{
    [SerializeField] private GameObject[] enemy; // массив типов врагов
    [NonSerialized] private int countSpawnEnemies = 1;
    public int maxCountSpawnEnemies = 20;

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
    private int countOfAvoidEnemies;
    private float zAngleEnemy;

    public TimeManager timeManagerScr;

    private const float ScaleTimeArithmetic = -0.05f;
    private const float ScaleTimeGeometric = 0.9f;

    public TypeOfTimeProgress typeOfTimeProgress;

    public enum TypeOfTimeProgress
    {
        Arithmetic,
        Geometric
    };

    private void Start()

    {
        startPositionY = transform.localPosition.y; //присвоение стартовой позиции по Y (transform.position)
        v3Start = new Vector3(0, startPositionY, 0); // стартовая позиция (Все координаты)
        targetAroundRotate = Player.playerGameObject;
        timeBtwSpawns = startTimeBtwSpawns; //ставим таймер в ноль, чтобы он заупстился
        StartCoroutine(StartTimerEnemy()); //заупскаем таймер первый раз(спусковой, дальше он сам себя будет вызывать)
    }

    private void IncreaseCountSpawnEnemies()
    {
        if (countSpawnEnemies < maxCountSpawnEnemies)
        {
            countSpawnEnemies++;
        }
    }

    private void CheckIfTimeScale()
    {
        if (timeManagerScr.minuteCounter == timeManagerScr.gameTime.Minute) return;
        switch (typeOfTimeProgress)
        {
            case TypeOfTimeProgress.Arithmetic:
                if (startTimeBtwSpawns > ScaleTimeArithmetic)
                {
                    startTimeBtwSpawns += ScaleTimeArithmetic;
                }
                else
                {
                    IncreaseCountSpawnEnemies();
                }

                break;

            case TypeOfTimeProgress.Geometric:
                if (startTimeBtwSpawns > 0.1f)
                {
                    startTimeBtwSpawns *= ScaleTimeGeometric;
                }
                else
                {
                    IncreaseCountSpawnEnemies();
                }

                break;
            default:
                print("Не выставлено значение прогрессии времени спавна");
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

        if (index >= enemy.Length)
        {
            index = enemy.Length - 1;
        }

        return enemy[index];
    }

    // ReSharper disable Unity.PerformanceAnalysis
    // ReSharper disable once FunctionRecursiveOnAllPaths
    private IEnumerator StartTimerEnemy() //корутина таймера
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
        CheckIfTimeScale();

        StartCoroutine(StartTimerEnemy()); //перезапуск таймера спавна
    }

    private void SpawnEnemies()
    {
        spawnPoint = gameObject.transform.position;
        curEnemy = NightPool.Spawn(GetRandomEnemyType(), spawnPoint);
        zAngleEnemy = random.Range(0f, 350f); // рандомный добавочный угол через быстрый рандом для поворота врага
        curEnemy.transform.Rotate(0f, 0f, zAngleEnemy);
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
}