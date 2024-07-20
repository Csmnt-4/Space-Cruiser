using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    // Start is called before the first frame update
    public void PlayMainScene()
    {
        SceneManager.LoadScene(1);
    }

    public void PlayHighScoresScene()
    {
        SceneManager.LoadScene(2);
    }

    public void SetPreferencesSound()
    {
        AudioSource audioSource = GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("EnableSound", 0) == 0)
        {
            PlayerPrefs.SetInt("EnableSound", 1);
            audioSource.Play();
        }
        else
        {
            PlayerPrefs.SetInt("EnableSound", 0);

            audioSource.Stop();
        }
    }
}