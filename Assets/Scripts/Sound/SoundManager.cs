using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    /*
     * Mode - What sounds does the manager need to load for a scene (as MonoBehaviours are reloaded inbetweeen scenes)(set in editor)
     *      MENU - Load Menu sounds
     *      GAME - Additionally load game sounds
    */
    public enum Mode
    {
        GAME,
        MENU
    }
    public Mode mode;

    // The scenes audio object to play sounds from
    public AudioSource audioSource;

    private bool muted = false;

    // Link each sound to a string
    private Dictionary<string, AudioClip> audioMap;
    private const string MutedKey = "Muted";

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioMap = new Dictionary<string, AudioClip>();
        if (mode == Mode.GAME)
        {
            LoadGameSounds();
        }

        LoadMenuSounds();
        muted = LoadMutedSetting();
    }

    public void PlaySound(string name)
    {
        if (muted)
        {
            return;    
        }

        // If sound of name exists in the audio map, play it once
        AudioClip clip;
        if (audioMap.TryGetValue(name, out clip))
        {
            audioSource.PlayOneShot(clip);
        }
    }

    public void SetMuted(bool muted)
    {
        this.muted = muted;
        SaveMutedSetting();
    }

    public bool GetMuted()
    {
        return muted;
    }

    private void LoadMenuSounds()
    {
        AddSound("MenuClick");
    }

    private void LoadGameSounds()
    {
        AddSound("BallHit");
    }

    // Add sound to map, where the filename is the same as the key
    private void AddSound(string name)
    {
        audioMap.Add(name, LoadSound(name));
    }

    // Load file from resources/sound
    private AudioClip LoadSound(string path)
    {
        return Resources.Load("Sound/" + path) as AudioClip;
    }

    // Get user sound prefs
    private bool LoadMutedSetting()
    {
        return PlayerPrefs.GetInt(MutedKey, 0) == 1;
    }

    // Save user sound prefs
    private void SaveMutedSetting()
    {
        PlayerPrefs.SetInt(MutedKey, muted == true ? 1 : 0);
    }
}
