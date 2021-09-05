using Godot;
using System;
using Dictionary = Godot.Collections.Dictionary;

public class InventoryComponent : Node
{
    [Export]
    public String InvName;
    [Export]
    public int NumberOfSlots;
    [Export]
    public Godot.Collections.Array<PackedScene> StartingItems = new Godot.Collections.Array<PackedScene>(); //<CSharpScript>
    [Export]
    public Godot.Collections.Array<int> StartingItemAmount = new Godot.Collections.Array<int>();
    [Export]
    public PackedScene WindowScene;

    public Godot.Collections.Array<Item> InvStructList = new Godot.Collections.Array<Item>();
    public Godot.Collections.Array<int> InvAmountList = new Godot.Collections.Array<int>();
    public Node interactor;
    public InventoryWindow WindowRef;
    private Item tempitem;

    public override void _Ready()
    {
        interactor = GetParent();
        PrepareInventory();
        if(StartingItems.Count == StartingItemAmount.Count && StartingItems.Count > 0)
            AddStartingItems();
    }

    public void PrepareInventory()
    {
        if(InvStructList.Count <= 0){
             InvStructList.Resize(NumberOfSlots);
             InvAmountList.Resize(NumberOfSlots);
        }
    }

    public bool AddToInventory(Item item, int amount)
    {
        bool _succ = false;
        if(IsInstanceValid(item)){
            if(item.isStackable){
                int returnedStackindex = HasPartialStack(item);
                if(returnedStackindex != -1)
                    _succ = AddToStack(item, amount, returnedStackindex);
                else{
                    _succ = CreateStack(item, amount);
                }
            } else {
                _succ = CreateStack(item, amount);
            }
        }
        return _succ;
    }

    public int HasPartialStack(Item item)
    {
        int local_i = -1;
        for (int i = 0; i < InvAmountList.Count; i++)
        {
            if(IsInstanceValid(InvStructList[i])){
                if(InvStructList[i].itemName == item.itemName && InvAmountList[i] < item.maxStackSize){
                    local_i = i;
                    break;
                }
            }   
        }
        return local_i;
    }

    public bool CreateStack(Item item, int amount)
    {
        bool HasSpace = new bool();
        int FoundIndex = new int();
        for (int i = 0; i < InvStructList.Count; i++){
            if(InvStructList[i] == null){
                HasSpace = true;
                FoundIndex = i;
                GD.Print("Empty Slot at index " + FoundIndex);
                break;
            }
        }
        if(HasSpace){
            if(amount > item.maxStackSize){
                InvAmountList[FoundIndex] = item.maxStackSize;
                //AddToInventory(item, amount - item.maxStackSize);
            } else if(amount > 1 && !item.isStackable){
                InvAmountList[FoundIndex] = 1;
                //AddToInventory(item, amount - 1);
            } else {
                InvAmountList[FoundIndex] = amount;
            }
            tempitem = item;
            InvStructList[FoundIndex] = tempitem;
            GD.Print(InvName + " " + InvStructList[FoundIndex].Name + " " + InvAmountList[FoundIndex] + " " + FoundIndex);
            RefreshSlotAtIndex(FoundIndex);
            return true;
        } else {
            return false;
        }
    }

    public bool AddToStack(Item item, int amount, int index)
    {
        int CurrentAmount = InvAmountList[index];
        if((CurrentAmount + amount) > InvStructList[index].maxStackSize){
            InvAmountList[index] = item.maxStackSize;
            int _calculation = amount - (item.maxStackSize - CurrentAmount);
            if(AddToInventory(item, _calculation)){
                RefreshSlotAtIndex(index);
            }
        } else {
            InvAmountList[index] += amount;
            item.QueueFree();
            RefreshSlotAtIndex(index);
        }
        return true;
    }

    public void AddStartingItems()
    {
        for (int i = 0; i < StartingItems.Count; i++)
        {
            Item item = (Item)StartingItems[i].Instance();
            if(item.isStackable && StartingItemAmount[i] > item.maxStackSize){
                AddToInventory(item, item.maxStackSize);
            } else {
                AddToInventory(item, StartingItemAmount[i]);
            }
        }
    }

    public int InvQuery(String name, int amount)
    {
        int total = 0;
        int Index = -1;
        for (int i = 0; i < InvStructList.Count; i++)
        {
            if(IsInstanceValid(InvStructList[i])){
                if(InvStructList[i].itemName == name){
                    total += InvAmountList[i];
                    Index = i;
                }
            }
        }
            return Index;
    }

    public void UseItemAtSlot(int index)
    {
        if(InvAmountList[index] > 0){
            InvStructList[index].Interact((Player)interactor);
            GD.Print("Interacted with Item " + InvStructList[index].itemName);
            if(InvStructList[index].isConsumable){
                InvAmountList[index] -= 1;
                RefreshSlotAtIndex(index);
            }
        }
    }

    public void RefreshSlotAtIndex(int index)
    {
        if(IsInstanceValid(WindowRef)){
            ItemSlot slot = (ItemSlot)WindowRef.Item_List[index];
            slot.RefreshSlot();
        }
    }

    public void ToggleWindow(Player player)
    {
        if(!IsInstanceValid(WindowRef)){
            interactor = player;
            InventoryWindow newWindow = (InventoryWindow)WindowScene.Instance();
            newWindow.ParentInventory = this;
            player.InventoryUi.AddChild(newWindow);
            WindowRef = newWindow;
            GetTree().Paused = true;
        } else {
            WindowRef.QueueFree();
            interactor = GetParent();
            GetTree().Paused = false;
        }
    }
}
