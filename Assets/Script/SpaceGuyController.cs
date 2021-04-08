using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class SpaceGuyController : MonoBehaviour {
  Rigidbody2D spaceGuy;
  public float boost = 0; // Rate of rocket boosters climb
  public float playerClimb = 0; // Current rocket booster climb

  static float initClimb = 0;
  static bool canMove = true;

  private void Awake(){
    Kill.onKill += OnDeath; // Add OnDeath function to onKill delegate event
  }

  // Start is called before the first frame update
  void Start(){
    spaceGuy = GetComponent<Rigidbody2D>();
    // Initialize defaults if not set
    canMove = true;
    boost = boost == 0 ? 0.1f : boost;
    playerClimb = playerClimb == 0 ? 5 : playerClimb;
    // Store init climb and debug Logs
    initClimb = playerClimb;
    Debug.Log("Start Position X" + spaceGuy.position[0]);
    Debug.Log("Start Position Y" + spaceGuy.position[1]);
  }
 
  // Update is called once per frame
  void Update(){
    if (!canMove) return;
    if (spaceGuy.velocity.y < -0.1) playerClimb = initClimb; // Check if falling to reset rocket boost
    bool pressedSpace = Input.GetButton("Space"); // Detect rocket being used afterwards
    if (pressedSpace){
      spaceGuy.velocity = new Vector2(spaceGuy.velocity.x, playerClimb);
      playerClimb += boost;
    }   
  }

  void OnDeath(){
    canMove = false;
    GetComponent<BoxCollider2D>().enabled = false; // So spaceGuy doesn't collect any coins when they die
    StopAllCoroutines();
  }

  private void OnDestroy(){
    Kill.onKill -= OnDeath; // Unsubscribve when gameobject is destoryed
  }
}
