using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour {
    // Start is called before the first frame update
    public string sceneName; // The name of our game scene
    public AudioSource selectSound;
    void Start(){
      selectSound = GetComponent<AudioSource>();
    }
    public async void goToScene(){
      selectSound.Play();
      await Task.Delay(700);
      SceneManager.LoadScene(sceneName);
    }
    // Update is called once per frame
    void Update(){}
}
