using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [Serializable]
    public class SoundEffect
    {
        public string Name;
        public AudioClip File;
    }

    [Serializable]
    public class SoundEffectTyped : SoundEffect
    {
        public DefaultTypes Type = DefaultTypes.Any;
    }

    public enum DefaultTypes
    {
        Any,
        UseItem,
        Death,
        Win
    }

    #region Singleton

    public static SoundManager Instance;

    #endregion

    #region Public Variables

    public float MaxSoundDistance { get; private set; }

    public AudioSource Music;

    public AudioSource SFX;

    public List<SoundEffect> Clips;

    public List<SoundEffectTyped> DefaultClips;

    public bool VolumeToggle
    {
        get { return _volumeToggle; }
        private set
        {
            _volumeToggle = value;

            PlayerPrefs.SetInt("VolumeToggle", (_volumeToggle ? 1 : 0));

            if (VolumeToggle)
            {
                SFX.volume = 1f;
                Music.volume = 1f;
            }
            else
            {
                SFX.volume = 0f;
                Music.volume = 0f;
            }
        }
    }

    #endregion

    #region Private Variables

    [SerializeField]
    [Range(15f, 30f)]
    private float _soundDistance;

    private bool _volumeToggle;

    #endregion

    #region Monobehaviour

    private void Awake()
    {

        // Prevent duplication of SoundManager
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this);

        Instance = this;

        MaxSoundDistance = Mathf.Pow(_soundDistance, 2);

        if (PlayerPrefs.HasKey("VolumeToggle"))
        {
            VolumeToggle = (PlayerPrefs.GetInt("VolumeToggle") == 1);
        }
        else
        {
            VolumeToggle = true;
            PlayerPrefs.SetInt("VolumeToggle", 1);
        }
    }

    private void Start()
    {
        Music.Play();
    }

    #endregion

    #region Public Methods

    public void PlaySFX(string soundName, float volume = 1f, DefaultTypes type = DefaultTypes.Any)
    {
        AudioClip clip = GetSound(soundName, type);

        if (clip) SFX.PlayOneShot(clip, volume);
    }

    public void ToggleVolume()
    {
        VolumeToggle = !VolumeToggle;
    }

    #endregion

    #region Private Methods

    private AudioClip GetSound(string soundName, DefaultTypes type = DefaultTypes.Any)
    {
        List<SoundEffect> searchList;

        switch (type)
        {
            case DefaultTypes.Any:
                searchList = Clips;
                break;
            default:
                searchList = Clips;
                break;
        }

        foreach (SoundEffect sfx in searchList)
        {
            if (sfx.Name.ToLower().Trim().Equals(soundName.ToLower().Trim()))
            {
                return sfx.File;
            }
        }

        if (type != DefaultTypes.Any)
        {
            return GetDefaultSound(type);
        }

        Debug.LogError("Sound not found - " + soundName);
        return null;
    }

    private AudioClip GetDefaultSound(DefaultTypes type)
    {
        foreach (SoundEffectTyped sfx in DefaultClips)
        {
            if (sfx.Type == type)
            {
                return sfx.File;
            }
        }

        Debug.LogError("Default sound not found for type - " + type);
        return null;
    }

    #endregion
}
