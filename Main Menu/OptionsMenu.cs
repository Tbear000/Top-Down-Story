using Godot;
using System;

public class OptionsMenu : Control
{
    private VBoxContainer Settings;
    public override void _Ready()
    {
        Settings = GetNode<VBoxContainer>("VBoxContainer/ScrollContainer/VBoxContainer");
        
    }
    public override void _Process(float delta)
    {
        if(!Settings.GetNode<CheckBox>("Music/CheckBox").Pressed){
            AudioManager.MusicVolume = (float)Settings.GetNode<HSlider>("MusicVolume/HSlider").Value;
        } else {
            AudioManager.MusicPlayer.Stop();
        }
    }

    public void OnBackButtonPressed()
    {
        this.Visible = false;
    }
}
