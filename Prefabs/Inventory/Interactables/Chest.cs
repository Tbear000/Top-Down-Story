using Godot;
using System;

public class Chest : Interactable, IInteract
{
    public InventoryComponent inventory;
    public override void _Ready()
    {
        inventory = GetNode<InventoryComponent>("InventoryComponent");   
    }

    public override void Interact(Player _player)
    {
        inventory.ToggleWindow(_player);
    }
}
