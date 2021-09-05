using Godot;
using System;

public class NPC : KinematicBody2D
{
    [Export(PropertyHint.ResourceType)]
    public Dialogue conversation;
    public DialogueManager dialogueManager;
    private bool _overlap = false;
    private AnimationPlayer animationPlayer;

    public override void _Ready()
    {
        dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
    }

    public void _on_Area2D_body_entered(Node body)
    {
        if(body.Name == "Player"){
            animationPlayer.Play("Active");
            _overlap = true;
        }
    }

    public void _on_Area2D_body_exited(Node body)
    {
        if(body.Name == "Player"){
            animationPlayer.Play("Close");
            _overlap = false;
         }
    }

    public void _on_AnimationPlayer_animation_finished(String animName)
    {
        if(animName == "Close"){
            animationPlayer.Play("Disabled");
        }
    }

    public override void _Process(float delta)
    {
        if(_overlap && Input.IsActionJustPressed("Interact")){
			Interact();
		}
    }

    public void Interact()
    {
        dialogueManager.ShowDialogue(conversation);
    }
}
