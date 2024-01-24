using fbg;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    [Header("Materials Arrays")]

    // độ dài của mảng 
    [SerializeField] int arraysLength;

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

    [Header("Audio Adjustment")]
    [SerializeField][Range(0, 1)] float storyVolume;

    [Header("Intro Audio Sounds")]
    [SerializeField] AudioClip introAudioClip1;
    [SerializeField] AudioClip introAudioClip2;
    [SerializeField][Range(0, 1)] float introVolume;
    [SerializeField] float timeBetweenIntroAudio = .8f;

    [Header("Question time before continue")]
    [SerializeField] float delayTime = 5f;
    [SerializeField] float delayTimeSmall = .8f;

    [SerializeField] bool isComplete;

    [Header("Game Session Zone")]

    // phần intro cho mỗi câu truyện
    [SerializeField] GameObject storyIntro;

    // phần truyện tương tác được
    [SerializeField] GameObject interactiveStorySection;

    //phần câu hỏi trắc nghiệm sau mỗi câu truyện
    [SerializeField] GameObject questionSection;

    // Components
    AudioSource storyAudioSource;

    // flow code: Awake sẽ là LoadFirstStoryPart, sau đó tiếp tục load part các index tiếp theo dần dần

    void Awake()
    {
        arraysLength = storyParts.Length;

        storyAudioSource = GetComponent<AudioSource>();

        questionSection.SetActive(false);


        LoadFirstStoryPart();
        //LoadStoryIntro();

    }

    void Start()
    {
        // sau 5s sẽ toggle isComplete cho phép ng chơi next hoặc backward
        StartCoroutine(ToggleIsComplete());

    }

    void LoadFirstStoryPart()
    {
        // tắt hết đi và reset index
        HideAllStoryParts();
        HideAllImageParts();
        MuteAllAudioParts();
        HideAllHiddenButtons();

        // reset index
        currentIndex = 0;

        // kích hoạt phần interactive story section
        ActivateStoryPart();

        // hiện story Parts đầu tiên
        PlayCurrentStoryPart();

        // hiện image parts đầu tiên 
        PlayCurrentImagePart();

        // bật hidden buttons section đầu tiên
        PlayCurrentHiddenButtons();

        // chạy âm thanh của trang truyện đầu tiên sau chừng delayTimeSmall
        Invoke("PlayCurrentAudioParts", delayTimeSmall);
    }

    // load ra part tương ứng với index
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

    // bật bool isComplete sau khi đã đọc xong
    IEnumerator ToggleIsComplete()
    {
        yield return new WaitForSecondsRealtime(delayTime);

        isComplete = true;
    }

    // chức năng sang trang tiếp theo của trang sách
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

    // chức năng back lại trang cũ
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



    //void LoadStoryIntro()
    //{
    //    // ẩn toàn bộ đi 
    //    HideAllStoryParts();
    //    HideAllImageParts();
    //    MuteAllAudioParts();
    //    HideAllHiddenButtons();





    //    // load cảnh intro ra
    //    LoadIntroSection();
    //    StartCoroutine(LoadIntroAudio());
    //}

   

    //IEnumerator LoadIntroAudio()
    //{
    //    // bật âm thanh xong đợi cho âm thanh chạy, âm thanh chạy xong thì sang audio thứ 2 
    //    storyAudioSource.PlayOneShot(introAudioClip1, introVolume);

    //    yield return new WaitForSecondsRealtime(introAudioClip1.length + timeBetweenIntroAudio);


    //    storyAudioSource.PlayOneShot(introAudioClip2, introVolume);

    //    yield return new WaitForSecondsRealtime(introAudioClip2.length + timeBetweenIntroAudio);


    //    // load cả 2 file audio xong thì bắt đầu vào câu truyện chính
    //    LoadFirstStoryPart();
    //}

    //void LoadIntroSection()
    //{
    //    storyIntro.SetActive(true);    
    //}

    //void HideIntroSection()
    //{
    //    storyIntro.SetActive(false);
    //}

    void PlayCurrentHiddenButtons()
    {
        hiddenButtonsParts[currentIndex].SetActive(true);
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

    void ActivateStoryPart()
    {
        interactiveStorySection.SetActive(true);
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
