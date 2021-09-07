using Godot;
using System;
using System.Collections.Generic;

public class Dialogue : Resource
{
    [Export]
    public bool isPopup;
    [Export]
    public List<string> Lines;
    [Export]
    public List<string> Replies;
    [Export]
    public string NPCName;

    // public Dialogue(bool ispopup = false, List<string> lines = null, List<string> replies = null, string npcname = "")
    // {
    //     isPopup = ispopup;
    //     Lines = lines ?? new List<string>();
    //     Replies = replies ?? new List<string>();
    //     NPCName = npcname;
    // }
}