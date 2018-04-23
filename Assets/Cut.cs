using UnityEngine;

public class Cut : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (PlayerPrefs.GetInt("Killed") == 0)
        {
            gameObject.SetActive(false);
        }
	}

}
