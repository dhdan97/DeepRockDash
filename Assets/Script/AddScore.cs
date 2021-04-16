using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour {
  private static Dictionary<string, int> gemValues = 
    new Dictionary<string, int>()
    {
      {"Green", 2 },
      {"Blue", 10 }
    };
  private int currentGemValue = 0;

  public delegate void ScoreHandler(int increment);
  public static event ScoreHandler onCollide;

  void OnTriggerEnter2D(Collider2D col){
    if (col.tag == "Player"){
     // Debug.Log("Current Gem NAME:\t" + gameObject.name);
      foreach (KeyValuePair<string, int> gemValue in gemValues) {
        if (gameObject.name.Contains(gemValue.Key)) {
          currentGemValue = gemValue.Value;
          break;
        }
      }
      onCollide(currentGemValue);
      Destroy(gameObject);
    }
  }
}
