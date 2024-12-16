using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingScript : MonoBehaviour
{
    private GameObject content;
    private Button saveButton;
    private Button closeButton;
    private Button defaultButton;


    private Slider effectsVolumeSlider;
    private float defaultEffectsVolume = 0.5f;

    private Slider ambientVolumeSlider;
    private float defaultAmbientVolume = 0.5f;


    private Toggle muteAllToggle;
    private bool defaultMuteAll = false;

    private Slider sensXSlider;
    private float defaultSensX = 0.5f;

    private Slider sensYSlider;
    private float defaultSensY = 0.5f;

    private TMPro.TMP_Dropdown difficultyDropDown;
    private int defaultDifficulty;


    void Start()
    {
        Transform contentTransform = transform.Find("Content");
        content = contentTransform.gameObject;
        effectsVolumeSlider = contentTransform.Find("SoundSettings/EffectsSlider").GetComponent<Slider>();
        defaultEffectsVolume = effectsVolumeSlider.value;

        ambientVolumeSlider = contentTransform.Find("SoundSettings/AmbientSlider").GetComponent<Slider>();
        defaultAmbientVolume = ambientVolumeSlider.value;

        muteAllToggle = contentTransform.Find("SoundSettings/MuteAllToggle").GetComponent<Toggle>();
        defaultMuteAll = muteAllToggle.isOn;

        sensXSlider = contentTransform.Find("MouseSpeedSettings/SensXSlider").GetComponent<Slider>();
        defaultSensX = sensXSlider.value;
        sensYSlider = contentTransform.Find("MouseSpeedSettings/SensYSlider").GetComponent<Slider>();
        defaultSensY = sensYSlider.value;

        difficultyDropDown = contentTransform.Find("DifficultySettings/Dropdown").GetComponent<TMPro.TMP_Dropdown>();
        defaultDifficulty = difficultyDropDown.value;


        saveButton = contentTransform.Find("SaveButton").GetComponent<Button>();
        closeButton = contentTransform.Find("CloseButton").GetComponent<Button>();
        defaultButton = contentTransform.Find("DefaultButton").GetComponent<Button>();


        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume)))
        {
            GameState.ambientVolume = PlayerPrefs.GetFloat(nameof(GameState.ambientVolume));
            ambientVolumeSlider.value = GameState.ambientVolume;
        }
        else
        {
            GameState.ambientVolume = ambientVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.effectsVolume)))
        {
            GameState.effectsVolume = PlayerPrefs.GetFloat(nameof(GameState.effectsVolume));
            effectsVolumeSlider.value = GameState.effectsVolume;

        }
        else
        {
            GameState.effectsVolume = effectsVolumeSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.isMuted)))
        {
            GameState.isMuted = PlayerPrefs.GetInt(nameof(GameState.isMuted)) == 1 ? true : false;
            muteAllToggle.isOn = GameState.isMuted;

        }
        else
        {
            GameState.isMuted = muteAllToggle.isOn;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookX)))
        {
            GameState.sensitivityLookX = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookX));
            sensXSlider.value = GameState.sensitivityLookX;
        }
        else
        {
            GameState.sensitivityLookX = sensXSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.sensitivityLookY)))
        {
            GameState.sensitivityLookY = PlayerPrefs.GetFloat(nameof(GameState.sensitivityLookY));
            sensYSlider.value = GameState.sensitivityLookY;
        }
        else
        {
            GameState.sensitivityLookY = sensYSlider.value;
        }

        if (PlayerPrefs.HasKey(nameof(GameState.ambientVolume)))
        {
            GameState.difficulty = (GameState.GameDifficulty)PlayerPrefs.GetInt(nameof(GameState.difficulty));
            difficultyDropDown.value = (int)GameState.difficulty;
        }
        else
        {
            GameState.difficulty = (GameState.GameDifficulty)difficultyDropDown.value;
        }

        Time.timeScale = content.activeInHierarchy ? 0.0f : 1.0f;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            OnCloseButtonClick();
        }
    }

    public void OnSaveButtonClick()
    {
        PlayerPrefs.SetFloat(nameof(GameState.ambientVolume), GameState.ambientVolume);
        PlayerPrefs.SetFloat(nameof(GameState.effectsVolume), GameState.effectsVolume);
        PlayerPrefs.SetInt(nameof(GameState.isMuted), GameState.isMuted ? 1 : 0);

        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookX), GameState.sensitivityLookX);
        PlayerPrefs.SetFloat(nameof(GameState.sensitivityLookY), GameState.sensitivityLookY);

        PlayerPrefs.SetInt(nameof(GameState.difficulty), (int)GameState.difficulty);
        PlayerPrefs.Save();
    }

    public void OnDefaultButtonClick()
    {
        GameState.sensitivityLookX = defaultSensX;
        sensXSlider.value = defaultSensX;
        GameState.sensitivityLookY = defaultSensY;
        sensYSlider.value = defaultSensY;
        GameState.ambientVolume = defaultAmbientVolume;
        ambientVolumeSlider.value = defaultAmbientVolume;
        GameState.effectsVolume = defaultEffectsVolume;
        effectsVolumeSlider.value = defaultEffectsVolume;
        GameState.isMuted = defaultMuteAll;
        muteAllToggle.isOn = defaultMuteAll;

        GameState.difficulty = (GameState.GameDifficulty)defaultDifficulty;
        difficultyDropDown.value = defaultDifficulty;
        OnSaveButtonClick();
    }

    public void OnCloseButtonClick()
    {
        Time.timeScale = content.activeInHierarchy ? 1.0f : 0.0f;
        content.SetActive(!content.activeInHierarchy);
    }

    public void OnSensXChanged(Single value) => GameState.sensitivityLookX = value;
    public void OnSensYChanged(Single value) => GameState.sensitivityLookY = value;


    public void OnEffectsVolumeChanged(Single value) => GameState.effectsVolume = value;
    public void OnAmbientVolumeChanged(Single value) => GameState.ambientVolume = value;
    public void OnMuteAllChanged(bool value) => GameState.isMuted = value;

    public void OnDifficultyChanged(int selectedIndex)

    {
        GameState.difficulty = (GameState.GameDifficulty)selectedIndex;
        Debug.Log(selectedIndex);
    }

}