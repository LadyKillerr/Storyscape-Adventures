using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateHiddenButtons : MonoBehaviour
{
    [SerializeField] GameObject wordsLabel;

    // thời gian từ từ hiện ra và thời gian từ từ ẩn đi
    [SerializeField] float showingTime = 5f;

    // Config settings
    [Header("Config settings")]

    [SerializeField] bool isShowing;

    [Header("Audio settings")]
    [SerializeField] AudioClip hiddenWordAudio;
    [SerializeField][Range(0,1)] float hiddenWordVolume;
    AudioSource hiddenWordAudioSource;


    void Awake()
    {
        DeactivateHiddenButton();

        hiddenWordAudioSource = GetComponent<AudioSource>();
    }

    public void ActivateButtons()
    {
        if (!isShowing)
        {
            ActivateHiddenButton();

            // sau 5s thì tắt nó đi lại
            Invoke("HideHiddenButtons", showingTime);

            playAudioClip(hiddenWordAudio, hiddenWordVolume);
        }
        else
        {
            return;
        }
    }

    public void playAudioClip(AudioClip wordAudio, float wordVolume)
    {
        hiddenWordAudioSource.PlayOneShot(wordAudio, wordVolume);
    }

    void HideHiddenButtons()
    {
        wordsLabel.SetActive(false);
    }

    void DeactivateHiddenButton()
    {
        wordsLabel.SetActive(false);
        isShowing = false;



    }

    void ActivateHiddenButton()
    {
        wordsLabel.SetActive(true);
        isShowing = true;
    }
}
