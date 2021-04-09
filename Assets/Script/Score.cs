using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
  public Text scoreText;
  public Text gameOverScoreText;
  public Text gameOverHighScoreText;
  public static Score instance;

  public int score;
  public int highScore;

  private void Awake(){
    if (instance != null && instance != this){
      Destroy(gameObject);
		}
    else {
      instance = this;
		}
    Kill.onKill += OnDeath;
    score = 0;
    AddScore.onCollide += UpdateScore;
  }

  private void Start(){
    highScore = PlayerPrefs.GetInt("HighScore", 0);
    UpdateScore(0);
  }


  void UpdateScore(int increment){
    score += increment;
    scoreText.text = score + "";
    Debug.Log(score);
  }

  private void OnDestroy(){
    AddScore.onCollide -= UpdateScore;
    Kill.onKill -= OnDeath;
  }

  void OnDeath(){
    if (score > highScore)
		{
      PlayerPrefs.SetInt("HighScore", score); // Saves the high score into our "HighScore" key
		}
    gameOverScoreText.text = "Score: " + score;
    gameOverHighScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
    Debug.Log("Current High Score is: " + PlayerPrefs.GetInt("HighScore"));
  }
}
