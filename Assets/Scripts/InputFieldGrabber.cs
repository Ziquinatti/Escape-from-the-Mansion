using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputFieldGrabber : MonoBehaviour
{
    public string inputText;

    public void GrabFromInputField(string input)
    {
        inputText = input;

    }
}
