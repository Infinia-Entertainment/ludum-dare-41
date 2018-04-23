using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FirstPersonUIController : MonoBehaviour {

    public Text attackText;
   //public Text protectText;
    public Text deathText;
    public Text winText;
    public GameObject resultPanel;
    public GameObject cut;
    private float timeLeft;

    [SerializeField]
    private Camera mainCamera;

    int randomNum = 0; //random digit for a letter
    char attackLetter; //,protectLetter

    string pickedLetter;
    KeyCode thisKeyCode;


    public bool isMonster = false;
    public Text scoreText;
    public int score;
    public int minusedScore = 50;
    public int numberOfRuns;

    public int killed;
    private  bool timeOutReached = false;

    public bool devMode = false;
    // Use this for initialization
    void Awake () {
        if (devMode)
        {
            score = 100;
            numberOfRuns = 0;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("NumberOfRuns", numberOfRuns);
        }
        numberOfRuns = PlayerPrefs.GetInt("NumberOfRuns");
        numberOfRuns += 1;
        PlayerPrefs.SetInt("NumberOfRuns", numberOfRuns);

        score = PlayerPrefs.GetInt("Score");
        
        scoreText.text = score.ToString();

        if (numberOfRuns == 1)
        {
            minusedScore = 50;
        }
        else
        {
            minusedScore = -50 + (numberOfRuns * 99);
        }

        timeLeft = mainCamera.GetComponent<TimeController>().timeVal;    


        randomNum = Random.Range(0, 26); //random digit for a letter
        int lastNum = randomNum;
        attackText.text += (char)('A' + randomNum);// picks a random letter

        pickedLetter = (attackText.text[attackText.text.Length - 1]).ToString();

        randomNum = Random.Range(0, 26);
        while (randomNum == lastNum)
        {
            randomNum = Random.Range(0, 26);

        }

        thisKeyCode = (KeyCode)System.Enum.Parse(typeof(KeyCode), pickedLetter);
        //protectText.text += (char)('A' + randomNum);// picks a random letter


    }

    private void Update()
    {
        timeLeft = mainCamera.GetComponent<TimeController>().timeVal;

        //Debug.Log(timeLeft);
        if (timeLeft <= 0f && timeOutReached == false)
        {
            timeOutReached = true;
            StartCoroutine(TimeOut());
        }

        else if (Input.GetKeyDown(thisKeyCode) && timeOutReached == false)
        {
            timeOutReached = true;
            StartCoroutine(CorrectInput());
        }
    }

    IEnumerator CorrectInput()
    {
        killed = 1;
        PlayerPrefs.SetInt("Killed", killed);
        cut.SetActive(true);
        yield return new WaitForSeconds(0.4f);
        if (isMonster == true)
        {
            score = score + (numberOfRuns * 100);
            


            winText.text = "+ " + (100 * numberOfRuns).ToString() + "!";
            winText.gameObject.SetActive(true);
            resultPanel.SetActive(true);
        }
        else
        {
            Debug.Log(minusedScore);
            score = score - minusedScore;
            

            deathText.text = "- " + minusedScore + " points";

            deathText.gameObject.SetActive(true);
            resultPanel.SetActive(true);
        }
        PlayerPrefs.SetInt("Score", score);
        scoreText.text = score.ToString();
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    } 
    IEnumerator TimeOut()
    {
        killed = 0;
        PlayerPrefs.SetInt("Killed", killed);
        if (isMonster == false)
        {
            score = score + (numberOfRuns * 100);

            winText.text = "+ " + (100 * numberOfRuns).ToString() + "!";
            winText.gameObject.SetActive(true);
            resultPanel.SetActive(true);
            scoreText.text = score.ToString();
            PlayerPrefs.SetInt("Score", score);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            deathText.text = "You DIED...";
            score = 0;
            scoreText.text = "";
            numberOfRuns = 0;
            PlayerPrefs.SetInt("Score", score);
            PlayerPrefs.SetInt("NumberOfRuns", numberOfRuns);
            deathText.gameObject.SetActive(true);
            resultPanel.SetActive(true);
            yield return new WaitForSeconds(3f);
            SceneManager.LoadScene("Main Menu");
        }
    }

}
