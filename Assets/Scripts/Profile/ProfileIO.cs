using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProfileIO
{
    private const string ProfilesKey = "Profiles";
    private const string NoSavedProfiles = "NoSavedProfiles";
    public static List<Profile> ReadProfiles()
    {
        string profilesStr = PlayerPrefs.GetString(ProfilesKey, NoSavedProfiles);
        if (profilesStr == NoSavedProfiles)
        {
            return new List<Profile>();
        }

        ProfileJSON profileJSON = JsonUtility.FromJson<ProfileJSON>(profilesStr);
        return profileJSON.profiles;
    }

    public static void SaveProfiles(List<Profile> profiles)
    {
        ProfileJSON profileJSON = new ProfileJSON();
        profileJSON.profiles = profiles;

        string profilesStr = JsonUtility.ToJson(profileJSON);
        PlayerPrefs.SetString(ProfilesKey, profilesStr);
    }
}