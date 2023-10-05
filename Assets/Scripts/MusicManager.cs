using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEffects {
    SmashChicken,
    Laught,
    Death
}

[System.Serializable]
public class SFXData {
    public SoundEffects soundEffect;
    public AudioClip[] arraySFX;

    public AudioClip GetRandomSFX() {
        if (arraySFX.Length == 1) return arraySFX[0];
        return arraySFX[Random.Range(0, arraySFX.Length)];
    }
}

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
    [SerializeField] AudioSource sfxAudioSource;

    [Header("Variables")]
    [SerializeField] MusicData [] musicDatas;
    [SerializeField] SFXData[] sfxData;
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

    public void PlaySFXSound(SoundEffects enumSFX) {
        sfxAudioSource.clip = GetSFXClip(enumSFX);
        sfxAudioSource.Play();
    }

    public AudioClip GetSFXClip(SoundEffects enumSFX) {
        foreach (var sfx in sfxData)
            if (sfx.soundEffect == enumSFX)
                return sfx.GetRandomSFX();

        return null;
    }
}
