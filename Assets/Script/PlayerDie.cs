using UnityEngine;

public class PlayerDie : MonoBehaviour
{
  public GameObject colGameObject;
  // Start is called before the first frame update
  void Awake()
  {
    Kill.onKill += PlayerFall;

  }
  void PlayerFall()
  {
    colGameObject.SetActive(false);
  }

	private void OnDestroy()
	{
    Kill.onKill -= PlayerFall;
	}
}
