using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kill : MonoBehaviour
{
    public delegate void KillHandler(); // A delegate; that is, a container for (possibly multiple) functions
    public static event KillHandler onKill; // The delegate event
    private void OnTriggerEnter2D(Collider2D collision) // Unity event, called when two colliders with isTrigger is touched
    {
        if(collision.tag == "Player") // Use tags to check for player in case there are other collisions in the game like pick ups
        {
            onKill(); // Invokes the delegate event
        }
    }
}
