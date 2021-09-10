using Godot;
using System;

public class Global : Node
{
    public static string CurrentScene = "res://Levels/World.tscn";
    public static Vector2 PlayerInitialMapPosition = Vector2.Zero;
    public static string PlayerName = "Eli";
    public static bool BrassKeyUnlocked = false;

    public static Godot.Collections.Array<Item> PlayerInvStructList = new Godot.Collections.Array<Item>();
    public static Godot.Collections.Array<int> PlayerInvAmountList = new Godot.Collections.Array<int>();

    public static float TimeOfDay = 15;

    public static int TimeOfDayHour;

    private int MinutesInCycle = 20;

    public override void _Ready()
    {
        this.AddToGroup("Persist");
    }

    public override void _Process(float delta)
    {
        if(TimeOfDay < 60){
            TimeOfDay += delta / MinutesInCycle;
        } else {
            TimeOfDay = 0;
        }
        TimeOfDayHour = (int)MapValue(0, 60, 1, 24, TimeOfDay);
    }

    public void SetTime(int hour)
    {
        TimeOfDay = MapValue(1, 24, 0, 60, hour);
    }

    public static float MapValue(float a0, float a1, float b0, float b1, float a)
    {
	    return b0 + (b1 - b0) * ((a-a0)/(a1-a0));
    }
}
