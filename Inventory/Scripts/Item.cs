using Godot;
using System;
using Dictionary = Godot.Collections.Dictionary;

public class Item : Interactable , IInteract
{
	[Export]
	public string itemName;
	[Export]
	public AtlasTexture itemTexture;
	[Export]
	public string description;
	[Export]
	public bool isConsumable;
	[Export]
	public bool isStackable;
	[Export]
	public int maxStackSize;

	public override void Interact(Player _player)
	{
		if(!IsInstanceValid(_player.inventory.WindowRef)){ //Check if an inventory is open
			Item temp = (Item)this.Duplicate();
			_player.inventory.AddToInventory(temp, 1);
			QueueFree();
		}
	}
}//done
