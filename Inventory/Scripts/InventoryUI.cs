using Godot;
using System;

public class InventoryUI : VBoxContainer
{
	private Player player;
	public override void _Ready()
	{
		player = (Player) GetNode("../..");
	}

	 // Called every frame. 'delta' is the elapsed time since the previous frame.
	 public override void _Process(float delta)
	 {
		 if(Input.IsActionJustPressed("Inventory"))
		{
			player.inventory.ToggleWindow(player);
		}
	 }
}
