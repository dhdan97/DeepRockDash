using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    public GameObject gameOver; // The gameOver gameobject under the Canvas gameobject
    public string gameSceneName; // The name of our game scene

    private void Awake()
    {
    Kill.onKill += OnGameOver; // Subscribes OnGameOver to the onKill event
    }

    void OnGameOver()
	{
    gameOver.SetActive(true); // Sets the active state of the gameOver panel to true
	}

    public void RestartGame()
	{
    SceneManager.LoadScene(gameSceneName); // Loads the game scene again
	}
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	private void OnDestroy()
	{
    Kill.onKill -= OnGameOver; // Unsubscribe when gameobkect is destoryed
	}
}
