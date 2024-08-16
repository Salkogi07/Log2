using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Drawing;

public class Menu : MonoBehaviour
{
    public GameObject HUD;
    public RectTransform main;

    float time = 0;
    public bool isAnim = false;


    private void Update()
    {
        if (!isAnim)
            return;

        main.localScale = Vector3.one * (1+time);
        time += Time.deltaTime;
        if(time > 1f)
        {
            SceneManager.LoadScene(1);
        }
    }

    public void StartGame()
    {
        HUD.SetActive(false);
        isAnim = true;
    }

    public void GameExit()
    {
        Application.Quit();
    }
}
