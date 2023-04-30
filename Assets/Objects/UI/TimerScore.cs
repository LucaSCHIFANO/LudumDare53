using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TimerScore : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private TimerScoreRef _ref;
    [SerializeField] private PauseRef pauseRef;

    [Header("Timers")]
    [SerializeField] private TextMeshProUGUI textTimer;
    [SerializeField] private TextMeshProUGUI textPizzaTimer;
    [SerializeField] private float timer;
    [SerializeField] private float pizzaTimer;
    private float currentTimer;
    private float currentPizzaTimer;

    [Header("Score")]
    [SerializeField] private TextMeshProUGUI textScore;
    private float currentScore;
    private bool freezeScore;

    [Header("Visual")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject visualGain;
    [SerializeField] private Transform popUpPosition;
    [SerializeField] private Sprite good;
    [SerializeField] private Sprite bad;

    [Header("End")]
    [SerializeField] GameObject endScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] private TextMeshProUGUI textScoreFin;
    [SerializeField] private TextMeshProUGUI textBestScore;

    private void Awake()
    {
        _ref.Instance= this;

        currentTimer = timer;
        currentPizzaTimer= pizzaTimer;

        gameScreen.SetActive(true);
        endScreen.SetActive(false);
    }
    private void Update()
    {
        currentTimer -= Time.deltaTime;
        currentPizzaTimer -= Time.deltaTime;

        textTimer.text = ((int)currentTimer).ToString("D3");
        textPizzaTimer.text = ((int)currentPizzaTimer).ToString("D3");
        textScore.text = $"Score : {((int)currentScore).ToString("D4")}";

        switch (currentPizzaTimer)
        {
            case < 5:
                textPizzaTimer.color = Color.red;
                break;
            case < 10:
                textPizzaTimer.color = Color.yellow;
                break;
            default:
                textPizzaTimer.color = Color.green;
                break;
        }

        if (currentTimer <= 0) EndGame();
    }

    public void Scored()
    {
        GameObject gain = Instantiate(visualGain, popUpPosition.position, Quaternion.identity, canvas.transform);
        Image gain1 = gain.transform.GetChild(0).GetComponent<Image>();
        TextMeshProUGUI gain2 = gain.transform.GetChild(1).GetComponent<TextMeshProUGUI>();

        if(currentPizzaTimer <= 0)
        {
            currentScore += (int)(currentPizzaTimer);
            currentTimer += (int)(currentPizzaTimer);

            gain1.sprite = bad;
            gain2.text = $"{((int)(currentPizzaTimer)).ToString()}";
        }
        else
        {
            currentScore += (int)currentPizzaTimer;
            currentTimer += (int)(currentPizzaTimer / 2);
            
            gain1.sprite = good;
            gain2.text = $" + {((int)currentPizzaTimer / 2).ToString()}";
        }



        currentPizzaTimer = pizzaTimer;
    }

    void EndGame()
    {
        pauseRef.Instance.CanPause = false;
        Time.timeScale = 0;

        if (!freezeScore)
        {
            if (currentPizzaTimer < 0) currentScore += (int)(currentPizzaTimer);
            freezeScore = true;
        }


        endScreen.SetActive(true);
        gameScreen.SetActive(false);

        textScoreFin.text = $"Score : {((int)currentScore).ToString("D4")}";

        if ((int)currentScore > PlayerPrefs.GetInt("Best Score")) PlayerPrefs.SetInt("Best Score", (int)currentScore);


        textBestScore.text = $"Best : {PlayerPrefs.GetInt("Best Score").ToString("D4")}";
    }
}
