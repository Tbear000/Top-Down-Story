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
        AudioManager.MusicVolume = (float)Settings.GetNode<HSlider>("MusicVolume/HSlider").Value;
    }

    public void OnBackButtonPressed()
    {
        this.Visible = false;
    }
}
