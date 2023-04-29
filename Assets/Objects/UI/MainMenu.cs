using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Button button2;

    [SerializeField] private GameObject main;
    [SerializeField] private GameObject htp;

    [SerializeField] private List<Sprite> list = new List<Sprite>();
    private int id;

    private void Awake()
    {
        button.Select();
    }
    public void Play()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void HowToPlay()
    {
        htp.SetActive(true);
        main.SetActive(false);
        button2.Select();
    }

    public void ReturnToMain()
    {
        htp.SetActive(false);
        main.SetActive(true);
        button.Select();
    }

    public void Next()
    {
        id++;
        if(id>=list.Count) id= 0;
    }
    public void Previous()
    {
        id--;
        if(id < 0) id= list.Count-1;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
