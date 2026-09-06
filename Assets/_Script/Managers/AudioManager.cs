using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Info
    public static AudioManager instance;
    [SerializeField] private float sfxMinimumDistance;
    [SerializeField] private AudioSource[] sfx;
    [SerializeField] private AudioSource[] bgm;

    public bool playBgm;
    private int bgmIndex;

    private bool canPlaySFX;
    #endregion

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Another instance of AudioManager found and destroyed.");
            Destroy(gameObject);
            return;
        }
        else
            instance = this;
        Invoke(nameof(AllowSFX), 1f);
        //DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        if (!playBgm)
        {
            StopAllBGM();
        }
        else
        {
            if (!bgm[bgmIndex].isPlaying)
            {
                PlayRandomBGM();
            }
        }
    }
    public void PlaySFX(int _sfxindex, Transform _source)
    {
        if (PlayerManager.instance == null || PlayerManager.instance.player == null)
        {
            Debug.LogError("PlayerManager instance is null!");
            return;
        }

        if (canPlaySFX == false) return;

        if (_source != null && Vector2.Distance(PlayerManager.instance.player.transform.position, _source.position) > sfxMinimumDistance)
            return;

        if (_sfxindex < sfx.Length)
        {
            sfx[_sfxindex].pitch = Random.Range(.7f, 1.15f);
            sfx[_sfxindex].Play();
        }
    }
    public void StopSFX(int _index) => sfx[_index].Stop();
    public void StopSFXOverTime(int _index) => StartCoroutine(DecreaseVolumeCoroutine(sfx[_index]));
    public void PlayRandomBGM()
    {
        int newBGMIndex = Random.Range(0, bgm.Length);
        PlayBGM(newBGMIndex);
    }
    public void PlayBGM(int _bgmindex)
    {
        if (_bgmindex < 0 || _bgmindex >= bgm.Length)
        {
            Debug.LogError("BGM index out of range: " + _bgmindex);
            return;
        }

        bgmIndex = _bgmindex;
        StopAllBGM();
        bgm[bgmIndex].Play();

    }
    public void StopAllBGM()
    {
        for (int i = 0; i < bgm.Length; i++)
        {
            bgm[i].Stop();
        }
    }
    private void AllowSFX() => canPlaySFX = true;
    private IEnumerator DecreaseVolumeCoroutine(AudioSource _audio)
    {
        float defaultVolume = _audio.volume;

        while (_audio.volume > .1f)
        {
            _audio.volume -= _audio.volume * .2f;
            yield return new WaitForSeconds(.4f);

            if (_audio.volume <= .1f)
            {
                _audio.Stop();
                _audio.volume = defaultVolume;
                break;
            }
        }
    }
}
