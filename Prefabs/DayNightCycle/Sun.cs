using Godot;
using System;
public class Sun : Node2D
{
    public override void _Process(float delta)
    {
        Position = new Vector2(Global.MapValue(0, 60, 6000, -6000, Global.TimeOfDay), 0);
        
    }
}
