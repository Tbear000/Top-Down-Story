using Godot;
using System;

public class Player : KinematicBody2D
{
    [Export]
    public int Speed;
    public Vector2 Velocity = Vector2.Zero;
    public AnimatedSprite sprite;

    //Inventory Resources
    public VBoxContainer InventoryUi;
	public InventoryComponent inventory;
	private Godot.Collections.Array<Interactable> WorldItems = new Godot.Collections.Array<Interactable>();

    public StateMachine _stateMachine;


    public override void _Ready()
    {
        inventory = GetNode<InventoryComponent>("InventoryComponent");
        _stateMachine = GetNode<StateMachine>("StateMachine");
        sprite = GetNode<AnimatedSprite>("AnimatedSprite");
        InventoryUi = GetNode<VBoxContainer>("UI/Inventory");
        if(Global.PlayerInitialMapPosition != Vector2.Zero){
            this.GlobalPosition = Global.PlayerInitialMapPosition;
        }
        if(Global.PlayerInvStructList.Count > 0){
            inventory.InvStructList = Global.PlayerInvStructList;
            inventory.InvAmountList = Global.PlayerInvAmountList;
        }
        
    }


    public override void _Process(float delta)
    {
        InteractWithWorld();
    }

    public void MovePlayer(float delta)
    {
        Velocity.x = (Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left")) * Speed;
        Velocity.y = (Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up")) * Speed;
        Velocity = MoveAndSlide(Velocity);
        ChangeSprite();
    }

    private void ChangeSprite()
    {
        if(Velocity.x < 0){
            sprite.FlipH = false;
        } else if(Velocity.x > 0){
            sprite.FlipH = true;
        }
        if(Velocity.x != 0){
            sprite.Play("Walk Side");
        } else if(Velocity.y < 0){
            sprite.Play("Walk Up");
        } else if(Velocity.y > 0){
            sprite.Play("Walk Down");
         
        }
        if(Velocity == Vector2.Zero && sprite.Animation == "Walk Down"){
            sprite.Play("Idle Down");
        } else if(Velocity == Vector2.Zero && sprite.Animation == "Walk Up"){
            sprite.Play("Idle Up");
        } else if(Velocity == Vector2.Zero && sprite.Animation == "Walk Side"){
            sprite.Play("Idle Side");
        }
    }

    private void InteractWithWorld()
	{
		if(Input.IsActionJustPressed("Interact")){
			if(WorldItems.Count > 0){
				WorldItems[0].Interact(this);
                Global.PlayerInvStructList = inventory.InvStructList;
                Global.PlayerInvAmountList = inventory.InvAmountList;
            }
		}
	}

	public void _on_Area2D_area_entered(Area2D body)
	{
		var parent = body.GetParent();
		if(parent is Interactable){
			WorldItems.Add((Interactable)parent);
		}
	}

	public void _on_Area2D_area_exited(Area2D body)
	{
		var parent = body.GetParent();
		if(parent is Interactable){
			WorldItems.Remove((Interactable)parent);
		}
	}
}
