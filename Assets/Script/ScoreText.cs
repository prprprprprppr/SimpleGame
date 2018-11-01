using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreText : MonoBehaviour {

    private int Score = 0;
    private Text text;
    private Color col;

    private void Awake()
    {
        text = GetComponent<Text>();
        col = text.color;
        StartCoroutine(BlankText());
    }

    IEnumerator BlankText()
    {
        yield return new WaitForSeconds(1);
        text.text = "";
    }

    public void AddScore()
    {
        Score += 1;
        text.text = Score.ToString(); 
    }
    public void GameOver()
    {
        text.text = "Game Over";
    }
}
