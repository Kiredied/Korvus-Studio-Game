using UnityEngine;

[System.Serializable] public struct SoundEffect
{
    public string groupID;
    public AudioClip[] clips;
}

public class SoundLibrary : MonoBehaviour
{
    //Creating an array of sound effects that can be accessed in the inspector. Each sound effect has a group ID and an array of audio clips associated with it. This allows for easy organization and retrieval of sound effects based on their group ID.
    public SoundEffect[] soundEffects;
    public AudioClip GetClipFromName(string name)
    {
        foreach (var soundEffect in soundEffects)
        {
            if (soundEffect.groupID == name)
            {
                // Return a random clip from the array of clips associated with the sound effect
                return soundEffect.clips[Random.Range(0, soundEffect.clips.Length)];
            }
            
        }
         return null; // Return null if no matching sound effect is found
    }

        
    }

