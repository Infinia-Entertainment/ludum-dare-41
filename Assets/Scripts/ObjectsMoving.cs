using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectsMoving : MonoBehaviour
{
    public float speed = 10f;
    public float boundaryFactor = 1f;
    public float zValue = -10f;
    float xValue = 0;
    float yValue = 0;

    public Text scoreText;

    private void Update()
    {
        GetInput();
        Move();
    }
    private void Start()
    {
        scoreText.text = PlayerPrefs.GetInt("Score").ToString();
    }

    void GetInput()
    {
        xValue = Input.GetAxis("Horizontal");
        yValue = Input.GetAxis("Vertical");
    }
    void Move()
    {
        if (yValue < 0f)
        {
            yValue = 0f;
        }
        Vector3 newPos = new Vector3(-xValue / boundaryFactor, -yValue/ boundaryFactor, zValue);

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, -newPos, Time.deltaTime * speed);
    }
}

