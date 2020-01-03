using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class HealthGameManager : MonoBehaviour 
{
    [SerializeField] GameObject enemyPrefab;
    [SerializeField] float spawnRadius;
    [SerializeField] float gameTime;
    [SerializeField] Text timeText;

    [SerializeField] GameObject gameWinCanvas;
    [SerializeField] GameObject gameOverCanvas;

    float remainTime;

    float waitTime;

    bool isGameOver;

    public static HealthGameManager Instance { get; private set; }
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }
    private void Start()
	{
        StartCoroutine(CreateEnemies());
        waitTime = 3;
        remainTime = gameTime;
        isGameOver = false;

        DontDestroy[] dontDestroy = FindObjectsOfType<DontDestroy>();
        for(int i = 0; i < dontDestroy.Length; i++)
        {
            Destroy(dontDestroy[i].gameObject);
        }
	}

    private void Update()
    {
        if (isGameOver)
            return;

        remainTime -= Time.deltaTime;

        if (remainTime < 0)
        {
            remainTime = 0;
            StopAllCoroutines();
            GameWin();
        }

        timeText.text = TimeToText(remainTime);
    }

    void GameWin()
    {
        if (isGameOver)
            return;

        Enemy[] enemies = FindObjectsOfType<Enemy>();
        for (int i = 0; i < enemies.Length; i++)
        {
            enemies[i].Destroy();
        }

        isGameOver = true;
        gameWinCanvas.SetActive(true);
        FindObjectOfType<Player>().IsControllable = false;
    }

    public void GameOver()
    {
        if (isGameOver)
            return;

        StopAllCoroutines();
        isGameOver = true;
        gameOverCanvas.SetActive(true);
        FindObjectOfType<Player>().IsControllable = false;
    }

    IEnumerator CreateEnemies()
    {
        yield return new WaitForSeconds(5f);

        while (true)
        {
            float angle = Random.Range(0, 360);
            Vector3 randomCircle = new Vector3(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad), 0);
            Vector3 worldPos = transform.TransformPoint(randomCircle * spawnRadius);
            worldPos.z = worldPos.y;
            worldPos.y = 0.75f;
            Instantiate(enemyPrefab, worldPos, Quaternion.identity);
            waitTime -= 0.065f;
            waitTime = Mathf.Clamp(waitTime, 0.8f, 5f);
            yield return new WaitForSeconds(waitTime);
        }
    }

    string TimeToText(float time)
    {
        string t = "";

        int minutes = 0;
        while (time > 59)
        {
            time -= 60;
            minutes++;
        }
        int seconds = Mathf.RoundToInt(time);
        seconds = Mathf.Clamp(seconds, 0, 59);
        string s = seconds > 9 ? seconds.ToString() : "0"+seconds;

        t = "0" + minutes + ":" + s;

        return t;
    }

    public bool IsGameOver() { return isGameOver;  }
}
