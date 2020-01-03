using UnityEngine;
using UnityEngine.Events;
using System;

namespace LifeSim.Core.Timer
{
    public class GameTime : MonoBehaviour
    {
        public static GameTime Instance { get; private set; }

        private DateTime today;
        public DateTime Today { get { return today; } private set { } }

        private float daySeconds;
        public float DaySeconds { get { return daySeconds; } private set { } }

        private const float TIME_SPEED_MULTIPLIER = 0.5f;
        private const float NORMAL_START_TIME = 60 * 6; // 6AM
        private const float MAX_DAY_DURATION = 60 * 24;  //12AM
        [SerializeField] UnityEvent onNextDay;

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
        }

        private void Start()
        {
            //Get Save Data for today
            GetTodayData();
        }

        private void Update()
        {
            daySeconds += Time.deltaTime * TIME_SPEED_MULTIPLIER;

            if (daySeconds > MAX_DAY_DURATION)
            {
                LifeSim.SceneControl.GameSceneManager.Instance.LoadScene("House");
                FindObjectOfType<Player>().transform.position = new Vector3(106f, 0f, 18f);
                NextDay();
            }
        }

        private void GetTodayData()
        {
            //today = new DateTime(2018, 1, 1);
            today = GameManager.Instance.GetActualData().dateTime;
            daySeconds = NORMAL_START_TIME;
        }

        public void NextDay()
        {
            daySeconds = NORMAL_START_TIME;
            today = today.AddDays(1);
            onNextDay.Invoke();
            GameManager.Instance.CropCycle();
            GameManager.Instance.SaveData();
        }

    }

}