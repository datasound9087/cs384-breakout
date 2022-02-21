using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class ProfileManager
{
    private static ProfileManager instance = null;

    private List<Profile> profiles;
    private Profile curentProfile;
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

    public void AddActiveProfile(Profile profile)
    {
        profiles.Add(profile);
        curentProfile = profile;
    }

    public void SetActiveProfile(int profileIndex)
    {
        curentProfile = profiles[profileIndex];
    }

    public Profile GetActiveProfile()
    {
        return curentProfile;
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