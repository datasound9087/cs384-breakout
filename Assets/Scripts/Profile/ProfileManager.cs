using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ProfileManager
{
    private static ProfileManager instance = null;

    private List<Profile> profiles;
    private Profile curentProfile = null;
    private const int MaxNumProfiles = 4;

    private ProfileManager()
    {
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
        if (profiles.Contains(profile))
        {
            return;
        }
        profiles.Add(profile);
    }

    public void SetActiveProfile(int profileIndex)
    {
        curentProfile = profiles[profileIndex];
    }

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
        if (profile == curentProfile)
        {
            curentProfile = null;
        }
        profiles.Remove(profile);
        SaveProfiles();
    }

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
        ProfileIO.SaveProfiles(profiles);
    }
}