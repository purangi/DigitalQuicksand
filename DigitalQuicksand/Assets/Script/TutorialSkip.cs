using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TutorialSkip : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SkipTutorial);
    }

    void SkipTutorial()
    {
        SceneManager.LoadScene("CharacterCreation");
    }
}
