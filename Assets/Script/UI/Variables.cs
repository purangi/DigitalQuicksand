using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class Variables : MonoBehaviour
{
    [YarnFunction("get_week")]
    public static int GetWeek()
    {
        return GameManager.instance.character.Week;
    }
}
