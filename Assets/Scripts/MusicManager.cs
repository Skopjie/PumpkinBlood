using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class MusicData {
    public GameState gameState;
    public AudioClip[] arrayMusic;
    public float timePerChange;

    [HideInInspector] public int lastMusicID = 0;
}

public class MusicManager : MonoBehaviour
{
    public static MusicManager Instance { get { return instace; } }
    private static MusicManager instace;


    [Header("Componentes")]
    [SerializeField] AudioSource musicAudioSource;

    [Header("Variables")]
    [SerializeField] MusicData [] musicDatas;
    public GameState actualGameState;

    private void Awake() {
        instace = this;
    }

    private void Start() {
        musicAudioSource = GetComponent<AudioSource>();
    }

    public void ChangeMusicState(GameState newGameState) {
        foreach (var music in musicDatas) 
            if (music.gameState == newGameState)
                PlayNewRandomMusic(music);
    }

    public void PlayNewRandomMusic(MusicData newMusicData) {
        actualGameState = newMusicData.gameState;

        if (newMusicData.arrayMusic.Length == 1) PlayNewMusic(newMusicData.arrayMusic[0]);
        else {
            int randomClip = Random.Range(0, newMusicData.arrayMusic.Length);
            while(randomClip == newMusicData.lastMusicID) 
                randomClip = Random.Range(0, newMusicData.arrayMusic.Length);

            newMusicData.lastMusicID = randomClip;
            PlayNewMusic(newMusicData.arrayMusic[randomClip]);
        }

    }

    public void PlayNewMusic(AudioClip musicClip) {
        musicAudioSource.clip = musicClip;
        musicAudioSource.Play();
    }
}
