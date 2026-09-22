using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NewGame : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;
    public void StartNew()
    {
        audioSource.Play();
        SceneManager.LoadScene("Gameplay");
    }
}
