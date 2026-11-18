using UnityEngine;
using UnityEngine.UI;

// Слайдер громкости в меню. Повесь на Slider, в OnValueChanged выбери VolumeSlider.OnChanged
public class VolumeSlider : MonoBehaviour
{
    public Slider slider;

    void Start()
    {
        slider.value = SaveManager.instance.data.musicVol;
    }

    public void OnChanged(float value)
    {
        SaveManager.instance.SetMusicVolume(value);
    }
}
