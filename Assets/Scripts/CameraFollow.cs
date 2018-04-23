using UnityEngine;
public class CameraFollow : MonoBehaviour
{

    public GameObject player;   
    public float lerpSpeed = 10f;

    private Vector3 offset;        

    // Use this for initialization
    void Start()
    {
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.transform.position + offset, Time.deltaTime * lerpSpeed);
    }
}
