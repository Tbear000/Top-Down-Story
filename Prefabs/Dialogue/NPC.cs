using Godot;
using System;
using System.Collections.Generic;

public class NPC : KinematicBody2D
{
    [Export]
    public Dialogue conversation;
    public DialogueManager dialogueManager;
    public bool _overlap = false;
    private AnimationPlayer animationPlayer;
    public StateMachine _stateMachine;

    public Navigation2D navigation2D;

    public List<Vector2> path = new List<Vector2>();
    public Vector2 Velocity = Vector2.Zero;
    [Export]
    public int speed = 50;
    public RandomNumberGenerator rng = new RandomNumberGenerator();

    public Vector2 StartPos;

    public Timer timer;

    public override void _Ready()
    {
        timer = GetNode<Timer>("Timer");
        StartPos = GlobalPosition;
        rng.Randomize();
        _stateMachine = GetNode<StateMachine>("StateMachine");
        dialogueManager = GetNode<DialogueManager>("/root/DialogueManager");
        animationPlayer = GetNode<AnimationPlayer>("AnimationPlayer");
        if(GetTree().HasGroup("LevelNavigation")){
            navigation2D = (Navigation2D)GetTree().GetNodesInGroup("LevelNavigation")[0];
        }
    }

    public void _on_Area2D_body_entered(Node body)
    {
        if(body.Name == "Player"){
            animationPlayer.Play("Active");
            _overlap = true;
        }
    }

    public void _on_Area2D_body_exited(Node body)
    {
        if(body.Name == "Player"){
            animationPlayer.Play("Close");
            _overlap = false;
         }
    }

    public void _on_AnimationPlayer_animation_finished(String animName)
    {
        if(animName == "Close"){
            animationPlayer.Play("Disabled");
        }
    }

    public void Interact()
    {
        dialogueManager.ShowDialogue(conversation);
    }

    public void GeneratePath()
    {
        if(navigation2D != null){
            Vector2 TargetPos = StartPos;
            if(GlobalPosition.x < StartPos.x + 150 || GlobalPosition.x > StartPos.x - 150){
                TargetPos.x = StartPos.x + rng.RandiRange(-150,150);
            }
            if(GlobalPosition.y < StartPos.y + 150 || GlobalPosition.y > StartPos.y - 150){
                TargetPos.y = StartPos.y +  rng.RandiRange(-150,150);
            }
            var temppath = navigation2D.GetSimplePath(GlobalPosition, TargetPos);
            path = new List<Vector2>(temppath);
        }
    }

    public void Navigate()
    {
        if(path.Count > 0){
            Velocity = GlobalPosition.DirectionTo(path[0]) * speed;
            if(GlobalPosition.x >= path[0].x - 0.5f && GlobalPosition.x <= path[0].x + 0.5f){
                if(GlobalPosition.y >= path[0].y - 0.5f && GlobalPosition.y <= path[0].y + 0.5f){
                    path.Remove(path[0]);
                }
            }
        } else {
            Velocity = Vector2.Zero;
        }
    }

    public  void MoveActor()
    {
        Velocity = MoveAndSlide(Velocity);
    }
}
