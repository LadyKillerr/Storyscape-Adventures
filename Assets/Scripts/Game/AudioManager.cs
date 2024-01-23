using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Vocabulary")]
    [SerializeField] AudioClip catWord;
    [SerializeField][Range(0, 1)] float catWordVolume = 1f;

    [SerializeField] AudioClip batWord;
    [SerializeField][Range(0, 1)] float batWordVolume = 1f;

    [SerializeField] AudioClip hatWord;
    [SerializeField][Range(0, 1)] float hatWordVolume = 1f;

    [SerializeField] AudioClip appleWord;
    [SerializeField][Range(0, 1)] float appleWordVolume = 1f;

    [SerializeField] AudioClip cakeWord;
    [SerializeField][Range(0, 1)] float cakeWordVolume = 1f;

    [SerializeField] AudioClip treeWord;
    [SerializeField][Range(0, 1)] float treeWordVolume = 1f;

    AudioSource gameAudio;

    Vector3 cameraPosition;

    void Start()
    {
        gameAudio = GetComponent<AudioSource>();

        // lấy ra vị trí của camera để làm điểm phát audio
        //cameraPosition = Camera.main.transform.position;

    }

    // hàm dùng chung để chạy tất cả các clip âm thanh được lưu ở đây
    void PlayAudio(AudioClip clip, float volume)
    {
        if (!gameAudio.isPlaying)
        {
            gameAudio.PlayOneShot(clip, volume);

        }
    }

    public void PlayCatClip()
    {
        PlayAudio(catWord, catWordVolume);
    }

    public void PlayBatClip()
    {
        PlayAudio(batWord, batWordVolume);
    }

    public void PlayHatClip()
    {
        PlayAudio(hatWord, hatWordVolume);
    }

    public void PlayAppleWord()
    {
        PlayAudio(appleWord, appleWordVolume);
    }

    public void PlayCakeWord()
    {
        PlayAudio(cakeWord, cakeWordVolume);
    }

    public void PlayTreeWord()
    {
        PlayAudio(treeWord, treeWordVolume);
    }
}
