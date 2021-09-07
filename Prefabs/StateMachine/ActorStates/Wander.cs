using Godot;
using System;
using System.Collections.Generic;

public class Wander : ActorState
{
    public Timer timer;
    public override void _Ready()
    {
        _actor = Owner as NPC;
    }
    public override void Enter(Dictionary<string, bool> message = null)
	{
        if(_actor.timer != null){
           _actor.timer.Start(7); 
        }
	}

    public override void PhysicsUpdate(float delta)
	{
        _actor.MoveActor();
        _actor.Navigate();
        if(_actor._overlap){
            _stateMachine.TransitionTo("StopAndTalk");
        }
    }
}
