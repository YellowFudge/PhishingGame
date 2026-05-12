using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class MusicControlSlider : MonoBehaviour
{
    [SerializeField] TMP_Text percentageText;
    Slider _slider;
    AudioSource _audioSource;
    bool _beenSetUp = false;
    bool _inStart;

    private void OnEnable()
    {
        if (!_beenSetUp)
        {
            _beenSetUp = true;
            _slider = GetComponent<Slider>();
            _slider.wholeNumbers = false;
            _slider.maxValue = 1;
            _slider.minValue = 0;
        }
        _slider.onValueChanged.AddListener(delegate { NewMusicVolume(); });
    }
    private void OnDisable()
    {
        _slider.onValueChanged.RemoveListener(delegate { NewMusicVolume(); });
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _inStart = true;
        _audioSource = SaveObjectBetweenScenesScript.objectToSaveInstance.GetComponent<AudioSource>();
        _slider.value = _audioSource.volume;
        _inStart = false;
    }

    void NewMusicVolume()
    {
        if (!_inStart){
            _audioSource.volume = _slider.value;
        }
        percentageText.text = $"Volume: {Mathf.Round(_audioSource.volume * 100)}%";
    }


}
