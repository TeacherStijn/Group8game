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
        public int backgroundId = -1; // -1 will keep the previous background image
        
        [TextAreaAttribute]
        public string text;
    }

    public Actor[] actors;
    public Sprite[] backgroundImages; // null sprites for no background image
    public Message[] messages;
}