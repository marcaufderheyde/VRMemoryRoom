using System;
using System.Timers;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class AudioManager : MonoBehaviour
{
    public AudioSource radio;
    public AudioClip[] playlist;
    public static AudioManager instance;
    public bool active = false; // Whether the radio is turned on or off
    public bool paused = false; // Whether the game is paused or not
    public XRController controller = null; // Listening for when we go back to the menu

    void Awake()
    {
        // Preserve the radio object so that the position in the track is maintained across scene changes 
        if (instance == null)
            instance = this;
        else {
            Destroy(this.gameObject); ; // Ensure that no duplicates of the object are created
            return;
        }

        radio.clip = playlist[0]; // Initialise the playlist
    }

    void Update() {
        // First check that we're still in the memory room
        if (controller.inputDevice.TryGetFeatureValue(CommonUsages.menuButton, out bool menuTarget))
        {
            if (menuTarget)
            {  
                PauseMusic();
            }
        }

        // If the game is not paused and the radio is turned on, and there is no song playing, then the song must have finished
        if (paused == false && active == true && !radio.isPlaying) {
            radio.clip = NextSong(radio.clip); // Load the next song in the playlist
            radio.Play(); // Play the next song
        }
    }

    public AudioClip NextSong(AudioClip song) {
        // Iterate through each song in playlist
        for (int i = 0; i < playlist.Length; i++){
            if (song == playlist[i]) {
                if (i == playlist.Length - 1) // If the final song in the playlist is being played
                    return playlist[0]; // Loop the playlist by returning to the first song
                else
                    return playlist[i + 1]; // Play the next song in the playlist
            
            }
        }
        return playlist[0];
    }

    public void PlayMusic() { // Resume the music when resuming game
        paused = false;
        radio = GetComponent<AudioSource>();
        radio.Play();
    }

    public void PauseMusic() { // Pause the music when pausing game
        paused = true;
        radio = GetComponent<AudioSource>();
        radio.Pause();
    }

    public void SkipSong() { // Pause the music when pausing game
        radio = GetComponent<AudioSource>();
        radio.clip = NextSong(radio.clip); // Load the next song in the playlist
        radio.Play(); // Play the next song
    }
}