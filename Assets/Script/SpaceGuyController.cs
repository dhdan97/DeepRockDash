using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceGuyController : MonoBehaviour {
  Rigidbody2D spaceGuy;
  public float playerClimb;
  bool canMove;

    private void Awake()
    {
        canMove = true;
        Kill.onKill += OnDeath; // Add OnDeath function to onKill delegate event
    }

    // Start is called before the first frame update
    void Start(){
    // canMove = true;
  
    spaceGuy = GetComponent<Rigidbody2D>();
    Debug.Log("Start Position X" + spaceGuy.position[0]);
    Debug.Log("Start Position Y" + spaceGuy.position[1]);
  }

  // Update is called once per frame
  void Update(){
    if (!canMove) return;

    bool pressedSpace = Input.GetButton("Space");
    if(pressedSpace){
      spaceGuy.velocity = new Vector2(spaceGuy.velocity.x, playerClimb);
    }   

  }

    void OnDeath()
    {
        canMove = false;
        GetComponent<BoxCollider2D>().enabled = false; // So spaceGuy doesn't collect any coins when they die
    }

    private void OnDestroy()
    {
        Kill.onKill -= OnDeath; // Unsubscribve when gameobject is destoryed
    }
}
