using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    #region Info
    protected Player player;
    protected SpriteRenderer sr;

    [Header("Pop Up Text")]
    [SerializeField] private GameObject popUpTextPrefab;
    [Space]

    [Header("FlashFX")]
    [SerializeField] float flashDuration;
    [SerializeField] private Material hitMat;
    private Material originalMat;

    [Header("Element color")]
    [SerializeField] private Color[] igniteColor;
    [SerializeField] private Color[] chillColor;
    [SerializeField] private Color[] shockColor;

    [Header("Element particles")]
    [SerializeField] private ParticleSystem igniteFX;
    [SerializeField] private ParticleSystem chillFX;
    [SerializeField] private ParticleSystem shockFX;

    [Header("HitFX")]
    [SerializeField] private GameObject hitFXNormal;
    [SerializeField] private GameObject hitFXCritical;

    private GameObject myHealthBar;
    
    #endregion
    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        player = PlayerManager.instance.player;
        originalMat = sr.material;
        myHealthBar = GetComponentInChildren<UI_HealthBar>()?.gameObject;
    }
    public void CreatePopUpText(string _text)
    {
        float randomX = Random.Range(-1, 1);
        float randomY = Random.Range(1.5f, 3);

        Vector3 positionOffSet = new Vector3(randomX, randomY, 0);

        GameObject newText = Instantiate(popUpTextPrefab, transform.position + positionOffSet, Quaternion.identity);

        newText.GetComponent<TextMeshPro>().text = _text;
    }
    public void MakeTransparent(bool _transparent)
    {
        if (_transparent)
        {
            myHealthBar.SetActive(false);
            sr.color = Color.clear;
        }
        else
        {
            myHealthBar.SetActive(true);
            sr.color = Color.white;
        }
    }
    private IEnumerator FlashFX()
    {
        sr.material = hitMat;
        Color currenColor = sr.color;
        sr.color = Color.white;

        yield return new WaitForSeconds(flashDuration);

        sr.color = currenColor;
        sr.material = originalMat;
    }
    public void RedColorBlink()
    {
        if (sr.color != Color.white)
            sr.color = Color.white;
        else
            sr.color = Color.red;
    }
    public void CancelColorChange()
    {
        CancelInvoke();
        sr.color = Color.white;

        igniteFX.Stop();
        chillFX.Stop();
        shockFX.Stop();
    }
    public void IgniteFxFor(float _second)
    {
        igniteFX.Play();

        InvokeRepeating(nameof(IgniteColorFx), 0, .3f);
        Invoke(nameof(CancelColorChange), _second);
    }
    public void ChillFxFor(float _second)
    {
        chillFX.Play();

        InvokeRepeating(nameof(ChillColorFx), 0, .3f);
        Invoke(nameof(CancelColorChange), _second);
    }
    public void ShockFxFor(float _second)
    {
        shockFX.Play();

        InvokeRepeating(nameof(ShockColorFx), 0, .3f);
        Invoke(nameof(CancelColorChange), _second);
    }
    private void IgniteColorFx()
    {
        if (sr.color != igniteColor[0])
            sr.color = igniteColor[0];
        else
            sr.color = igniteColor[1];
    }
    private void ChillColorFx()
    {
        if (sr.color != chillColor[0])
            sr.color = chillColor[0];
        else
            sr.color = chillColor[1];
    }
    private void ShockColorFx()
    {
        if (sr.color != shockColor[0])
            sr.color = shockColor[0];
        else
            sr.color = shockColor[1];
    }
    public void CreateHitFX(Transform _target, bool _critical)
    {
        float zRotation = Random.Range(-90, 90);
        float xPosition = Random.Range(-.5f, .5f);
        float yPosition = Random.Range(-.5f, .5f);

        Vector3 hitFxRotation = new Vector3(0, 0, zRotation);

        GameObject hitPrefab = hitFXNormal;

        if (_critical)
        {
            hitPrefab = hitFXCritical;

            float yRotation = 0;
            zRotation = Random.Range(-45, 45);

            if (GetComponent<Entity>().facingDir == -1)
                zRotation = 180;

            hitFxRotation = new Vector3(0, yRotation, zRotation);
        }

        GameObject newHitFX = Instantiate(hitPrefab, _target.position + new Vector3(xPosition, yPosition),
            Quaternion.identity); //,_target);

        newHitFX.transform.Rotate(hitFxRotation);

        Destroy(newHitFX, 0.5f);
    }
}
