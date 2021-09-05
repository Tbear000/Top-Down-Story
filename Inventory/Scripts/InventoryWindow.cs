using Godot;
using System;

public class InventoryWindow : MarginContainer
{
    [Export]
    public PackedScene SlotTemplate;
    public GridContainer gridContainer;
    public InventoryComponent ParentInventory;
    public Godot.Collections.Array Item_List = new Godot.Collections.Array();
    private Label Title;
    public override void _Ready()
    {
        Title = GetNode<Label>("Background/UpperOverlay/InventoryName");
        gridContainer = GetNode<GridContainer>("Background/InventoryGrid");
        for (int i = 0; i < ParentInventory.NumberOfSlots; i++)
        {
            var slot = (ItemSlot)SlotTemplate.Instance();
            slot.slotIndex = i;
            gridContainer.AddChild(slot);
            Item_List.Add(slot);
        }
        Title.Text = ParentInventory.InvName;
    }

    private void _on_CloseButton_pressed()
    {
        GetTree().Paused = false;
        QueueFree();
    }

}//done
