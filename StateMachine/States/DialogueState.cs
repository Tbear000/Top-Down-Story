using Godot;
using System;
using System.Collections.Generic;

public class DialogueState : PlayerState
{
    public override void Enter(Dictionary<string, bool> message = null)
	{
	}

    public override void PhysicsUpdate(float delta)
	{
        if(!DialogueManager.InDialogue){
            _stateMachine.TransitionTo("Walk");
        }
    }
}

