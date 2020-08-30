using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogManager : MonoBehaviour
{
    private Queue<string> names;
    private Queue<string> sentences;

    public Text nameText;
    public Text sentenceText;
    public GameObject talkingTo;

    // Start is called before the first frame update
    private void Awake() {
        names = new Queue<string>();
        sentences = new Queue<string>();
    }

    public void StartDialoge(Dialog dialoge, GameObject talkingToWho)
    {
        talkingTo=talkingToWho;
        //Debug.Log(dialoge.names[1]);
        //Debug.Log(dialoge.sentences[1]);
        //Debug.Log("Hello seth here");
        sentences.Clear();
        names.Clear();

        foreach (string name in dialoge.names)
        {
            names.Enqueue(name);
        }

        foreach (string sentence in dialoge.sentences)
        {
            sentences.Enqueue(sentence);
        }
        DisplayNextSentence();

    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            if(talkingTo.CompareTag("Guard")){
                EndDialogueGuard();
            }
            EndDialogue();
            return;
        }

        string sentence= sentences.Dequeue();
        string name= names.Dequeue();
        nameText.text = name;
        sentenceText.text = sentence;
    }

    void EndDialogue()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerInteract>().EndConversation();
    }
    void EndDialogueGuard(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
