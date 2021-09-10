using Godot;
using System;

public class AudioManager : Node
{
    private int NumberOfVoices = 8;
    public static AudioStreamPlayer MusicPlayer;
    public static float MusicVolume = 0.0f;
    public static float SFXVolume = 0.0f;
    private string bus = "master";
    public string GameMusic = "res://Prefabs/AudioManager/Ludum Dare 38 - Track 1.wav";

    private static Godot.Collections.Array<AudioStreamPlayer> AvailableChannels = new Godot.Collections.Array<AudioStreamPlayer>();
    private static Godot.Collections.Array<AudioStream> AudioQueue = new Godot.Collections.Array<AudioStream>();
    public override void _Ready()
    {
        MusicPlayer = new AudioStreamPlayer();
        AddChild(MusicPlayer);
        MusicPlayer.VolumeDb = MusicVolume;
        if(GameMusic != null){
            MusicPlayer.Stream = (AudioStream)ResourceLoader.Load(GameMusic);
            MusicPlayer.Play();   
        }
        for (int i = 0; i < NumberOfVoices; i++)
        {
            var p = new AudioStreamPlayer();
            AddChild(p);
            AvailableChannels.Add(p);
            p.Connect("finished", this, nameof(OnStreamFinished),new Godot.Collections.Array{p});
            p.Bus = bus;
            p.VolumeDb = SFXVolume;
        }
        
    }

    public void OnStreamFinished(AudioStreamPlayer channel)
    {
        AvailableChannels.Add(channel);
    }

    public static void Play(AudioStream audio)
    {
        AudioQueue.Add(audio);
    }
     public override void _Process(float delta)
     {
        if(AudioQueue.Count != 0 && AvailableChannels[0] != null){
            AvailableChannels[0].Stream = AudioQueue[0];
            AudioQueue.RemoveAt(0);
            AvailableChannels[0].Play();
            AvailableChannels.RemoveAt(0);
        }
        if(MusicPlayer.VolumeDb != MusicVolume){
                MusicPlayer.VolumeDb = MusicVolume;
        } 
     }
}
