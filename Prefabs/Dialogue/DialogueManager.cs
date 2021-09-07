using Godot;
using System;
using System.Collections.Generic;

public class DialogueManager : CanvasLayer //Auto-load
{
    private int WordSpeed = 25;
    private Tween tween;
    //UI Reference
    private NinePatchRect DialogueBox;
    private Label DialogueText;
    private Label DialogueName;
    private NinePatchRect ReplyBox;
    private Label ReplyText;
    private Label ReplyName;
    private NinePatchRect PopUpBox;
    private Label PopUpText;

    //Variables
    private int CurrentLine = 0;
    private Dialogue dialogue;
    public static bool InDialogue = false;
    private bool Talking = false;

    public override void _Ready()
    {
        tween = GetNode<Tween>("Tween");
        DialogueBox = GetNode<NinePatchRect>("DialogueBox");
        DialogueText = DialogueBox.GetNode<Label>("VBoxContainer/Text");
        DialogueName = DialogueBox.GetNode<Label>("VBoxContainer/Name");
        ReplyBox = GetNode<NinePatchRect>("ReplyBox");
        ReplyText = ReplyBox.GetNode<Label>("VBoxContainer/Text");
        ReplyName = ReplyBox.GetNode<Label>("VBoxContainer/Name");
        PopUpBox = GetNode<NinePatchRect>("PopUpBox");
        PopUpText = PopUpBox.GetNode<Label>("Text");
    }

    public void ShowDialogue(Dialogue convo)
    {
        if(convo == null){
            GD.Print("Err");
        } else {
            dialogue = convo;
        if(InDialogue)
        {
            ContinueDialogue();
        } else {
            if(dialogue.isPopup){
                InDialogue = true;
                PopUpBox.Visible = true;
                PopUpText.PercentVisible = 0;
                tween.InterpolateProperty(PopUpText, "percent_visible", 0, 1, dialogue.Lines[0].Length/15, Tween.TransitionType.Linear, Tween.EaseType.Out);
                PopUpText.Text = dialogue.Lines[0];
                tween.Start();
                ++CurrentLine;
            } else {
                DialogueName.Text = dialogue.NPCName;
                DialogueText.Text = "";
                DialogueBox.Visible = true;
                if(dialogue.Replies != null){
                    ReplyName.Text = Global.PlayerName;
                    ReplyText.Text = "";
                    ReplyBox.Visible = true;
                }
                DialogueText.PercentVisible = 0;
                tween.InterpolateProperty(DialogueText, "percent_visible", 0, 1, dialogue.Lines[0].Length/WordSpeed, Tween.TransitionType.Linear, Tween.EaseType.Out);
                DialogueText.Text = dialogue.Lines[0];
                tween.Start();
                InDialogue = true;
                Talking = true;
            }
        }

        }
    }

    public void ContinueDialogue()
    {
        if(InDialogue){
            if(dialogue.isPopup){
                if(CurrentLine < dialogue.Lines.Count){
                    PopUpText.PercentVisible = 0;
                    tween.InterpolateProperty(PopUpText, "percent_visible", 0, 1, dialogue.Lines[CurrentLine].Length/WordSpeed, Tween.TransitionType.Linear, Tween.EaseType.Out);
                    PopUpText.Text = dialogue.Lines[CurrentLine];
                    tween.Start();
                    ++CurrentLine;
                } else {
                    PopUpText.Text = "";
                    PopUpBox.Visible = false;
                    InDialogue = false;
                    CurrentLine = 0;
                }
            } else {
                if(dialogue.Replies != null && Talking){
                    ReplyText.PercentVisible = 0;
                    tween.InterpolateProperty(ReplyText, "percent_visible", 0, 1, dialogue.Lines[CurrentLine].Length/WordSpeed, Tween.TransitionType.Linear, Tween.EaseType.Out);
                    ReplyText.Text = dialogue.Replies[CurrentLine];
                    tween.Start();
                    Talking = false;
                    ++CurrentLine;
                } else if(dialogue.Replies == null && CurrentLine < dialogue.Lines.Count){
                    Talking = false;
                    DialogueText.PercentVisible = 0;
                    tween.InterpolateProperty(DialogueText, "percent_visible", 0, 1, dialogue.Lines[CurrentLine].Length/WordSpeed, Tween.TransitionType.Linear, Tween.EaseType.Out);
                    DialogueText.Text = dialogue.Lines[CurrentLine];
                    tween.Start();
                    ++CurrentLine;
                } else if(CurrentLine < dialogue.Lines.Count && !Talking){
                    DialogueText.PercentVisible = 0;
                    tween.InterpolateProperty(DialogueText, "percent_visible", 0, 1, dialogue.Lines[CurrentLine].Length/WordSpeed, Tween.TransitionType.Linear, Tween.EaseType.Out);
                    DialogueText.Text = dialogue.Lines[CurrentLine];
                    tween.Start();
                    Talking = true;
                } else {
                    CurrentLine = 0;
                    InDialogue = false;
                    DialogueBox.Visible = false;
                    ReplyBox.Visible = false;
                }
            }
        }
    }
}
