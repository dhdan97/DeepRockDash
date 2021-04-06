using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour {
  public GameObject gemPrefab;
  public float moveSpeed;
  public float spawnDelay;
  
  void Start(){
    StartSpawn();
    InvokeRepeating("RemoveGems", 10.0f, 5.0f);
    Kill.onKill += OnSpaceGuyDeath;
  }

  IEnumerator StartSpawnCo(){
    while(true){
      GameObject gems = Instantiate(gemPrefab, transform.position, Quaternion.identity);
      gems.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed;
      yield return new WaitForSeconds(spawnDelay);
    }
  }

  public void StartSpawn(){
    StartCoroutine("StartSpawnCo");
  }

  public void StopSpawn(){
    StopCoroutine("StartSpawnCo");
  }

  private static void UpdateGems(string action){
    GameObject[] Gems = GameObject.FindGameObjectsWithTag("gems");
    switch(action){
      case "destroy":
        foreach (GameObject gem in Gems){
          if(gem.transform.position.x < -15.0)  Destroy(gem);
        }
      break;
      case "pause":
        foreach(GameObject gem in Gems){
          gem.GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;
        }
      break;
      default:
        Debug.Log("Error in processing action for Gems");
        break;
    };
  }

  void RemoveGems(){
    UpdateGems("destroy");
  }
  void OnSpaceGuyDeath(){
    StopSpawn();
    UpdateGems("pause");
  }

  private void OnDestroy(){
    Kill.onKill -= OnSpaceGuyDeath;
  }
}
