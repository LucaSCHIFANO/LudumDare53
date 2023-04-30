using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using static Unity.VisualScripting.Member;

public class PauseManager : MonoBehaviour
{
    [Header("Ref")]
    [SerializeField] private PauseRef _ref;
    private bool isPaused;

    [Header("Pause Menu")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image imageSound;
    [SerializeField] private List<Sprite> listSound = new List<Sprite>();
    [SerializeField] private AudioManager source;
    private bool isSound;



    private void Awake()
    {
        _ref.Instance = this;

        pauseMenu.SetActive(false);


        switch (PlayerPrefs.GetInt("Sound"))
        {
            case 0:
                isSound = true;
                break;
            default:
                isSound = false;
                break;

        }

        ChangeSoundVisual();
    }

    public void ChangePause()
    {
        isPaused = !isPaused;
        CallPause();
    }

    public void CallPause()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
            Time.timeScale = 1.0f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void ChangeSound()
    {
        isSound = !isSound;
        if (isSound) PlayerPrefs.SetInt("Sound", 0);
        else PlayerPrefs.SetInt("Sound", 1);
        source.ChangeSound();

        ChangeSoundVisual();
    }

    void ChangeSoundVisual()
    {
        if (isSound) imageSound.sprite = listSound[0];
        else imageSound.sprite = listSound[1];
    }
}