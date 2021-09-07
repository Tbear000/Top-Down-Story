using Godot;
using System;
using System.Collections.Generic;

public class Walk : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
	{
	}

    public override void PhysicsUpdate(float delta)
	{
        _player.MovePlayer(delta);
        if(DialogueManager.InDialogue){
            _stateMachine.TransitionTo("Dialogue");
        }
    }
}

