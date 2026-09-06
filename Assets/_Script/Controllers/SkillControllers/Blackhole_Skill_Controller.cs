using System.Collections.Generic;
using UnityEngine;

public class Blackhole_Skill_Controller : MonoBehaviour
{
    #region Info
    [SerializeField] private GameObject hotKeyPrefab;
    [SerializeField] private List<KeyCode> keyCodeList;

    private float maxSize;
    private float growSpeed;
    private float shrinkSpeed;
    private float blackHoleTimer;

    private bool canGrow = true;
    private bool canShrink;
    private bool canCreateHotKeys = true;
    private bool cloneAttackRealesed;
    private bool playerCanDissapear = true;

    private int amountOfAttacks = 4;
    private float cloneAttackCooldown = .3f;
    private float cloneAttackTimer;

    private List<Transform> targets = new List<Transform>();
    private List<GameObject> createHotKey = new List<GameObject>();

    public bool playerCanExitState { get; private set; }
    #endregion
    private void Update()
    {
        TimeDeltaTime();
        FinishBlackHoleNoMatterWhat();
        CheckInput();
        CloneAttackLogic();
        CheckCanGrow_BlackHole();
        CheckCanShrink();
    }
    public void SetupBlackHole(float _maxSize, float _growSpeed, float _shrinkSpeed, int _amountOfAttacks, float _cloneAttackCooldown, float _blackHoleDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfAttacks = _amountOfAttacks;
        cloneAttackCooldown = _cloneAttackCooldown;
        blackHoleTimer = _blackHoleDuration;

        if (SkillManager.instance.clone.crystalInsteadOfCloneUnlocked)
            playerCanDissapear = false;
    }
    private void TimeDeltaTime()
    {
        cloneAttackTimer -= Time.deltaTime;
        blackHoleTimer -= Time.deltaTime;
    }
    private void FinishBlackHoleNoMatterWhat()
    {
        if (blackHoleTimer < 0)
        {
            blackHoleTimer = Mathf.Infinity;
            if (targets.Count > 0)
                RealeaseCloneAttack();
            else
                FinishBlackHoleSkill();
        }
    }
    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            RealeaseCloneAttack();
        }
    }
    private void CheckCanShrink()
    {
        if (canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(-1, -1), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
                Destroy(gameObject);
        }
    }
    private void CheckCanGrow_BlackHole()
    {
        if (canGrow && !canShrink)
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
    }
    private void RealeaseCloneAttack()
    {
        if (targets.Count <= 0) return;

        DestroyHotKey();
        cloneAttackRealesed = true;
        canCreateHotKeys = false;

        if (playerCanDissapear)
        {
            playerCanDissapear = false;
            PlayerManager.instance.player.playerFX.MakeTransparent(true);
        }
    }
    private void CloneAttackLogic()
    {
        if (cloneAttackTimer < 0 && cloneAttackRealesed && amountOfAttacks > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;
            int randomIndex = Random.Range(0, targets.Count);
            float xOffset;
            if (Random.Range(0, 100) > 50)
                xOffset = 1;
            else
                xOffset = -1;

            if (SkillManager.instance.clone.crystalInsteadOfCloneUnlocked)
            {
                SkillManager.instance.crystal.CreateCrystal();
                SkillManager.instance.crystal.CurrentCrystalChooseRandomEnemy();
            }
            else
            {
                SkillManager.instance.clone.CreateClone(targets[randomIndex], new Vector3(xOffset, 0));
            }
            amountOfAttacks--;

            if (amountOfAttacks <= 0)
            {
                Invoke(nameof(FinishBlackHoleSkill), 1f);
            }
        }
    }
    private void FinishBlackHoleSkill()
    {
        DestroyHotKey();
        playerCanExitState = true;
        canShrink = true;
        cloneAttackRealesed = false;
    }
    private void DestroyHotKey()
    {
        if (createHotKey.Count <= 0) return;

        for (byte i = 0; i < createHotKey.Count; i++)
        {
            Destroy(createHotKey[i]);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeTime(true);
            CreateHotKey(collision);

        }
    }
    private void OnTriggerExit2D(Collider2D collision) => collision.GetComponent<Enemy>()?.FreezeTime(false);
    private void CreateHotKey(Collider2D collision)
    {
        if (keyCodeList.Count <= 0)
        {
            Debug.Log("Not enought Hot Key in the List!");
            return;
        }
        if (!canCreateHotKeys) return;

        GameObject newHotKey = Instantiate(hotKeyPrefab, collision.transform.position + new Vector3(0, 2), Quaternion.identity); // Intance ra prefab Hotkey
        createHotKey.Add(newHotKey);

        KeyCode choosenKey = keyCodeList[Random.Range(0, keyCodeList.Count)]; // Choose a random HotKey in List HotKey
        keyCodeList.Remove(choosenKey); // Then remove it from List after add

        Blackhole_HotKey_Controller newHotKeyScript = newHotKey.GetComponent<Blackhole_HotKey_Controller>(); // Khoi tao Script

        newHotKeyScript.SetupHotKey(choosenKey, collision.transform, this);
    }
    public void AddEnemyToList(Transform _enemyTransform) => targets.Add(_enemyTransform);
}
