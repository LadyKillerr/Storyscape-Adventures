using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoryManager : MonoBehaviour
{
    // mảng chứa các mảnh ghép của câu truyện
    [SerializeField] GameObject[] storyParts;

    // mảng chứa các hình ảnh tương ứng với câu truyện đó
    [SerializeField] GameObject[] imageParts;

    // index của story parts hiện tại
    [SerializeField] int currentIndex = 0;

    int nextStoryPartIndex;

    void Start()
    {
        // index tiếp theo là index hiện tại + 1

        nextStoryPartIndex = currentIndex + 1;

        // kiểm soát index trong phạm vi cho phép
        Mathf.Clamp(currentIndex, 0, storyParts.Length);

        LoadFirstStoryParts();


    }

    public void ShowNextStoryPart()
    {

        // ẩn storyParts hiện tại đi
        HideCurrentStoryPart();

        storyParts[nextStoryPartIndex].SetActive(true);

        // nếu index chưa phải max (chưa phải part cuối trong 1 câu truyện)
        if (currentIndex >= 0 && currentIndex < storyParts.Length)
        {
            

            storyParts[nextStoryPartIndex].SetActive(true);

            Debug.Log("load ra story part mới");
        }
        // nếu index đã max (là part cuối trong 1 câu truyện)
        else if (currentIndex >= 0 && currentIndex >= storyParts.Length)
        {
            // Reset lại curentIndex về thành 0 (scene đầu tiên) 
            LoadFirstStoryParts();

            Debug.Log("load ra story part đầu tiên");


        }
    }

    void LoadFirstStoryParts()
    {
        // reset index
        currentIndex = 0;

        // ẩn toàn bộ storyParts đi 
        HideAllStoryParts();

        // hiện story Parts đầu tiên
        storyParts[currentIndex].SetActive(true);

    }


    public void HideCurrentStoryPart()
    {
        if (currentIndex >= 0 && currentIndex <= storyParts.Length)
        {
            storyParts[currentIndex].SetActive(false);
        }

    }

    public void HideAllStoryParts()
    {
        foreach (GameObject part in storyParts)
        {
            part.SetActive(false);
        }
    }

}
