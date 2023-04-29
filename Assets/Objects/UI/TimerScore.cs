using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerScore : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private TimerScoreRef _ref;

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

    [Header("Visual")]
    [SerializeField] private GameObject canvas;
    [SerializeField] private GameObject visualGain;
    [SerializeField] private Transform popUpPosition;


    private void Awake()
    {
        _ref.Instance= this;

        currentTimer = timer;
        currentPizzaTimer= pizzaTimer;
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
        TextMeshProUGUI gain = Instantiate(visualGain, popUpPosition.position, Quaternion.identity, canvas.transform).GetComponent<TextMeshProUGUI>();
        

        if(currentPizzaTimer <= 0)
        {
            currentScore += (int)(currentPizzaTimer * 2);
            currentTimer += (int)(currentPizzaTimer * 2);

            gain.color = Color.red;
            gain.text = $"{((int)(currentPizzaTimer * 2)).ToString()}";
        }
        else
        {
            currentScore += (int)currentPizzaTimer;
            currentTimer += (int)(currentPizzaTimer / 2);
            
            gain.color = Color.green;
            gain.text = $" + {((int)currentPizzaTimer).ToString()}";
        }



        currentPizzaTimer = pizzaTimer;
    }

    void EndGame()
    {
        Time.timeScale = 0;
    }
}
