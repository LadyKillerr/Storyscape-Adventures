using fbg;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [Header("Materials Arrays")]
    // index của story parts hiện tại
    [SerializeField] int currentIndex = 0;

    // mảng chứa các mảnh ghép của câu truyện
    [SerializeField] GameObject[] storyParts;

    // mảng chứa các hình ảnh tương ứng với câu truyện đó
    [SerializeField] GameObject[] imageParts;

    // mảng chứa các nút bấm tương tác tương ứng với trang ảnh minh hoạ
    [SerializeField] GameObject[] hiddenButtonsParts;

    // mảng chứa các audio tương ứng với câu truyện
    [SerializeField] AudioClip[] audioParts;

    [Header("Audio Clip Sound Adjustment")]
    [SerializeField][Range(0, 1)] float storyVolume;

    [Header("Question time before continue")]
    [SerializeField] float delayTime = 5f;
    [SerializeField] float delayTimeSmall = .8f;

    [SerializeField] bool isComplete;

    [Header("Game Session Zone")]
    // phần truyện tương tác được
    [SerializeField] GameObject interactiveStorySection;

    //phần câu hỏi trắc nghiệm sau mỗi câu truyện
    [SerializeField] GameObject questionSection;

    // Components
    AudioSource storyAudioSource;

    void Awake()
    {

        storyAudioSource = GetComponent<AudioSource>();

        questionSection.SetActive(false);

        LoadFirstPart();

    }

    void Start()
    {
        // sau 5s sẽ toggle isComplete cho phép ng chơi next hoặc backward
        StartCoroutine(ToggleIsComplete());

    }

    void Update()
    {

    }

    public void NextPart()
    {
        // nếu index chưa phải max (chưa phải part cuối trong 1 câu truyện)
        // trừ 1 vì bắt đầu từ mảng bắt đầu từ 0
        if (currentIndex >= 0 && currentIndex < storyParts.Length - 1 && isComplete)
        {
            // reset lại biến isComplete
            isComplete = false;

            // ẩn hình với truyện hiện tại đi
            HideCurrentImagePart();
            HideCurrentStoryPart();
            HideCurrentHiddenButtons();
            MuteAllAudioParts();


            currentIndex += 1;

            // tăng index lên sau khi đã ẩn hình với ảnh hiện tại đi
            LoadParts();

        }
        // nếu index đã max (là part cuối trong 1 câu truyện)
        else if (currentIndex >= 0 && currentIndex >= storyParts.Length - 1 && isComplete)
        {
            // load ra màn hình câu hỏi trắc nghiệm, sẽ có câu hỏi riêng để ng chơi trả lời trắc nghiệm
            // ẩn hết màn hình câu hỏi đi 
            interactiveStorySection.SetActive(false);
            questionSection.SetActive(true);
        }
    }

    public void PreviousPart()
    {
        if (currentIndex > 0 && currentIndex <= storyParts.Length - 1 && isComplete)
        {
            // reset lại biến isComplete
            isComplete = false;

            // ẩn hình với truyện hiện tại đi
            HideCurrentImagePart();
            HideCurrentStoryPart();
            HideCurrentHiddenButtons();
            MuteAllAudioParts();

            // giảm index đi để lùi trang truyện và trang tranh về trang trước 
            currentIndex -= 1;

            // load ra trang tương ứng với index mới trừ đó
            LoadParts();
        }
        else if (currentIndex == 0 && currentIndex < storyParts.Length - 1 && isComplete)
        {
            // nếu đã back về scene đầu tiên rồi thì không lùi được nữa
            return;
        }
    }

    void LoadParts()
    {
        // load ra hình với ảnh và câu hỏi ẩn của index phù hợp
        storyParts[currentIndex].SetActive(true);
        imageParts[currentIndex].SetActive(true);
        hiddenButtonsParts[currentIndex].SetActive(true);

        // load ra âm thanh của index phù hợp sau chừng delayTimeSmall
        Invoke("PlayCurrentAudioParts", delayTimeSmall);

        // sau 5s sẽ toggle isComplete cho phép ng chơi next hoặc backward
        StartCoroutine(ToggleIsComplete());
    }

    IEnumerator ToggleIsComplete()
    {
        yield return new WaitForSecondsRealtime(delayTime);

        isComplete = true;
    }

    void LoadFirstPart()
    {
        // ẩn toàn bộ đi 
        HideAllStoryParts();
        HideAllImageParts();
        MuteAllAudioParts();
        HideAllHiddenButtons();

        // reset index
        currentIndex = 0;

        // hiện story Parts đầu tiên
        storyParts[currentIndex].SetActive(true);

        // hiện image parts đầu tiên 
        PlayCurrentImagePart();

        // bật hidden buttons section đầu tiên
        

        // chạy âm thanh của trang truyện đầu tiên sau chừng delayTimeSmall
        Invoke("PlayCurrentAudioParts", delayTimeSmall);


    }

    void HideAllHiddenButtons()
    {
        foreach (GameObject part in hiddenButtonsParts)
        {
            part.SetActive(false);
        }
    }

    void HideCurrentHiddenButtons()
    {
        hiddenButtonsParts[currentIndex].SetActive(false);
    }

    void MuteAllAudioParts()
    {
        storyAudioSource.Stop();
    }

    public void PlayCurrentAudioParts()
    {
        if (!storyAudioSource.isPlaying)
        {
            storyAudioSource.PlayOneShot(audioParts[currentIndex], storyVolume);

        }
        else { return; }
    }

    void PlayCurrentStoryPart()
    {
        storyParts[currentIndex].SetActive(true);
    }

    void HideCurrentStoryPart()
    {
        if (currentIndex >= 0 && currentIndex <= storyParts.Length)
        {
            storyParts[currentIndex].SetActive(false);
        }

    }

    void HideAllStoryParts()
    {
        foreach (GameObject part in storyParts)
        {
            part.SetActive(false);
        }
    }

    void PlayCurrentImagePart()
    {
        imageParts[currentIndex].SetActive(true);
    }

    void HideCurrentImagePart()
    {
        imageParts[currentIndex].SetActive(false);
    }

    void HideAllImageParts()
    {
        foreach (GameObject part in imageParts)
        {
            part.SetActive(false);
        }
    }


    public int GetCurrentIndex()
    {
        return currentIndex;
    }

    public int GetTotalIndex()
    {
        return storyParts.Length;
    }

}
