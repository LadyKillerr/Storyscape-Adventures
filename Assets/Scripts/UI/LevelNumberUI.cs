using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumberUI : MonoBehaviour
{
    [SerializeField] Canvas gameCanvas;

    StoryManager storyManager;

    TextMeshProUGUI storyProgressUI;

    string currentStoryProgress;
    string totalStoryProgress;

    void Awake()
    {
        // Get ra TextMeshPro component
        storyProgressUI = GetComponent<TextMeshProUGUI>();

        // Get ra mananger để lấy currentIndex
        storyManager = gameCanvas.GetComponent<StoryManager>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // thể hiện số level
        showGameProgressUI();
    }

    void showGameProgressUI()
    {
        // +1 vì index của mảng bắt đầu từ số 0
        currentStoryProgress = (storyManager.GetCurrentIndex() + 1).ToString("00");

        totalStoryProgress = storyManager.GetTotalIndex().ToString("00");

        storyProgressUI.text = "Story " + currentStoryProgress + " / " + totalStoryProgress;
    
        
    }
}
