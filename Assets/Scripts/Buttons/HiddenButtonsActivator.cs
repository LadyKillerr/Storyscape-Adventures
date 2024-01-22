using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.ShaderGraph;
using UnityEngine;
using UnityEngine.UI;

public class HiddenButtonsActivator : MonoBehaviour
{
    [Header("Hidden Words Label")]

    // từ sẽ hiện ra tuỳ theo tranh
    [SerializeField] GameObject wordsLabel;

    // thời gian từ từ hiện ra và thời gian từ từ ẩn đi
    [SerializeField] float showingTime = 5f;
    [SerializeField] float hidingTime = 3f;


    // Components
    Image wordLabelImage;

    // Config settings
    [Header("Config settings")]

    [SerializeField] bool isShowing;
    float elapsedTime = 0f;
    float fullOpacity = 255f;
    float noOpacity = 0f;

    // Config R G B for label colors
    [Header("Config R G B for label colors")]

    [SerializeField] float redColor = 1f;
    [SerializeField] float greenColor = 1f;
    [SerializeField] float blueColor = 1f;


    void Awake()
    {
        wordLabelImage = wordsLabel.GetComponent<Image>();

        wordsLabel.SetActive(false);
        isShowing = false;
    }

    private void Start()
    {
        if (wordLabelImage != null)
        {
            Debug.Log("Get Component success");
        }
        else if ( wordLabelImage == null)
        {
            Debug.Log("GetComponent success");
        }
    }

    public void ActivateHiddenWords()
    {
            StartCoroutine(ShowWordsForSeconds(showingTime));
        if (isShowing == false)
        {


        }
        else if (isShowing)
        {
            return;
        }
    }

    // hiện ra từ từ trong khoảng showSeconds giây
    IEnumerator ShowWordsForSeconds(float showSeconds)
    {
        isShowing = true;

        // khi thời gian xuất hiện ít hơn thời gian Serialize
        while (elapsedTime < showSeconds)
        {
            float currentOpacity = Mathf.Lerp(0f, fullOpacity, elapsedTime / showSeconds);

            // config colors để dễ chỉnh sửa hơn 
            wordLabelImage.color = new Color(redColor, greenColor, blueColor, currentOpacity);

            elapsedTime += Time.deltaTime;

            yield return null;
        }

        // khi đã 5s trôi qua (elapsed time = với showingTime
        wordLabelImage.color = new Color(redColor, greenColor, blueColor, fullOpacity);

        ResetElapsedTime();

        StartCoroutine(HideButton(hidingTime));


    }


    // ẩn đi từ từ trong khoảng hideSeconds giây
    IEnumerator HideButton(float hideSeconds)
    {

        while (elapsedTime < hideSeconds)
        {
            float currentOpacity = Mathf.Lerp(0f, noOpacity, elapsedTime / hideSeconds);

            // config colors để dễ chỉnh sửa hơn 
            wordLabelImage.color = new Color(redColor, greenColor, blueColor, currentOpacity);

            elapsedTime += Time.deltaTime;

            yield return null;

        }

        // khi đã 5s trôi qua (elapsed time = với showingTime
        wordLabelImage.color = new Color(redColor, greenColor, blueColor, noOpacity);

        ResetElapsedTime();
        ResetIsShowing();
    }

    void ResetElapsedTime()
    {
        elapsedTime = 0f;
    }

    void ResetIsShowing()
    {
        isShowing = false;
    }
}
