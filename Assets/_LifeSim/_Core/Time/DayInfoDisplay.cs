using UnityEngine;
using UnityEngine.UI;
using LifeSim.Core.Timer;

public class DayInfoDisplay : MonoBehaviour 
{
    [SerializeField] Text dateText;
    [SerializeField] Text dayWeekText;
    [SerializeField] Text timeText;
    [SerializeField] Image climateImage;

    private GameTime gameTime;

    int minutes;

    private void Start()
    {
        gameTime = GameTime.Instance;
        UpdateDayInfo();
    }

    private void Update()
    {
        int seconds = Mathf.FloorToInt(gameTime.DaySeconds);
        if (seconds > minutes)
            ChangeTime(gameTime.DaySeconds);
    }

    void ChangeTime(float seconds)
    {
        int h = Mathf.FloorToInt(seconds / 60);
        int m = Mathf.FloorToInt(seconds - (h * 60));

        string timeIndicator = h > 11 ? "p.m." : "a.m";

        if (h > 12)
            h -= 12;

        string hours = h < 10 ? "0" + h.ToString() : h.ToString();
        string minutes = m < 10 ? "0" + m.ToString() : m.ToString();

        timeText.text = hours + ":" + minutes + " " + timeIndicator;
    }

    public void UpdateDayInfo()
    {
        string date = gameTime.Today.Day + " de " + TimeUtility.GetSpanishMonth(gameTime.Today.Month);
        string dayWeek = TimeUtility.GetSpanishDayWeek(gameTime.Today.DayOfWeek);

        dateText.text = date;
        dayWeekText.text = dayWeek;
    }
}
