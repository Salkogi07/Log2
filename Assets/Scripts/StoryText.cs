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
        "21���� �Ĺ�, �η��� �ڿ��� ���� ���� ��ȭ�� ���� �ؽ��� ���°� �ر��� �ް� �Ǿ����ϴ�.",
        "�̿� ���� ������ ������ ���� ġ���� ������ ������, �ᱹ ���������� ���� ������ Ȳ���������ϴ�.",
        "��Ƴ��� �Ҽ��� �ΰ����� ���ɰ� �������� ����ü, �׸��� �ڿ��� �ѷ��� ���� ���� ���� �ӿ��� ������ �̾�� �ֽ��ϴ�.",
        "���ɿ� �����Ǿ� �������� ����ü�� ���ع��� ���ΰ��� ġ������ ���ϰ� ������ ������ Ż���� �� ������?"
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
