using UnityEngine;

public class Arrow_Controller : MonoBehaviour
{
    [SerializeField] private string targetPlayerName = "Player";
    [SerializeField] private int damage;
    [SerializeField] private float xVelocity;

    [SerializeField] private bool canMove;
    [SerializeField] private bool flipped;

    private CharacterStats myStats;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private int facingDir = 1;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        CheckCanMove();
        CheckArrowFacingDirection();
    }
    private void CheckArrowFacingDirection()
    {
        if (facingDir == 1 && rb.velocity.x < 0)
        {
            facingDir = -1;
            sr.flipX = true;
        }
    }
    private void CheckCanMove()
    {
        if (canMove)
            rb.velocity = new Vector2(xVelocity, rb.velocity.y);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(targetPlayerName))
        {
            //collision.GetComponent<CharacterStats>()?.TakeDamage(damage);
            myStats.DoDamage(collision.GetComponent<CharacterStats>());
            StuckInto(collision);
        }
        else if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            StuckInto(collision);
    }
    public void SetupArrow(float _speed, CharacterStats _myStats)
    {
        sr = GetComponent<SpriteRenderer>();
        xVelocity = _speed;
        myStats = _myStats;

    }
    private void StuckInto(Collider2D collision)
    {
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponent<CapsuleCollider2D>().enabled = false;
        canMove = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        transform.parent = collision.transform;

        Destroy(gameObject, Random.Range(4, 7));
    }

    public void FlipArrow()
    {
        if (flipped)
            return;

        xVelocity *= -1;
        flipped = true;
        transform.Rotate(0, 180, 0);
        targetPlayerName = "Enemy";
    }
}
