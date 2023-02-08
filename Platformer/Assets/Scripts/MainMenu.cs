using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;

    [SerializeField] private AudioSource _source;
    public void StartHandler()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitHandler()
    {
        Application.Quit(); 
    }

    public void SoundHandlerOff()
    {
        _soundOn.SetActive(false);
        _soundOff.SetActive(true);
        _source.mute = true;
    }
    public void SoundHandlerOn()
    {
        _soundOn.SetActive(true);
        _soundOff.SetActive(false);
        _source.mute = false;
    }
}
