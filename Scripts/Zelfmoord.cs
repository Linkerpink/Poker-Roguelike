using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zelfmoord : MonoBehaviour
{
    private void OnGUI()
    {
        if (GUI.Button(new Rect(10, 10, 150, 100), "I am a button"))
        {
            print("You clicked the button!");
        }
    }
}
