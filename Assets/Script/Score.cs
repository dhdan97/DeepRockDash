using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {
  public Text scoreText;
  public Text gameOverScoreText;
  public Text gameOverHighScoreText;
  public GameObject GameOverCanvas;
  public GameObject NameText;
  public GameObject NameInputField;

  private AudioSource getGemSound;
  public static Score instance;
  public static int score;
  public static int highScore;

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
    getGemSound = GetComponent<AudioSource>();
    highScore = PlayerPrefs.GetInt("HighScore", 0);
    score = 0;
  }

  void UpdateScore(int increment){
    getGemSound.Play();
    score += increment;
    scoreText.text = score + "";
    Debug.Log(score);
  }

  private void OnDestroy(){
    AddScore.onCollide -= UpdateScore;
    Kill.onKill -= OnDeath;
  }

  void OnDeath(){

    /*if (score > highScore){
      PlayerPrefs.SetInt("HighScore", score); // Saves the high score into our "HighScore" key
		}*/
    gameOverScoreText.text = "Score: " + score;
    gameOverHighScoreText.text = "Lowest High Score: " + PlayerPrefs.GetInt("HighScore");
    Debug.Log("High Score is: " + PlayerPrefs.GetInt("HighScore"));
  }
}
