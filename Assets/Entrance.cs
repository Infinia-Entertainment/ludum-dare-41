using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Entrance : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
