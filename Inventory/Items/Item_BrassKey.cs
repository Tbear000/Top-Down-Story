using Godot;
using System;

public class Item_BrassKey : Item
{

    public override void _Ready()
    {
        for (int i = 0; i < Global.PlayerInvStructList.Count; i++){
            if(IsInstanceValid(Global.PlayerInvStructList[i])){
                if(Global.PlayerInvStructList[i].itemName == "Brass Key"){
                    QueueFree();
                }
            }
        }
    }
    public override void Interact(Player _player)
    {
        base.Interact(_player);
        // if(IsInstanceValid(_player.inventory.WindowRef)){
        //     //if(GetNode<LockedPortalArea>("/root/World/YSort/LockedPortalArea")._overlap){
        //     // Need to think of way to check if the player is in range of the door.
        //         Global.BrassKeyUnlocked = true;
        //         QueueFree();
        //     //}
        // }
    }
}
