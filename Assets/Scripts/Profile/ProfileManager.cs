using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Singleton handing all Profile operations.
public sealed class ProfileManager
{
    private static ProfileManager instance = null;

    // All user profiles
    private List<Profile> profiles;

    // Currently selected profile to use
    private Profile curentProfile = null;

    private ProfileManager()
    {
        // Read all profile data
        profiles = ProfileIO.ReadProfiles();
    }

    public List<Profile> GetProfiles()
    {
        return profiles;
    }

    public int ProfileCount()
    {
        return profiles.Count;
    }

    public void AddProfile(Profile profile)
    {   
        // Do nothing if profile already exists
        if (profiles.Contains(profile))
        {
            return;
        }
        profiles.Add(profile);
    }

    // Set profile being used by player using index
    public void SetActiveProfile(int profileIndex)
    {
        curentProfile = profiles[profileIndex];
    }

    // Set active profile via reference (as long as it already exsists)
    public void SetActiveProfile(Profile profile)
    {
        if (profiles.Contains(profile))
        {
            curentProfile = profile;
        }
    }

    public Profile GetActiveProfile()
    {
        return curentProfile;
    }

    public void DeleteProfile(int profileIndex)
    {
        Profile profile = profiles[profileIndex];

        // If profile currently in use, remove it from active selection
        if (profile == curentProfile)
        {
            curentProfile = null;
        }
        profiles.Remove(profile);
        SaveProfiles();
    }

    // Return current Instance
    public static ProfileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ProfileManager();
            }
            return instance;
        }
    }

    public void SaveProfiles()
    {
        // Save all profile data
        ProfileIO.SaveProfiles(profiles);
    }
}