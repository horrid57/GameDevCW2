using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    public Sentence[] sentences;
}


[System.Serializable]
public class Sentence
{
    public float typingSpeed;
    public string name;
    [TextArea(3, 10)]
    public string text;
}
