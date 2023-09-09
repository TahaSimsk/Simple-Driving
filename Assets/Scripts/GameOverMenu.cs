using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text youScoredText;
    [SerializeField] TMP_Text highScoreText;


    Scene targetScene;

    void Start()
    {
        SceneManager.LoadScene("1_Game", LoadSceneMode.Additive);
        Scene targetScene = SceneManager.GetSceneByBuildIndex(1);

        if (targetScene.isLoaded)
        {
            GameObject scoreDisplay = GameObject.Find("ScoreDisplay");

            if (scoreDisplay!=null)
            {
                Debug.Log("found");
            }
            else
            {
                Debug.Log("null");
            }
        }
        else
        {
            Debug.Log("not loaded");
        }

        SceneManager.UnloadSceneAsync("1_Game");








    }


    void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }

    void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
