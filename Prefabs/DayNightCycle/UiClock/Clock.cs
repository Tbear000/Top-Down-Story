using Godot;
using System;

public class Clock : TextureRect
{
    private TextureRect HourHand;
    private TextureRect MinuteHand;

    public override void _Ready()
    {
        HourHand = GetNode<TextureRect>("HourHand");
        MinuteHand = GetNode<TextureRect>("MinuteHand");
    }
     public override void _Process(float delta)
     {
        HourHand.RectRotation = Global.MapValue(0, 30, 0, 360, Global.TimeOfDay % 30);
        MinuteHand.RectRotation = Global.MapValue(0, 2.4f, 0, 360, Global.TimeOfDay % 2.4f);
        // if(Global.TimeOfDayHour < 32.5f){
        //     RadialInitialAngle = Global.MapValue(0, 30, 0, 360, Global.TimeOfDay);
        // } else {
        //     RadialInitialAngle = Global.MapValue(32.5f, 60, 0, 360, Global.TimeOfDay);
        // }
     }
}
