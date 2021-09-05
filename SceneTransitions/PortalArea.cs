using Godot;
using System;

public class PortalArea : Area2D
{
	[Export]
	public String NextScenePath = "";
	[Export]
	public Vector2 PlayerSpawnLocation = Vector2.Zero;
	private AnimationPlayer _UiArrow;
	public bool _overlap = false;
	public Player player;
	
	public override void _Ready()
	{
		_overlap = false;
		_UiArrow = GetNode<AnimationPlayer>("AnimationPlayer");
		if (NextScenePath.GetFile() == null)
		{
			throw new InvalidProgramException("Next Scene is null in the Portal area check.");
		}
	}
	
	private void _on_PortalArea_body_entered(object body)
	{
		if(body is Player){
		_UiArrow.Play("Active");
		_overlap = true;
		player = (Player)body;
		}
	}

	private void _on_PortalArea_body_exited(object body)
	{
		if(body is Player){
			_UiArrow.Play("Disabled");
			_overlap = false;
		}
	}

    public override void _Process(float delta)
    {
        if(Input.IsActionJustPressed("Interact") && _overlap){
			Interact();
		}
    }

	public virtual void Interact()
	{
		Global.PlayerInitialMapPosition = PlayerSpawnLocation;
		GetTree().ChangeScene(NextScenePath);
	}

}
