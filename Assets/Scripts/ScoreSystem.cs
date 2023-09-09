using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier = 1f;

    public const string highScoreKey = "HighScore";
    public const string lastScoreKey = "LastScore";

    [HideInInspector] public float score;
    [HideInInspector] public float score2 = 5f;

    void Update()
    {
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.RoundToInt(score).ToString();
        
    }


    void OnDestroy()
    {
        int currentHighScore = PlayerPrefs.GetInt(highScoreKey, 0);

        PlayerPrefs.SetInt(lastScoreKey, Mathf.RoundToInt(score));

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(highScoreKey, Mathf.RoundToInt(score));
        }
    }

}
