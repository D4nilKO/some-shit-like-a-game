using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float startTimeScale = 1;
    public static float timeScale;

    [HideInInspector] public int secondCounter;
    [HideInInspector] public int minuteCounter;

    [SerializeField] private float startTimePause = 0.5f; //���������� �����
    private float timePause;
    public TextMeshProUGUI timer;
    [HideInInspector] public SurviveTimer gameTime = new SurviveTimer();


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
        timePause = startTimePause;
        while (timePause > 0) //���� ����� ������ 0, �� ��������� ��� �� ����-����
        {
            timePause -= Time.unscaledDeltaTime; //���� ���������� �������
            yield return null; //���������� ���������� ����� ����� �����
        }

        Time.timeScale = timeScale;
    }
}