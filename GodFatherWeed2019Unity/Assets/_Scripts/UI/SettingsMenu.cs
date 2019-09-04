using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingsMenu : MonoBehaviour {

	public void SetVolume (float volume)
	{
        SoundManager.Instance.ChangeVolumeMusic(volume);
        SoundManager.Instance.ChangeVolumeSFX(volume);
	}
}
