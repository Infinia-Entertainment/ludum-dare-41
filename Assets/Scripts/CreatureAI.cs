using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CreatureAI : MonoBehaviour {

    
    private AudioSource soundSource;
    [Header("Audio Settings")]
    public AudioSource backgroundMusic;
    public float finalBGMusicVolume = 0.5f;
    public float startTime = 0f;
    public float delay = 0f;
    public float volumeIncreaseSpeed = 10f;
    public float finalVolume = 1f;
    public float timeForMassiveIncrease = 15f;
    public float massiveIncreaseFactor = 2f;

    private float originalSpeedChoice;
    private bool isPlaying = false;

    [Header("Movement")]
    public float speed = 20f;
    public float timeBeforeJump = 1f;
    public GameObject character;

    private bool firstDelay;
    private int numberOfRuns;

    bool startedLoading = false;
    // Use this for initialization
    void Start () {
        numberOfRuns = PlayerPrefs.GetInt("NumberOfRuns");

        if (numberOfRuns > 1)
        {
            delay = 3f;
            if (numberOfRuns > 2)
            {
                delay = 0f;
            }
        }

        soundSource = GetComponent<AudioSource>();
        StartCoroutine(StartSound());
        originalSpeedChoice = volumeIncreaseSpeed;
        StartCoroutine(Rotate());

    }

    IEnumerator StartSound()
    {
        yield return new WaitForSeconds(delay);
        soundSource.Play();
        soundSource.time = startTime;
        isPlaying = true;
    }
    IEnumerator Rotate()
    {
        while (true)
        {
            if (transform.position.x - character.transform.position.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            else
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
            yield return new WaitForSeconds(3f);
        }
    }
    // Update is called once per frame
    void Update()
    {

        if (Time.timeSinceLevelLoad > (delay + (soundSource.clip.length - startTime - timeBeforeJump)))
        {
            Debug.Log((delay + (soundSource.clip.length - startTime - timeBeforeJump)));
            transform.position = Vector3.Lerp(transform.position, character.transform.position, Time.deltaTime * speed);
           
        }
        if (Time.timeSinceLevelLoad > (delay + (soundSource.clip.length - startTime - timeBeforeJump + 0.4f)) && startedLoading == false)
        {
            LoadNextScene();
        }
        SoundBlend();
    }
    void LoadNextScene()
    {
        startedLoading = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    void SoundBlend()
    {
        if (isPlaying == true)
        {
            soundSource.volume = Mathf.Lerp(soundSource.volume, finalVolume, Time.deltaTime * volumeIncreaseSpeed);
            backgroundMusic.volume = Mathf.Lerp(backgroundMusic.volume, finalBGMusicVolume, Time.deltaTime * volumeIncreaseSpeed);
        }
        if (soundSource.time > timeForMassiveIncrease)
        {
            volumeIncreaseSpeed = originalSpeedChoice * massiveIncreaseFactor;
        }
    }


}
