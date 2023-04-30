using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [Header("Buttons Selected")]
    [SerializeField] private Button button;
    [SerializeField] private Button button2;

    [Header("Scene")]
    [SerializeField] private GameObject main;
    [SerializeField] private GameObject htp;

    [Header("HTP")]
    [SerializeField] private Image image;
    [SerializeField] private List<Sprite> list = new List<Sprite>();
    private int id;
    
    [Header("Sound")]
    [SerializeField] private Image imageSound;
    [SerializeField] private List<Sprite> listSound = new List<Sprite>();
    [SerializeField] private AudioSource source;
    private bool isSound;

    private void Awake()
    {
        button.Select();
        ReturnToMain();

        switch (PlayerPrefs.GetInt("Sound"))
        {
            case 0:
                isSound= true;
                break;
            default: isSound= false;
                break;
            
        }

        ChangeSoundVisual();


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
        id = 0;
        ShowImage();
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
        ShowImage();
    }
    public void Previous()
    {
        id--;
        if(id < 0) id= list.Count-1;
        ShowImage();
    }

    public void ShowImage()
    {
        image.sprite = list[id];
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void ChangeSound()
    {
        isSound = !isSound;
        if (isSound) PlayerPrefs.SetInt("Sound", 0);
        else PlayerPrefs.SetInt("Sound", 1);

        ChangeSoundVisual();
    }

    void ChangeSoundVisual()
    {
        if (isSound)
        {
            imageSound.sprite = listSound[0];
            source.enabled = true;
        }
        else
        {
            imageSound.sprite = listSound[1];
            source.enabled = false;
        }
    }
}
