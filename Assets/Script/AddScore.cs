using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddScore : MonoBehaviour {
  public delegate void ScoreHandler();
  public static event ScoreHandler onCollide;
  // Start is called before the first frame update
  void OnTriggerEnter2D(Collider2D col){
    if (col.tag == "Player"){
      onCollide();
      Destroy(gameObject);
    }
  }
}
