using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour {
  GameObject gameObj;
  MainMenuMusic currentSpawner;
  public InputField moveSpeedInput;
  public InputField laserDelayInput;
  void Awake() {
    gameObj = GameObject.Find("MainMenuMusic");
    currentSpawner = gameObj.GetComponent<MainMenuMusic>();
  }

  public void onSaveSettings(){
    try {
      currentSpawner.moveSpeed = float.Parse(moveSpeedInput.text);
      currentSpawner.spawnDelay = float.Parse(laserDelayInput.text);
    }
    catch (FormatException){
      Debug.Log("FORMAT EXCEPTION");
      Debug.Log("Invalid moveSpeed?:\t" + moveSpeedInput.text);
      Debug.Log("Invalid laserDelay?:\t" + laserDelayInput.text);
    }
    Debug.Log(currentSpawner.moveSpeed);
    Debug.Log(currentSpawner.spawnDelay);
  }
  // Update is called once per frame
  void Update(){
        
  }
}
