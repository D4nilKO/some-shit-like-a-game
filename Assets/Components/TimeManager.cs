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

    public void ApplyWaitBeforeContinueTime()
    {
        StopCoroutine(WaitBeforeContinueTime());
        StartCoroutine(WaitBeforeContinueTime());
    }

    private IEnumerator WaitBeforeContinueTime()
    {
        timePauseBeforeContinueTime = startTimePauseBeforeContinueTime;
        while (timePauseBeforeContinueTime > 0) 
        {
            timePauseBeforeContinueTime -= Time.unscaledDeltaTime; 
            yield return null; 
        }

        Time.timeScale = timeScale;
    }
}