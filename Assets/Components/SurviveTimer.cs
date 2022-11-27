using System;
using System.Text;

[Serializable]
public class SurviveTimer
{
    public int Minute
    {
        get { return minute; }
    }

    public int Second
    {
        get { return second; }
    }

    public int Millisecond
    {
        get { return millisecond; }
    }

    public float CompareValue
    {
        get { return compareValue; }
    }

    private int minute;
    private int second;
    private int millisecond;
    private float time;
    private float compareValue;
    private StringBuilder formattedMinute = new StringBuilder();
    private StringBuilder formattedSecond = new StringBuilder();
    private StringBuilder formattedMillisecond = new StringBuilder();

    public static SurviveTimer Compare(SurviveTimer a, SurviveTimer b)
    {
        return a.CompareValue < b.CompareValue ? a : b;
    }

    public void AddTime(float deltaTime)
    {
        compareValue += deltaTime;
        time += deltaTime;
        if (time >= 0.01f)
        {
            while (time >= 0.01f)
            {
                time -= 0.01f;
                millisecond++;
            }
        }

        if (millisecond >= 100)
        {
            millisecond = 0;
            second++;
        }

        if (second < 60) return;
        second = 0;
        minute++;
    }

    public string FormattedTime()
    {
        return String.Format("{0}:{1}",
            ZeroAdder(ref formattedMinute, Minute),
            ZeroAdder(ref formattedSecond, Second));
        //ZeroAdder(ref _formattedMillisecond, Millisecond));
    }

    private StringBuilder ZeroAdder(ref StringBuilder builder, int value)
    {
        builder.Remove(0, builder.Length);
        return (value >= 10) ? builder.Append(value) : builder.Append("0" + value);
    }
}