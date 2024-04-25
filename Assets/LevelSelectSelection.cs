using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelSelectSelection : MonoBehaviour
{
    public bool selected;
    public GameObject outline;
    public LevelSelectContollerInputManager inputManager;
    public bool activated;
    public TextMeshProUGUI buttonPrompt;

    public virtual void OnActivation() { }
}
