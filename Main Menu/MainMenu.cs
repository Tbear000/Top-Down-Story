using Godot;
using System;

public class MainMenu : Control
{
    [Export]
    public String LevelToLoad = "";

    private Control OptionsMenu;

    public override void _Ready()
    {
      OptionsMenu = GetNode<Control>("Options Menu");
    }
    public override void _Process(float delta)
    {
    }

    public void _on_NewGameButton_pressed()
    {
      
		  GetTree().ChangeScene(LevelToLoad);
    }
    public void _on_QuitButton_pressed()
    {
        GetTree().Quit();
    }

    public void _on_OptionsButton_pressed()
    {
      OptionsMenu.Visible = true;
    }
}
