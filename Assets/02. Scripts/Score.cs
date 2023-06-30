using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public bool isHighScore;

    float highScore;
    Text uiText;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();

        if (isHighScore) {
            highScore = PlayerPrefs.GetFloat("Score");
            uiText.text = highScore.ToString("F0");
        }
    }

    void LateUpdate()
    {
        if (!GameManager.isLive)
            return;

        if (isHighScore && GameManager.score < highScore)
            return;
        
        uiText.text = GameManager.score.ToString("F0");
    }
}
