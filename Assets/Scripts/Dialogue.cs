using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue", menuName = "Dialogue")]
public class Dialogue : ScriptableObject
{
    [System.Serializable]
    public class Actor
    {
        public string name;
        public Sprite avatar;
    }

    [System.Serializable]
    public class Message
    {
        public int actorId;
        public string text;
    }

    public Actor[] actors;
    public Message[] messages;
}