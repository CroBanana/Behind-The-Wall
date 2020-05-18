using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogManager : MonoBehaviour
{
    private Queue<string> names;
    private Queue<string> sentences;
    private GameObject player;

    public Text nameText;
    public Text sentenceText;

    // Start is called before the first frame update
    void Start()
    {
        names = new Queue<string>();
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void StartDialoge(Dialog dialoge)
    {
        Debug.Log("Hello seth here");
        sentences.Clear();
        nameText = transform.Find("Panel/Name").GetComponent<Text>();
        sentenceText = transform.Find("Panel/MainText").GetComponent<Text>();

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
        player.GetComponent<PlayerMovment>().EndConversation();
    }
}
