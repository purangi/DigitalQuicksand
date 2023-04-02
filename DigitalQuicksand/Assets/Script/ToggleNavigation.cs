using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ToggleNavigation : MonoBehaviour, ISelectHandler, IDeselectHandler, IMoveHandler
{
    private Toggle toggle;
    private bool isSelected;

    void Start()
    {
        toggle = GetComponent<Toggle>();
        isSelected = toggle.isOn;
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
    }

    public void OnMove(AxisEventData eventData)
    {
        if (eventData.moveDir == MoveDirection.Right && isSelected && toggle.navigation.selectOnRight != null)
        {
            toggle.navigation.selectOnRight.Select();
        }
        else if (eventData.moveDir == MoveDirection.Left && isSelected && toggle.navigation.selectOnLeft != null)
        {
            toggle.navigation.selectOnLeft.Select();
        }
        else if (eventData.moveDir == MoveDirection.Up && isSelected && toggle.navigation.selectOnUp != null)
        {
            toggle.navigation.selectOnUp.Select();
        }
        else if (eventData.moveDir == MoveDirection.Down && isSelected && toggle.navigation.selectOnDown != null)
        {
            toggle.navigation.selectOnDown.Select();
        }
    }
}
