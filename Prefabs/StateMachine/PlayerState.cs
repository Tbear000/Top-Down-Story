using Godot;
using System;
using System.Threading.Tasks;

public class PlayerState : State
{
	protected Player _player;

	public override void _Ready()
	{
		_player = Owner as Player;

		if (_player == null)
		{
			throw new InvalidProgramException("Player is null in the PlayerState type check.");
		}
	}
}
