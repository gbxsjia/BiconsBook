using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField] Slider MasterVolumeSlider;
    [SerializeField] Slider BGMVolumeSlider;
    [SerializeField] Slider SoundEffectVolumeSlider;
    [SerializeField] AudioMixer audioMixer;

    [Header("BGM")]
    float BGMVolume = 0.35f;

    [SerializeField] List<AudioClip> OpenWorldBgmList;
    [SerializeField] List<AudioClip> BattleBgmList;

    [SerializeField] AudioSource BgmSource;

    [SerializeField] int OpenWorldBgmID;
    [SerializeField] int BattleBgmID;

    [Header("Environment")]
    [SerializeField] AudioSource Environment1;

    [SerializeField] List<AudioClip> VillageEnvironmentClip;
    [SerializeField] List<AudioClip> WindEnvironmentClip;

    [Header("SoundEffect")]

    [SerializeField] AudioSource Equip;
    [SerializeField] AudioSource OpenBag;
    [SerializeField] AudioSource OpenBook;
    [SerializeField] AudioSource BattleStart;
    [SerializeField] AudioSource FlyWind;
    [SerializeField] AudioSource InitialFlyWind;
    [SerializeField] AudioSource Repair;
    [SerializeField] AudioSource AbilityUnblock;

    [Header("BattleSoundEffect")]
    [SerializeField] AudioSource CardSet;
    [SerializeField] AudioSource Swing;
    [SerializeField] AudioSource Hit;
    [SerializeField] AudioSource TurnChange;

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        InGameManager.instance.BattleStartEvent += PlayBattleBgm;
        InGameManager.instance.ReturnToCampEvent += PlayBigWorldBgm;
    }
    public void PlayBigWorldBgm()
    {
        MediumBGM();
        if(OpenWorldBgmID == OpenWorldBgmList.Count)
        {
            OpenWorldBgmID = 0;
        }
        BgmSource.clip = OpenWorldBgmList[OpenWorldBgmID];
        BgmSource.Play();
        PlayWind();
        PlayEnvironment(true);
    }

    public void SetBGMVolume()
    {
        audioMixer.SetFloat("BGMVolume", BGMVolumeSlider.value);
    }
    public void SetSoundEffectVolume()
    {
        audioMixer.SetFloat("SoundEffectVolume", SoundEffectVolumeSlider.value);
    }
    public void SetMasterVolume()
    {
        audioMixer.SetFloat("MasterVolume", MasterVolumeSlider.value);
    }
    public void PlayBagSound()
    {
        OpenBag.Play();
    }
    public void PlayEquipSound()
    {
        Equip.Play();
    }
    public void PlayBookSound()
    {
        OpenBook.Play();
    }

    public void PlayAbilityUnblockSound()
    {
        AbilityUnblock.Play();
    }

    public void HighBGM()
    {
        BGMVolume = 0.6f;
    }

    public void MediumBGM()
    {
        BGMVolume = 0.35f;
    }

    public void LowBGM()
    {
        BGMVolume = 0.01f;
    }
    private void Update()
    {
        BgmSource.volume = Mathf.Lerp(BgmSource.volume,BGMVolume,0.02f);
    }
    public void PlayRepairSound()
    {
        Repair.Play();
    }
    public void PlayFlyWindSound(float Speed)
    {
        FlyWind.pitch = Speed;
        FlyWind.Play();
    }

    public void PlayInitialFlyWindSound()
    {
        InitialFlyWind.Play();
    }

    public void PlayInVillage()
    {
        switch(UIManager.instance.timeType)
        {
            case UIManager.TimeType.夜晚:
                Environment1.clip = VillageEnvironmentClip[1];
                break;
            default:
                Environment1.clip = VillageEnvironmentClip[0];
                break;
        }
        Environment1.Play();
    }

    public void PlayWind()
    {
        Environment1.clip = WindEnvironmentClip[0]; Environment1.Play();
    }
    public void PlayEnvironment(bool play)
    {
        if (play == false)
        {
            Environment1.Stop();
        }
        else
        {
            Environment1.Play();
        }
    }
    public void PlayAttackSound(bool HitTarget,AudioClip cast,AudioClip hit)
    {
        Swing.clip = cast;
        Hit.clip = hit;
        StartCoroutine(PlayAttackSoundProcess(HitTarget));
    }

    IEnumerator PlayAttackSoundProcess( bool HitTarget)
    {
        Swing.Play();
        yield return new WaitForSeconds(0.1f);
        if (HitTarget)
        {
            Hit.Play();
        }
       ;
    }
    public void PlayTurnChangeSound()
    {
        TurnChange.Play();
    }

    public void PlayOpenCardSetSound()
    {
        CardSet.Play();
    }
    public void PlayBattleStartSound()
    {
        BattleStart.Play();
    }
    public void PlayBattleBgm()
    {
        HighBGM();
        if (BattleBgmID == BattleBgmList.Count)
        {
            BattleBgmID = 0;
        }
        BgmSource.clip = BattleBgmList[BattleBgmID];
        BgmSource.Play();
        PlayEnvironment(false);
    }
}
