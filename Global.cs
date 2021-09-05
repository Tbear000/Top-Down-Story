using Godot;
using System;

public class Global : Node
{
    public static Vector2 PlayerInitialMapPosition = Vector2.Zero;
    public static string PlayerName = "Eli";
    public static bool BrassKeyUnlocked = false;

    public static Godot.Collections.Array<Item> PlayerInvStructList = new Godot.Collections.Array<Item>();
    public static Godot.Collections.Array<int> PlayerInvAmountList = new Godot.Collections.Array<int>();
}
