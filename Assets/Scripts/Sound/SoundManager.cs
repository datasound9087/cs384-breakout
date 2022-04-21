using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public enum Mode
    {
        GAME,
        MENU
    }
    public Mode mode;
    public AudioSource audioSource;

    private bool muted = false;
    private Dictionary<string, AudioClip> audioMap;
    private const string MutedKey = "Muted";

    void Awake()
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

    private void AddSound(string name)
    {
        audioMap.Add(name, LoadSound(name));
    }

    private AudioClip LoadSound(string path)
    {
        return Resources.Load("Sound/" + path) as AudioClip;
    }

    private bool LoadMutedSetting()
    {
        return PlayerPrefs.GetInt(MutedKey, 0) == 1;
    }

    private void SaveMutedSetting()
    {
        PlayerPrefs.SetInt(MutedKey, muted == true ? 1 : 0);
    }
}
