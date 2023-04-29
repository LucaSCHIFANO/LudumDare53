using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PizzaManager : MonoBehaviour, ICallable
{
    [Header("Ref")]
    [SerializeField] private PizzaManagerRef _ref;
    [SerializeField] GameObject go;
    private bool isPizzaGet;

    public bool IsPizzaGet { get => isPizzaGet; }

    private void Awake()
    {
        _ref.Instance = this;

    }

    private void Update()
    {
        if (!isPizzaGet) go.SetActive(true);
       
    }

    public void Interacted()
    {
        isPizzaGet= true;
        go.SetActive(false);
    }

    public void PizzaDelivered()
    {
        isPizzaGet= false;
    }
}
