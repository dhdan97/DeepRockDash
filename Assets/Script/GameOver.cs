using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOver : MonoBehaviour{
  public GameObject gameOver; // The gameOver gameobject under the Canvas gameobject
  public GameObject NameText;
  public GameObject NameInputField;
  public string gameSceneName; // The name of our game scene
  public string mainMenuSceneName;
  //public Score score;

  private void Awake(){
    gameOver.SetActive(false);
    Kill.onKill += OnGameOver; // Subscribes OnGameOver to the onKill event
  }

  void OnGameOver(){
    gameOver.SetActive(true); // Sets the active state of the gameOver panel to true
    if (Score.score > PlayerPrefs.GetInt("HighScore", Score.score)){
      PlayerPrefs.SetInt("HighScore", Score.score); // Saves the high score into our "HighScore" key
      NameText.SetActive(true);
      NameInputField.SetActive(true); // TODO: Now implement add highscore to highscore list
    }
  }

  public void RestartGame(){
    if(NameText.activeSelf && NameInputField.activeSelf){
      Debug.Log("Name Entered: " + NameInputField.GetComponent<InputField>().text);
      HighScoreController.AddHighscoreEntry(PlayerPrefs.GetInt("HighScore", Score.score), NameInputField.GetComponent<InputField>().text);
		}
    SceneManager.LoadScene(gameSceneName); // Loads the game scene again
	}

  public void GoToMainMenu(){
    if (NameText.activeSelf && NameInputField.activeSelf){
      Debug.Log("Name Entered: " + NameInputField.GetComponent<InputField>().text);
      HighScoreController.AddHighscoreEntry(PlayerPrefs.GetInt("HighScore", Score.score), NameInputField.GetComponent<InputField>().text);
    }
    SceneManager.LoadScene(mainMenuSceneName);
	}
  // Start is called before the first frame update
  void Start(){
        
  }

  // Update is called once per frame
  void Update(){
        
  }

	private void OnDestroy(){
    Kill.onKill -= OnGameOver; // Unsubscribe when gameobkect is destoryed
	}
}
