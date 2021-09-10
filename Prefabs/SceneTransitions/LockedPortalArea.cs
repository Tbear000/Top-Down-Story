using Godot;
using System;

public class LockedPortalArea : PortalArea
{
    [Export(PropertyHint.ResourceType)]
    public Dialogue lockedconversation = new Dialogue();

    [Export(PropertyHint.ResourceType)]
    public Dialogue Unlocked = new Dialogue();
    public DialogueManager dialogueManager;

    private bool locked = true;

    public override void _Ready()
    {
        base._Ready();
        dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
    }
    public override void Interact()
    {
        if(player.inventory.InvQuery("Brass Key", 1) == -1 && locked){
            dialogueManager.ShowDialogue(lockedconversation);
        } else if (player.inventory.InvQuery("Brass Key", 1) != -1 && locked){
            dialogueManager.ShowDialogue(Unlocked);
            if(!DialogueManager.InDialogue){
                locked = false;
            }
        } else if (!locked){
            Global.PlayerInitialMapPosition = PlayerSpawnLocation;
		    GetTree().ChangeScene(NextScenePath);
        }
    }
}
