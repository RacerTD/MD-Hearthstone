using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioTest : MonoBehaviour
{
    public AudioClip buttonClickSFX;
    public AudioClip music;

    public void Start()
    {
        AudioManager.Instance.PlayMusicWithFade(music);
    }
    private void Update()
    {
        
    }
    public void ButtonPressed()
    {
        AudioManager.Instance.PlaySFX(buttonClickSFX);
    }

}
