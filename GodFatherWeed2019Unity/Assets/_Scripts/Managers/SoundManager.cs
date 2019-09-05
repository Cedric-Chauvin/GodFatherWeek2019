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

    public enum DefaultTypes
    {
        Any,
        UseItem,
        Death,
        Win,
        Damage
    }

    #region Singleton

    public static SoundManager Instance;

    #endregion

    #region Public Variables

    public AudioSource Music;

    public AudioSource SFX;

    public List<SoundEffect> Clips;

    public List<SoundEffect> Items;
    public List<SoundEffect> Deaths;
    public List<SoundEffect> Wins;
    public List<SoundEffect> Damage;

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
    private bool _volumeToggle;

    #endregion

    #region Monobehaviour

    private void Awake()
    {
        // Last minute code!
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;

        // Prevent duplication of SoundManager
        if (Instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        DontDestroyOnLoad(this);

        Instance = this;

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

    public void PlaySFX(string soundName, DefaultTypes type = DefaultTypes.Any, float volume = 1f)
    {
        AudioClip clip = GetSound(soundName, type);

        if (clip) SFX.PlayOneShot(clip, volume);
    }
    public void PlayRandomSFX( DefaultTypes type, float volume = 1f)
    {
        AudioClip clip = GetRandomSound(type);

        if (clip) SFX.PlayOneShot(clip, volume);
    }

    public void ToggleVolume()
    {
        VolumeToggle = !VolumeToggle;
    }

    public void ChangeVolumeMusic(float vol)
    {
        Music.volume = vol;
    }

    public void ChangeVolumeSFX(float vol)
    {
        SFX.volume = vol;
    }

    #endregion

    #region Private Methods

    private AudioClip GetSound(string soundName, DefaultTypes type = DefaultTypes.Any)
    {
        List<SoundEffect> searchList = new List<SoundEffect>();
        GetListByType(type, out searchList);

        foreach (SoundEffect sfx in searchList)
        {
            if (sfx.Name.ToLower().Trim().Equals(soundName.ToLower().Trim()))
            {
                return sfx.File;
            }
        }

        Debug.LogError("Sound not found - " + soundName);
        return null;
    }

    private AudioClip GetRandomSound(DefaultTypes type)
    {
        List<SoundEffect> searchList = new List<SoundEffect>();
        GetListByType(type, out searchList);

        if (searchList.Count > 0)
        {
            System.Random rng = new System.Random();
            int random = rng.Next(0, searchList.Count + 1);

            return searchList[random].File;
        }

        Debug.LogError("Random sound not found for type - " + type);
        return null;
    }

    private void GetListByType(DefaultTypes type, out List<SoundEffect> searchList)
    {
        switch (type)
        {
            case DefaultTypes.UseItem:
                searchList = Items;
                break;
            case DefaultTypes.Death:
                searchList = Deaths;
                break;
            case DefaultTypes.Win:
                searchList = Wins;
                break;
            case DefaultTypes.Damage:
                searchList = Damage;
                break;
            default:
                searchList = Clips;
                break;
        }
    }

    #endregion
}
