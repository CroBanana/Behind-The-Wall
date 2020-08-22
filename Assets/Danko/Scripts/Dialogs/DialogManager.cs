﻿using System.Collections;
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
    public GameObject talkingTo;

    // Start is called before the first frame update
    private void Awake() {
        names = new Queue<string>();
        sentences = new Queue<string>();
        player = GameObject.FindGameObjectWithTag("Player");
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
        player.GetComponent<PlayerInteract>().EndConversation();
        if(talkingTo==Quest.targetss[Quest.currentObjective]){
            Quest.SetNextObjective();
        }
    }
}
