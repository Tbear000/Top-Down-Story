using Godot;
using System;

public class LockedPortalArea : PortalArea
{
    [Export(PropertyHint.ResourceType)]
    public Dialogue lockedconversation = new Dialogue();
    public DialogueManager dialogueManager;

    public override void _Ready()
    {
        base._Ready();
        dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
    }
    public override void Interact()
    {
        if(player != null && player.inventory.InvQuery("Brass Key", 1) != -1){
            //player.inventory.UseItemAtSlot(player.inventory.InvQuery("Brass Key", 1));
            Global.PlayerInitialMapPosition = PlayerSpawnLocation;
		    GetTree().ChangeScene(NextScenePath);
        } else {
            dialogueManager.ShowDialogue(lockedconversation);
        }
    }
}
