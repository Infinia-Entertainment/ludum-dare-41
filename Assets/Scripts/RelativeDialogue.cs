using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class RelativeDialogue : MonoBehaviour {

    public Text nameText;
    public Text dialogueText;
    public Dialogue dialogueNormal;
    public Dialogue dialogueKilled;
    public float animationTime = 1f;
    public Animator animator;

    public Queue<string> sentences;

    int count = 0;

    // Use this for initialization
    void Start()
    {
        sentences = new Queue<string>();
        int killed = PlayerPrefs.GetInt("Killed");
        if (killed == 0)
        {
            StartDialogue(dialogueNormal);
        }
        else
        {
            StartDialogue(dialogueKilled);
        }
    }

    public void StartDialogue(Dialogue dialogue)
    {
        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        count = 0;
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        count++;
        if (sentences.Count == 0 && count % 2 != 0)
        {
            EndDialogue();
            return;
        }

        if (count % 2 != 0)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeWrite(sentence));
        }
    }

    IEnumerator TypeWrite(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            if (count % 2 != 0)
            {
                dialogueText.text += letter;
                yield return new WaitForSeconds(0.032f);
            }
            else
            {
                dialogueText.text += letter;
                yield return new WaitForFixedUpdate();
            }
        }
        count = 0;
    }
    public void Skip()
    {
        EndDialogue();
    }
    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        StartCoroutine(LoadNextDialogue());
    }
    IEnumerator LoadNextDialogue()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
