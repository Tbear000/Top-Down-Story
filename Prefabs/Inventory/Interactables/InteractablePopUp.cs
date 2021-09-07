using Godot;
using System;

public class InteractablePopUp : Interactable, IInteract
{
    [Export(PropertyHint.ResourceType)]
    public Dialogue conversation = new Dialogue();
    public DialogueManager dialogueManager;

    public override void _Ready()
    {
        dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
    }

    public override void Interact(Player _player)
    {
        dialogueManager.ShowDialogue(conversation);
    }
}
