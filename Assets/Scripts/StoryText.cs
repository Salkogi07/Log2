using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoryText : MonoBehaviour
{
    public Text textStory;
    public float typingSpeed = 0.05f;
    public float pauseBetweenTexts = 2f;

    private string[] storyTexts =
    {
        "21세기 후반, 인류는 자원의 고갈과 기후 변화로 인해 극심한 생태계 붕괴를 겪게 되었습니다.",
        "이에 따라 각국은 생존을 위해 치열한 전쟁을 벌였고, 결국 핵전쟁으로 인해 지구는 황폐해졌습니다.",
        "살아남은 소수의 인간들은 방사능과 돌연변이 생물체, 그리고 자원을 둘러싼 서로 간의 갈등 속에서 생존을 이어가고 있습니다.",
        "방사능에 오염되어 돌연변이 생명체로 변해버린 주인공은 치료제를 구하고 오염된 지구를 탈출할 수 있을까?"
    };

    private void Awake()
    {
        textStory = GetComponent<Text>();
    }

    private void Start()
    {
        textStory.text = "";
        StartCoroutine(DisplayStoryTexts());
    }
    
    IEnumerator DisplayStoryTexts()
    {
        foreach(string storytext in storyTexts)
        {
            yield return StartCoroutine(TypeText(storytext));
            yield return new WaitForSeconds(pauseBetweenTexts);
        }
        SceneManager.LoadScene(1);
    }

    IEnumerator TypeText(string text)
    {
        textStory.text = "";
        foreach(char letter in text.ToCharArray())
        {
            textStory.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    public void NextSceneClick()
    {
        SceneManager.LoadScene(1);
    }
}
