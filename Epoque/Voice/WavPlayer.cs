using System;
using System.Media;

public static class WavPlayer
{
    private static SoundPlayer player;

    // Sets up the SoundPlayer object.
    private static void InitializeSound()
    {
        // Create an instance of the SoundPlayer class.
        player = new SoundPlayer();

    }

    private static void LoadSound(String filepath)
    {
        try
        {
            // Assign the selected file's path to 
            // the SoundPlayer object.  
            player.SoundLocation = filepath;

            // Load the .wav file.
            player.Load();
        }
        catch (Exception ex)
        {

        }
    }

    private static void PlaySound()
    {
        player.Play();
    }
    public static void PlayWavSound(String filepath)
    { 
        InitializeSound();
        LoadSound(filepath);
        PlaySound();
        Console.WriteLine(filepath);
    }

}