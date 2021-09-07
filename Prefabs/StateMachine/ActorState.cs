using System;
using Godot;
using System.Threading.Tasks;

public class ActorState : State
{
    protected NPC _actor;

    public override void _Ready()
	{
		_actor = Owner as NPC;

		if (_actor == null)
		{
			throw new InvalidProgramException("Actor is null in the ActorState type check.");
		}
	}

}