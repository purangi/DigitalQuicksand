using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnalysisBtn : MonoBehaviour
{
    public void OnClickBackBtn()
    {
        SceneManager.LoadScene("MainBasic");
    }

    public void OnClickAnalysis()
    {
        SceneManager.LoadScene("Analysis");
    }
}
