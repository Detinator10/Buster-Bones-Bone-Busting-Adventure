using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

    public int levelProgress = 2;
    public int lives = 3;
    public int[] score = new int[5];
    public static int player;
    public static int currentLevel;
	// Use this for initialization
	void Start () {
        DontDestroyOnLoad(this);
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitleScreen");
        player = 0;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
	}
    public void LoadScene(string level)
    {

        if (level == "progress")
        {
            levelProgress += 1;
            UnityEngine.SceneManagement.SceneManager.LoadScene(levelProgress);
            if (levelProgress == 5)
            {
                player += 1;
            }
        }else if(level == "retry" && currentLevel == 1)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 1");
        }
        else if (level == "retry" && currentLevel == 2)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 2");
        }
        else if (level == "retry" && currentLevel == 3)
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Level 3");
        }
        else
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(level);
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
    public void GameOver()
    {
        player += 1;
    }
}
