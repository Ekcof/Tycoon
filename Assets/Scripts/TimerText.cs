using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerText : MonoBehaviour
{
    [SerializeField] private Text timerText;
    [SerializeField] private int startHour;
    [SerializeField] private int endHour;
    [SerializeField] private int realSeconds;
    [SerializeField] private int minuteStep = 5;
    private int scriptTime = 0;
    private int period;
    private float realTimeForInGameTime;
    private int minutes;

    private void Awake()
    {
        period = (endHour - startHour) * 60;
        if (period < 0) period = 0;
        realTimeForInGameTime = (float)realSeconds / period;
    }

    private void FixedUpdate()
    {
        if (minuteStep > 0)
        {
            minutes = (int)(startHour * 60 + Time.time / realTimeForInGameTime);
            minutes = (int)(minutes - minutes % minuteStep);
        }
        if (minutes > 1440) minutes = 0;
        timerText.text = TransformMinutesToText();
    }

    private string TransformMinutesToText()
    {
        int hours = minutes / 60;
        if (hours > 12) hours -= 12;
        int trueMinutes = minutes % 60;
        string minutesToString = trueMinutes.ToString();
        if (trueMinutes < 10) minutesToString = "0" + minutesToString;
        string hoursToString = hours.ToString();
        if (hours < 10) hoursToString = "0" + hoursToString;
        string lastPart = " AM";
        if (minutes >= 780) lastPart = " PM";

        return (hoursToString + ":" + minutesToString + lastPart);
    }
}
