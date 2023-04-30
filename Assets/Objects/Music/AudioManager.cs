using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour // pretty bad but it works !
{
    private bool isSound;
    public AudioSource source;
    public void Awake()
    {

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

    public void ChangeSound() // change the sound x)
    {
        isSound= !isSound;
        ChangeSoundVisual();
    }

    public void ChangeSoundVisual() // mute or unmute the sound
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
