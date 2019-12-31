using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public Dropdown resolutionDropdown;
	public Toggle fullscreenToggle;
	public Dropdown qualityDropdown;
	public Slider volumeSlider;

	public AudioMixer masterMixer;

	Resolution[] resolutions;

	private void Start()
	{
		InitResolutions();
		LoadOptionsValues();
	}

	private void OnEnable()
	{
		EventSystem.current.SetSelectedGameObject( resolutionDropdown.gameObject );
	}

	void InitResolutions()
	{
		resolutions = Screen.resolutions;
		resolutionDropdown.ClearOptions();

		List<string> resolutionStrings = new List<string>();

		int currentResolutionIndex = 0;
		for( int i = 0; i < resolutions.Length; ++i )
		{

			string option = resolutions[ i ].width + " x " + resolutions[ i ].height;
			resolutionStrings.Add( option );

			if( resolutions[ i ].width == Screen.currentResolution.width &&
				resolutions[ i ].height == Screen.currentResolution.height )
			{
				currentResolutionIndex = i;
			}
		}

		resolutionDropdown.AddOptions( resolutionStrings );
		resolutionDropdown.value = currentResolutionIndex;
		resolutionDropdown.RefreshShownValue();
	}

	public void SaveOptionsValues()
	{
		PlayerPrefs.SetInt( "o_resolutionIndex", resolutionDropdown.value );
		PlayerPrefs.SetInt( "o_fullscreen", fullscreenToggle.isOn ? 1 : 0 );
		PlayerPrefs.SetInt( "o_qualityIndex", qualityDropdown.value );
		PlayerPrefs.SetFloat( "o_master_volume", volumeSlider.value );
	}

	public void LoadOptionsValues()
	{
		resolutionDropdown.value = PlayerPrefs.GetInt( "o_resolutionIndex" );
		fullscreenToggle.isOn = PlayerPrefs.GetInt( "o_fullscreen" ) == 1;
		qualityDropdown.value = PlayerPrefs.GetInt( "o_qualityIndex" );
		volumeSlider.value = PlayerPrefs.GetFloat( "o_master_volume" );
	}


	public void SetResolution( int resolutionIndex )
	{
		Resolution newRes = resolutions[ resolutionIndex ];
		Screen.SetResolution( newRes.width, newRes.height, Screen.fullScreen );
	}

	public void SetQuality( int qualityIndex )
	{
		QualitySettings.SetQualityLevel( qualityIndex );
	}

	public void SetFullscreen( bool fullScreenMark )
	{
		Screen.fullScreen = fullScreenMark;
	}

	public void SetMasterVolume( float volume )
	{
		masterMixer.SetFloat( "volume", volume );
	}
}
