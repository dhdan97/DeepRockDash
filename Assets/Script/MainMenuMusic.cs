using UnityEngine;

public class MainMenuMusic : MonoBehaviour {
  public float moveSpeed;
  public float spawnDelay;
  void Awake(){
    GameObject[] objs = GameObject.FindGameObjectsWithTag("mainmusic");
    if (objs.Length > 1) Destroy(this.gameObject);

    DontDestroyOnLoad(this.gameObject);
  }
}