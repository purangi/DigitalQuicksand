using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VideoMaker : MonoBehaviour
{
    public GameObject video;
    public GameObject parent;
    public Texture[] Images;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void generateVideo()
    {
        GameObject temp = Instantiate(video);
        temp.transform.SetParent(parent.transform);
    }
}
