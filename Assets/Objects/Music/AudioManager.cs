using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private bool isSound;
    public AudioSource source;
    public void Awake()
    {

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

    public void NextAwake()
    {

    }

    public void ChangeSound()
    {
        isSound= !isSound;
        ChangeSoundVisual();
    }

    public void ChangeSoundVisual()
    {
        if (isSound)
        {
            source.enabled= true;
        }
        else
        {
            source.enabled= false;
        }
    }
}
