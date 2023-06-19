using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public static SoundManager Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<SoundManager>();
            }
            return instance;
        }
    }

    [SerializeField]
    private AudioSource bgmSource;

    [SerializeField]
    private AudioSource sfxSource;

    [SerializeField]
    private float masterVolumeSFX = 1f;  // sfx master volume
    [SerializeField]
    private float masterVolumeBGM = 1f;  // bgm master volume

    [SerializeField]
    private AudioClip[] bgmAudioClips;  // BGM sounds
    [SerializeField]
    private AudioClip[] sfxAudioClips;  // Effect sounds
    [SerializeField]
    private Slider bgmSlider;
    [SerializeField]
    private Slider sfxSlider;

    Dictionary<string, AudioClip> sfxAudioClipsDic = new Dictionary<string, AudioClip>();
    Dictionary<string, AudioClip> bgmAudioClipsDic = new Dictionary<string, AudioClip>();

    private void Awake() {
        if(Instance != this)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);

        bgmSource = GameObject.Find("BGMSource").GetComponent<AudioSource>();
        sfxSource = GameObject.Find("SFXSource").GetComponent<AudioSource>();
        //bgmSlider = GameObject.Find("bgmSlider").GetComponent<Slider>();
        //sfxSlider = GameObject.Find("sfxSlider").GetComponent<Slider>();

        foreach(AudioClip audioclip in sfxAudioClips)
        {
            sfxAudioClipsDic.Add(audioclip.name, audioclip);
        }

        foreach(AudioClip audioclip in bgmAudioClips)
        {
            bgmAudioClipsDic.Add(audioclip.name, audioclip);
        }

        bgmSlider.onValueChanged.AddListener(bgmVolume);
        sfxSlider.onValueChanged.AddListener(sfxVolume);
    }

    private void Start() 
    {
        PlayBGMSound("MainScene");
    }

    private void bgmVolume(float volume)
    {
        masterVolumeBGM = volume;
        bgmSource.volume = masterVolumeBGM;
    }
    private void sfxVolume(float volume)
    {
        masterVolumeSFX = volume;

        if(!sfxSource.isPlaying)
            PlaySFXSound("GetDamage",masterVolumeSFX);//소리 바꿀 것
    }


    //  play sfx sound
    public void PlaySFXSound(string name, float volume = 1f)
    {
        if(sfxAudioClipsDic.ContainsKey(name) == false)
        {
            Debug.Log(name + " is not Contained audioClipsDic");
            return;
        }
        sfxSource.PlayOneShot(sfxAudioClipsDic[name], volume * masterVolumeSFX);
    }


    //  play bgm sound
    public void PlayBGMSound(string bgmName, float volume = 1f)
    {
        bgmSource.loop = true;
        if(bgmName=="GameOver")
        {
            bgmSource.loop = false;
        }
        
        bgmSource.volume = volume * masterVolumeBGM;

        Debug.Log("bgmName = "+bgmName);

         if(bgmAudioClipsDic.ContainsKey(bgmName) == false)
        {
            Debug.Log(bgmName + " is not Contained audioClipsDic");
            return;
        }

        bgmSource.clip = bgmAudioClipsDic[bgmName];
        bgmSource.Play();

    }
}
