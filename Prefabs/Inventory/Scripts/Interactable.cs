using Godot;
using System;

public class Interactable : Node2D, IInteract
{
    public virtual void Interact(Player _player)
    {
        GD.Print("Interacted with " + this.Name);
    }
}
