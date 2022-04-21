using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Profile reading and saving to and from JSON.
*/
public class ProfileIO
{
    // Where to find the profiles in PayerPrefs
    private const string ProfilesKey = "Profiles";
    private const string NoSavedProfiles = "NoSavedProfiles";

    // Read all profile info from PlayerPrefs and decode JSON
    public static List<Profile> ReadProfiles()
    {
        string profilesStr = PlayerPrefs.GetString(ProfilesKey, NoSavedProfiles);
        if (profilesStr == NoSavedProfiles)
        {
            // No profiles exist
            return new List<Profile>();
        }
        
        ProfileJSON profileJSON = JsonUtility.FromJson<ProfileJSON>(profilesStr);
        return profileJSON.profiles;
    }

    // Save all profile data into JSON and then into PlayerPrefs
    public static void SaveProfiles(List<Profile> profiles)
    {
        ProfileJSON profileJSON = new ProfileJSON();
        profileJSON.profiles = profiles;

        string profilesStr = JsonUtility.ToJson(profileJSON);
        PlayerPrefs.SetString(ProfilesKey, profilesStr);
    }
}