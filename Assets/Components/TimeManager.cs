using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float startTimeScale = 1;
    public static float timeScale;

    [FormerlySerializedAs("startTimePause")] [SerializeField]
    private float startTimePauseBeforeContinueTime = 0.5f; 

    private float timePauseBeforeContinueTime;

    [HideInInspector] public int secondCounter;
    [HideInInspector] public int minuteCounter;

    [HideInInspector] public SurviveTimer gameTime = new SurviveTimer();
    public TextMeshProUGUI timer;


    private void Start()
    {
        Time.timeScale = startTimeScale;

        minuteCounter = gameTime.Minute;
        secondCounter = gameTime.Second;
    }

    private void Update()
    {
        MainTimer();

        if (!(Math.Abs(timeScale - startTimeScale) > 0.1f)) return;
        timeScale = startTimeScale;

        Time.timeScale = timeScale;
    }

    private void MainTimer()
    {
        gameTime.AddTime(Time.deltaTime);
        if (secondCounter != gameTime.Second)
        {
            secondCounter = gameTime.Second;

            timer.text = gameTime.FormattedTime();
        }
    }

    public IEnumerator WaitBeforeContinueTime()
    {
        timePauseBeforeContinueTime = startTimePauseBeforeContinueTime;
        while (timePauseBeforeContinueTime > 0) //если время больше 0, то уменьшаем его по чуть-чуть
        {
            timePauseBeforeContinueTime -= Time.unscaledDeltaTime; //само уменьшение таймера
            yield return null; //продолжить выполнение после этого кадра
        }

        Time.timeScale = timeScale;
    }
}