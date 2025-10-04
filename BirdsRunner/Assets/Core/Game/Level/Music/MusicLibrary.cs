using UnityEngine;
using System.Collections.Generic;
using System;
using AYellowpaper.SerializedCollections;

[Serializable]
[CreateAssetMenu(fileName = "MusicLibrary", menuName = "Game/MusicLibrary")]
public class MusicLibrary : ScriptableObject
{
    [SerializedDictionary("ID", "Audio Clip")]
    public SerializedDictionary<int, AudioClip> musicTracks = new SerializedDictionary<int, AudioClip>();

    public AudioClip GetAudioClip(int key)
    {
        if (musicTracks.ContainsKey(key))
        {
            return musicTracks[key];
        }
        else
        {
            Debug.LogError("Music key not found: " + key);
            return null;
        }
    }
}