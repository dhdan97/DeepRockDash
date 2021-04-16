using System.Collections;
using UnityEngine;

public class moveBackground : MonoBehaviour
{
  public GameObject background;
  public float moveSpeed;

  private void Awake()
  {
    Kill.onKill += OnSpaceGuyDeath;
  }
  // Start is called before the first frame update
  void Start()
  {
    startMovement();

  }

  public void startMovement()
  {
    StartCoroutine("StartMoveCo"); // Starts the spawn coroutine 
  }

  public void stopMovement()
  {
    StopCoroutine("StartMoveCo"); // Stops the spawn coroutine
  }

  IEnumerator StartMoveCo() // Spawn laser and set it's RigidBody 2D's velocity
  {
    while (true)
    {
      background.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;

      yield return new WaitForSeconds(0);
    }

  }

  void OnSpaceGuyDeath()
  {
    stopMovement();
    background.GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;
  }

  private void OnDestroy()
  {
    Kill.onKill -= OnSpaceGuyDeath;
  }
}
