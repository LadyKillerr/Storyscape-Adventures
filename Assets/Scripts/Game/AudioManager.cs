using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [Header("Vocabulary")]
    [SerializeField] AudioClip bunnyWord;
    [SerializeField][Range(0, 1)] float bunnyWordVolume = 1f;

    [SerializeField] AudioClip dolphinWord;
    [SerializeField][Range(0, 1)] float dolphinWordVolume = 1f;

    [SerializeField] AudioClip shipWord;
    [SerializeField][Range(0, 1)] float shipWordVolume = 1f;

    [SerializeField] AudioClip seaWord;
    [SerializeField][Range(0, 1)] float seaWordVolume = 1f;

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

    public void PlayBunnyClip()
    {
        PlayAudio(bunnyWord, bunnyWordVolume);
    }

    public void PlayDolphinClip()
    {
        PlayAudio(dolphinWord, dolphinWordVolume);
    }

    public void PlayShipClip()
    {
        PlayAudio(shipWord, shipWordVolume);
    }

    public void PlaySeaClip()
    {
        PlayAudio(seaWord, seaWordVolume);
    }
}
