using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Text volumeText;
    

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("Volume", 1f);
        volumeSlider.value = savedVolume;
        UpdateVolumeSlider(savedVolume);
        
        volumeSlider.onValueChanged.AddListener(OnVolumeSliderChanged);
    }

    private void OnVolumeSliderChanged(float volume)
    {
        UpdateVolumeSlider(volume);
        SaveVolumeSlider(volume);
    }

    private void UpdateVolumeSlider(float volume)
    {
        AudioListener.volume = volume;
        volumeText.text = Mathf.RoundToInt(volume * 100) + "%";
    }

    private void SaveVolumeSlider(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
        PlayerPrefs.Save();
    }
}
