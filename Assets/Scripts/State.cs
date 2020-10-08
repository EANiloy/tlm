using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "State")]
public class State : ScriptableObject{

    [TextArea(14, 10)] [SerializeField] string storyText;
    [SerializeField] string button1;
    [SerializeField] string button2;
    [SerializeField] State[] nextStates;
    [SerializeField] string _dayNumber;

    public string GetDayNumber()
    {
        return _dayNumber;
    }
    public string GetStateStory()
    {
        return storyText;
    }
    public string GetButton1Text()
    {
        return button1;
    }
    public string GetButton2Text()
    {
        return button2;
    }

    public State[] GetNextState()
    {
        return nextStates;
    }
}
