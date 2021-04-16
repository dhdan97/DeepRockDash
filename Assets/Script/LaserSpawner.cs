using System.Collections;
using UnityEngine;

public class LaserSpawner : MonoBehaviour {
    private GameObject gameObj;
    private MainMenuMusic spawnData;

    public GameObject laserPrefab; // Our laser gameobject from the prefab asset folder
    private float moveSpeed; // Speed of our laser (future addition: vary moveSpeed?)
    private float spawnDelay; // The delay between each laser spawn

    public int rotationMinOffset; // The min possible angle the laser will spawn with
    public int rotationMaxOffset; // The max possible angle the laser wil spawn with

    public int YMinOffset; // The min possible Y position the laser will spawn
    public int YMaxOffset; // The max possible Y position the laser will spawn

    public float scaleMinOffset; // The min possible scale the laser will spawn as
    public float scaleMaxOffset; // The max possible scale the laser will spawn as

    private void Awake(){
      Kill.onKill += OnSpaceGuyDeath; // subscribe to onDeath event
    }

    // Start is called before the first frame update
    void Start(){
      gameObj = GameObject.Find("MainMenuMusic");
      spawnData = gameObj.GetComponent<MainMenuMusic>();
      moveSpeed = spawnData.moveSpeed != 0 ? spawnData.moveSpeed : 3;
      spawnDelay = spawnData.spawnDelay !=0 ? spawnData.spawnDelay: 6;
      Debug.Log("MOVE SPEED FROM OBJECT:\t" + moveSpeed);
      Debug.Log("SPAWN DELAY FROM OBJECT:\t" + spawnDelay);
      startSpawn(); // We will start the spawn when the game starts
      InvokeRepeating("RemoveLasers", 10.0f, 5.0f);
    }

    void RemoveLasers()
    {
        GameObject[] Lasers = GameObject.FindGameObjectsWithTag("laser");
        foreach (GameObject laser in Lasers)
        {
            if(laser.transform.position.x < -15.0)
            {
                Destroy(laser);
            }
        }
    }

    public void startSpawn()
    {
        StartCoroutine("StartSpawnCo"); // Starts the spawn coroutine 
    }

    public void stopSpawn()
    {
        StopCoroutine("StartSpawnCo"); // Stops the spawn coroutine
    }

    IEnumerator StartSpawnCo() // Spawn laser and set it's RigidBody 2D's velocity
    {
        while(true)
        {
            Vector3 randRotationOffset = new Vector3(0, 0, Random.Range(rotationMinOffset, rotationMaxOffset)); // Randomized angle for lasers to spawn with
            Vector3 randYOffset = new Vector3(transform.position.x, Random.Range(YMinOffset, YMaxOffset), 0);

            GameObject laser = Instantiate(laserPrefab, // gameObject we want to spawn
                                            randYOffset, // The gameobject's position that this script is attached to
                                            Quaternion.Euler(randRotationOffset)); // The rotation we are spawning the gameObject at || TODO: Change this to randomly spawn at different angles ||

            laser.GetComponent<Rigidbody2D>().velocity = Vector2.left * moveSpeed; // Grab gameObject's velocity, set it moving in direction left, at the speed of moveSpeed
            laser.transform.localScale = new Vector3(1, Random.Range(scaleMinOffset, scaleMaxOffset), 1);

            yield return new WaitForSeconds(spawnDelay); // yield return wait for spawnDelay time
        }
    }
    // Update is called once per frame
    void OnSpaceGuyDeath()
    {
        stopSpawn();
        GameObject[] Lasers = GameObject.FindGameObjectsWithTag("laser");
        foreach(GameObject laser in Lasers)
        {
            laser.GetComponent<Rigidbody2D>().velocity = Vector2.left * 0;
        }
    }

    private void OnDestroy()
    {
        Kill.onKill -= OnSpaceGuyDeath;
    }

}
