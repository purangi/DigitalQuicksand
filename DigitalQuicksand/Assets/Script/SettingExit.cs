using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class SettingExit : MonoBehaviour
{
    public bool tutorial = false;
    private DialogueRunner dialogueRunner;
    public GameObject[] settings;

    // Start is called before the first frame update
    void Start()
    {
        dialogueRunner = FindObjectOfType<Yarn.Unity.DialogueRunner>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckTutorial()
    {
        if(!tutorial)
        {
            tutorial = true;
            StartConversation("Tutorial2");
        }

        settings[1].SetActive(true);
        settings[0].SetActive(false);
        settings[2].SetActive(false);
    }

    private void StartConversation(string node)
    {
        dialogueRunner.StartDialogue(node);
    }
}
