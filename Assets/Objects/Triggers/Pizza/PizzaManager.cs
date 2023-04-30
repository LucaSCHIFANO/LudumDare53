using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PizzaManager : MonoBehaviour, ICallable
{
    [Header("Ref")]
    [SerializeField] private PizzaManagerRef _ref;
    [SerializeField] GameObject go;
    private bool isPizzaGet;

    [Header("UI")]
    [SerializeField] private Image logo1;
    [SerializeField] private Image logo2;
    [SerializeField] private GameObject logo3;

    public bool IsPizzaGet { get => isPizzaGet; }

    private void Awake()
    {
        _ref.Instance = this;
        go.SetActive(true);
    }
    private void Update()
    {
        if (!isPizzaGet)
        {
            logo2.color = new Color(1,1,1, Mathf.PingPong(Time.time * 2, 2)); // make the "Go Get Pizza" image blink
        }
    }

    public void Interacted() // when the player enter the red area
    {
        isPizzaGet= true;
        go.SetActive(false);
        ChangeVisual();
    }

    public void PizzaDelivered() // when the player enter a yellow area
    {
        isPizzaGet= false;
        go.SetActive(true);
        ChangeVisual();
    }

    private void ChangeVisual() // change visuals of the pizza logo 
    {
        if (isPizzaGet)
        {
            logo1.color = Color.gray;
            logo2.gameObject.SetActive(false);
            logo3.SetActive(false);
        }
        else
        {
            logo1.color = Color.white;
            logo2.gameObject.SetActive(true);
            logo3.SetActive(true);
        }
    }
}
