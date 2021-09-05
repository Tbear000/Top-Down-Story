using Godot;
using System;
using Dictionary = Godot.Collections.Dictionary;

public class ItemSlotDragDrop : TextureRect
{
    public ItemSlot slot;

    public override void _Ready()
    {
        slot = (ItemSlot)GetParent();
    }

    public override object GetDragData(Vector2 position)
    {
        if(IsInstanceValid(slot.itemStruct)){
            TextureRect preview = new TextureRect();
            preview.Texture = slot.itemStruct.itemTexture;
            preview.Expand = true;
            preview.RectSize = new Vector2(80,80);
            SetDragPreview(preview);
            return slot;
        }else{
            return null;
        }
    }

    public override bool CanDropData(Vector2 position, object data)
    {
        ItemSlot temp = (ItemSlot) data;
        if(temp != null){
            if(temp.itemStruct is IInteract){
                return true;
            } else {
                return false;
            }
        } else {
            return false;
        }
    }

    public override void DropData(Vector2 position, object data)
    {
        ItemSlot temp = (ItemSlot) data;
        if(IsInstanceValid(slot.itemStruct) && slot.itemStruct.itemName == temp.itemStruct.itemName){
            if(!slot.itemStruct.isStackable || !temp.itemStruct.isStackable){
                slot.parentInventory.InvAmountList[slot.slotIndex] = temp.stackAmount;
                temp.parentInventory.InvAmountList[temp.slotIndex] = slot.stackAmount;
            } else {
                if(temp.stackAmount + slot.stackAmount > slot.itemStruct.maxStackSize){
                    slot.parentInventory.InvAmountList[slot.slotIndex] = slot.itemStruct.maxStackSize;
                } else {
                    slot.parentInventory.InvAmountList[slot.slotIndex] = slot.stackAmount + temp.stackAmount;
                }

                temp.parentInventory.InvAmountList[temp.slotIndex] -= slot.parentInventory.InvAmountList[slot.slotIndex] - slot.stackAmount;
            }
        } else {
            slot.parentInventory.InvAmountList[slot.slotIndex] = temp.stackAmount;
            temp.parentInventory.InvAmountList[temp.slotIndex] = slot.stackAmount;
        }

        slot.parentInventory.InvStructList[slot.slotIndex] = temp.itemStruct;
        temp.parentInventory.InvStructList[temp.slotIndex] = slot.itemStruct;
        slot.RefreshSlot();
        temp.RefreshSlot();
    }

    public override Control _MakeCustomTooltip(string forText)
    {
        Label label = new Label();
        if(IsInstanceValid(slot.itemStruct)){
            //label.AddColorOverride("fontColor", new Color(1.0f, 1.0f, 1.0f));
            label.Text = slot.itemStruct.description;
        } else {
            label.Text = "";
        }
        return label;
    }
}