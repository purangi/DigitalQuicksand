using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource bgm_player;
    public AudioSource sfx_player;

    public AudioClip[] bgm_clips;
    public AudioClip[] sfx_clips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void BGMChange(string scene)
    {
        int index = 0;

        switch(scene)
        {
            case "TitleMenu": index = 0; break;
            case "Prologue": index = 1; break;
            case "MainScene": index = 2; break;
        }

        bgm_player.clip = bgm_clips[index];
        bgm_player.Play();

    }

    public void PlaySound(string type)
    {
        int index = 0;

        switch(type)
        {
            case "title_press": index = 0; break;
            case "cursor_move": index = 1; break;
            case "click_bobit": index = 2; break;
            case "prolog_text": index = 3; break;
            case "mouse_click": index = 4; break;
            case "char_creation": index = 5; break;
            case "char_error": index = 6; break;
            case "tutorial_click": index = 7; break;
            case "event_click": index = 8; break;
            case "video_click": index = 9; break;
            case "result4": index = 10; break;
            case "analysis_load": index = 11; break;
            case "result": index = 12; break; //¿£µù
        }

        sfx_player.clip = sfx_clips[index];
        sfx_player.Play();
    }
}
