using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MenuManager : MonoBehaviour {

    public Text enterText;
    public int enterButtonPressed = 0;
    public string firstSentence;
    public string secondSentence;
    public Button EnterButton;

    public GameObject HelpPanel;
    public Animator animator;

    // Use this for initialization
    void Start() {
        PlayerPrefs.SetInt("Score", 0);
        PlayerPrefs.SetInt("NumberOfRuns", 0);
        enterText.text = "";
        enterButtonPressed = 0;
        PlayerPrefs.SetInt("EnterButtonPressed", enterButtonPressed);
    }

    #region Buttons

    public void Enter_Button()
    {
        enterButtonPressed = 1;
        StartCoroutine(TypeWrite(firstSentence, secondSentence));
        PlayerPrefs.SetInt("EnterButtonPressed", enterButtonPressed);
        EnterButton.gameObject.SetActive(false);
    }

    public void Help_Button()
    {

        animator.SetBool("IsOpen", !animator.GetBool("IsOpen"));
    }


    #endregion


    IEnumerator TypeWrite(string sentenceOne, string sentenceTwo)
    {
        enterText.text = "";
        foreach (char letter in sentenceOne.ToCharArray())
        {
            enterText.text += letter;
          yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(1f);
        if(enterText.text == sentenceOne)
        {
            enterText.text = "";
            foreach (char letter in sentenceTwo.ToCharArray())
            {
                enterText.text += letter;
                yield return new WaitForFixedUpdate();
            }
        }
        yield return new WaitForSeconds(2f);
        enterText.text = "";
    }
}