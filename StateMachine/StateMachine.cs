using Godot;
using System.Collections.Generic;
using System.Threading.Tasks;


public class StateMachine : Node
{
	[Signal]
	public delegate void Transitioned(string stateName);

	
	[Export]
	public NodePath InitialState;

	public State State;

	public override void _Ready()
	{
		State = GetNode<State>(InitialState);

		foreach (State child in GetChildren())
		{
			child._stateMachine = this;
		}

		State.Enter();
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		State.HandleInputs(@event);
	}

	public override void _Process(float delta)
	{
		State.Update(delta);
	}

	public override void _PhysicsProcess(float delta)
	{
		State.PhysicsUpdate(delta);
	}


	public void TransitionTo(string targetStateName, Dictionary<string, bool> message = null)
	{
		if (!HasNode(targetStateName))
		{
			return;
		}

		State.Exit();
		State = GetNode<State>(targetStateName);
		State.Enter(message);
		EmitSignal(nameof(Transitioned), State.Name);
	}
}
