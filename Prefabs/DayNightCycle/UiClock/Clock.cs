using Godot;
using System;

public class Clock : TextureProgress
{
     public override void _Process(float delta)
     {
        if(Global.TimeOfDayHour < 32.5f){
            RadialInitialAngle = Global.MapValue(0, 30, 0, 360, Global.TimeOfDay);
        } else {
            RadialInitialAngle = Global.MapValue(32.5f, 60, 0, 360, Global.TimeOfDay);
        }
     }
}
