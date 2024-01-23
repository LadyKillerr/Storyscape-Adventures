using Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;

public class JSONLoader : MonoBehaviour
{
    TextMeshPro textField;


    public TextAsset textJSON;

    [System.Serializable]
    public class StoryData
    {
        public string id;
        public string story;
        public string[] sentences;
        public string[] noun;
        public QuestionData[] question;
    }

    [System.Serializable]
    public class QuestionData
    {
        public string q;
        public string[] a;
        public int c;
    }

    void Start()
    {

    }

    void Awake()
    {
        textField = GetComponent<TextMeshPro>();



        string jsonText = File.ReadAllText("\"D:\\VIETDEFI-games\\Storyscape-Adventures\\Assets\\Scripts\\a_cat_and_a_bat.json\"");

        StoryData storyData = JsonUtility.FromJson<StoryData>(textJSON.text);


        textField.text = storyData.story;


    }


}
