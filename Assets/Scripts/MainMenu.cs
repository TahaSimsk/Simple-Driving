using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] TMP_Text highScoreText;
    [SerializeField] Button playButton;
    [SerializeField] Color disableTransparency;


    void Start()
    {
        int highScore = PlayerPrefs.GetInt(ScoreSystem.highScoreKey, 0);
        int lastScore = PlayerPrefs.GetInt(ScoreSystem.lastScoreKey, 0);

        highScoreText.text = "High Score:\n" + highScore; 
        
        
    }

    public void Play()
    {
        playButton.image.color = disableTransparency;

        SceneManager.LoadScene(1);
    }









}
