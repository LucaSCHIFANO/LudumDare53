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
    }

    public void Scored()
    {
        currentScore += (int)currentPizzaTimer;

        currentPizzaTimer = pizzaTimer;
        currentTimer += (int)(currentPizzaTimer / 2);
    }
}
