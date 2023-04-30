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
    [SerializeField] Button selected;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private Image imageSound;
    [SerializeField] private List<Sprite> listSound = new List<Sprite>();
    [SerializeField] private AudioManager source;
    private bool isSound;

    private bool canPause;
    public bool CanPause { get => canPause; set => canPause = value; }

    private void Awake()
    {
        _ref.Instance = this;
        canPause = true;

        Time.timeScale = 1;
        pauseMenu.SetActive(false);


        switch (PlayerPrefs.GetInt("Sound")) // get if the sound was muted or not
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

    public void ChangePause() // change the state of the pause menu. Is the pause menu active or not ?
    {
        if (!canPause) return;
        isPaused = !isPaused;
        CallPause();
    }

    public void CallPause()
    {
        if (isPaused)
        {
            Time.timeScale = 0.0f;
            pauseMenu.SetActive(true);
            selected.Select();
        }
        else
        {
            Time.timeScale = 1.0f;
            pauseMenu.SetActive(false);
        }
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void ChangeSound() // change the state of the sound and save it !
    {
        isSound = !isSound;
        if (isSound) PlayerPrefs.SetInt("Sound", 0);
        else PlayerPrefs.SetInt("Sound", 1);
        source.ChangeSound(); // mute or unmuted the sound 

        ChangeSoundVisual();
    }

    void ChangeSoundVisual() // change the logo to show if the sound is muted or not
    {
        if (isSound) imageSound.sprite = listSound[0];
        else imageSound.sprite = listSound[1];
    }
}
