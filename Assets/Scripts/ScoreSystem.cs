using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] float scoreMultiplier = 1f;

    public const string HIGHSCORE_KEY = "HighScore";
    public const string LASTSCORE_KEY = "LastScore";

    [HideInInspector] public float score;

    void Update()
    {
        IncrementScoreAndUpdateText();

    }

    private void IncrementScoreAndUpdateText()
    {
        score += Time.deltaTime * scoreMultiplier;
        scoreText.text = Mathf.RoundToInt(score).ToString();
    }

    void OnDestroy()
    {
        SetLastAndHighScore();
    }

    private void SetLastAndHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt(HIGHSCORE_KEY, 0);

        PlayerPrefs.SetInt(LASTSCORE_KEY, Mathf.RoundToInt(score));

        if (score > currentHighScore)
        {
            PlayerPrefs.SetInt(HIGHSCORE_KEY, Mathf.RoundToInt(score));
        }
    }
}
