using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainMenuMusic : MonoBehaviour {
  void Awake(){
    GameObject[] objs = GameObject.FindGameObjectsWithTag("mainmusic");

    if (objs.Length > 1){
      Destroy(this.gameObject);
    }

    DontDestroyOnLoad(this.gameObject);
  }
}