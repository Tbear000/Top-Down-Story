using Godot;
using System;
using Dictionary = Godot.Collections.Dictionary;

public class ItemSlot : MarginContainer
{
    public int slotIndex;
    public Item itemStruct;
    public int stackAmount;
    public InventoryComponent parentInventory;
    public Label uiStackAmount;
    public TextureRect uiTexture;

    public override void _Ready()
    {
        parentInventory = GetNode<InventoryWindow>("../../../").ParentInventory;
        uiStackAmount = GetNode<Label>("Background/Overlay/StackAmount");
        uiTexture = GetNode<TextureRect>("Background/Overlay/Image");

        RefreshSlot();
    }

    public void RefreshSlot()
    {
        itemStruct = parentInventory.InvStructList[slotIndex];
        stackAmount = parentInventory.InvAmountList[slotIndex];

        if(IsInstanceValid(itemStruct)){
            uiTexture.Texture = itemStruct.itemTexture;
            uiStackAmount.Text = stackAmount.ToString();
        } else {
            stackAmount = 0;
            parentInventory.InvAmountList[slotIndex] = 0;
        }

        if(stackAmount <= 0){
            stackAmount = 0;
            parentInventory.InvAmountList[slotIndex] = stackAmount;

            uiTexture.Visible = false;
            uiStackAmount.Visible = false;

            if(IsInstanceValid(itemStruct))
                itemStruct.QueueFree();
        } else {
            uiTexture.Visible = true;
            uiStackAmount.Visible = true;
        }
    }

    public void _on_gui_input_signal(InputEvent @event)
    {
        if(@event is InputEventMouseButton eventkey){
            if(!eventkey.Pressed && eventkey.ButtonIndex == 2){
                if(IsInstanceValid(parentInventory.InvStructList[slotIndex])){
                    Player player = (Player)parentInventory.interactor;
                    InventoryComponent InteractorInvComp = player.inventory;

                    if(parentInventory == InteractorInvComp){
                        parentInventory.UseItemAtSlot(slotIndex);
                    } else if(InteractorInvComp.AddToInventory(itemStruct, stackAmount)){
                        parentInventory.InvStructList[slotIndex] = null;
                        parentInventory.InvAmountList[slotIndex] = 0;
                        RefreshSlot();
                    } else {
                        RefreshSlot();
                    }
                }
            }
        }
    }
}//done
