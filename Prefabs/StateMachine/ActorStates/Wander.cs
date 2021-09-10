using Godot;
using System;
using System.Collections.Generic;

public class Wander : ActorState
{
    private RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
    
    public override void _Ready()
    {
        _actor = Owner as NPC;
    }
    public override void Enter(Dictionary<string, bool> message = null)
	{
        randomNumberGenerator.Randomize();
        if(_actor.timer != null){
           _actor.timer.Start(randomNumberGenerator.RandfRange(5,10)); 
        }
	}

    public override void PhysicsUpdate(float delta)
	{
        if(_actor.Velocity == Vector2.Zero && _actor.timer.TimeLeft == 0){
            _actor.timer.Start(randomNumberGenerator.RandfRange(5,10));
            GD.Print(_actor.timer.TimeLeft);
        }
        _actor.MoveActor();
        _actor.Navigate();
        if(_actor._overlap){
            _stateMachine.TransitionTo("StopAndTalk");
        }
    }
}
