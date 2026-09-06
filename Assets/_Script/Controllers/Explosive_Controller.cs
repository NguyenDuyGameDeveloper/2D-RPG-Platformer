using UnityEngine;

public class Explosive_Controller : MonoBehaviour
{
    #region Info
    private Animator anim;
    private CharacterStats myStats;
    private float growSpeed = 15;
    private float maxSize = 6;
    private float explosionRadius;

    private bool canGrow = true;
    #endregion
    private void Update()
    {
        if(canGrow)
            transform.localScale = Vector2.Lerp(transform.localScale,new Vector2(maxSize,maxSize),growSpeed * Time.deltaTime);

        if(maxSize - transform.localScale.x < .5f)
        {
            canGrow = false;
            anim.SetTrigger("Explode");
        }
    }
    public void SetupExplosive(CharacterStats _myStats, float _growSpeed, float _maxSize, float _explosionRadius)
    {
        anim = GetComponent<Animator>();
        myStats = _myStats;
        growSpeed = _growSpeed;
        maxSize = _maxSize;
        explosionRadius = _explosionRadius;
    }
    private void AnimationEplodeEvent()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (var hit in colliders)
        {
            if (hit.GetComponent<CharacterStats>() != null)
            {
                hit.GetComponent<Entity>().SetupKnockBackDirection(transform);

                myStats.DoDamage(hit.GetComponent<CharacterStats>());
            }
        }
    }
    private void SelfDestroy() => Destroy(gameObject);
}
