using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
  public Text scoreText;
  public int score;
  public int increment = 0;

  private void Awake(){
    score = 0;
    increment = increment == 0 ? 1 : increment;
    AddScore.onCollide += UpdateScore;
  }

  public void IncreaseScore(int amount){
    score+=amount;
    scoreText.text = score + "";
    Debug.Log(score);
  }

   void UpdateScore(){
    IncreaseScore(increment);
  }

  private void OnDestroy(){
    AddScore.onCollide -= UpdateScore;
  }

}
