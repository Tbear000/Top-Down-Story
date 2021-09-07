using Godot;
using System;
using System.Collections.Generic;

public class StopAndTalk : ActorState
{
    public override void Enter(Dictionary<string, bool> message = null)
	{
        _actor.timer.Stop();
	}

    public override void PhysicsUpdate(float delta)
	{
        if(Input.IsActionJustPressed("Interact")){
            _actor.Interact();
        }
        if(!_actor._overlap){
            _stateMachine.TransitionTo("Wander");
        }
    }
}