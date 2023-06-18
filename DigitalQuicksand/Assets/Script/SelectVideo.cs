using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using MyDB;

public class SelectVideo : MonoBehaviour
{
    public Image thumb;
    public TextMeshProUGUI title;
    public TextMeshProUGUI summary;

    public VideoResult vid_result;
    public GameObject reaction_pop;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ButtonClicked()
    {
        if(reaction_pop.activeSelf == true)
        {
            reaction_pop.GetComponent<Reaction>().Stop();
        } else
        {
            reaction_pop.SetActive(true);
        }

        reaction_pop.GetComponent<Reaction>().PopUp(vid_result.Reaction);
        UpdateStat();
    }

    private void UpdateStat()
    {
        GameManager.instance.genre[vid_result.Genre_id] += vid_result.Skill;

        SmallGenre temp = GameManager.instance.small_genre.Find(x => x.Sgenre_id == vid_result.SG_ID);
        temp.Count++;
        temp.Interest++;

        if(vid_result.SG_ID2 != 0)
        {
            temp = GameManager.instance.small_genre.Find(x => x.Sgenre_id == vid_result.SG_ID2);
            temp.Count++;
            temp.Interest++;
        }

        List<int> prop = vid_result.Property;

        for(int i = 1; i <= prop.Count; i++)
        {
            GameManager.instance.property[i] += prop[i - 1];
        }

        GameManager.instance.character.This_st += vid_result.Length;
        GameManager.instance.character.Sum_st += vid_result.Length;
        GameManager.instance.character.Gold += vid_result.Length * 100;

        Destroy(this.gameObject);
    }

}
