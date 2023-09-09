
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    [SerializeField] TMP_Text youScoredText;
    [SerializeField] TMP_Text highScoreText;


    void Start()
    {
        youScoredText.text = "You Scored" + "\n" + PlayerPrefs.GetInt(ScoreSystem.lastScoreKey, 0);
        highScoreText.text = "High Score:" + "\n" + PlayerPrefs.GetInt(ScoreSystem.highScoreKey, 0);
        
    }


    public void PlayAgain()
    {
        
       
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
