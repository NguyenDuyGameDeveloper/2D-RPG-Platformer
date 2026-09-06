using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CharacterStats characterStats = collision.GetComponent<CharacterStats>();
        if (characterStats != null)
        {
            EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
            if (enemyStats != null)
            {
                enemyStats.killByPlayer = false;
                //Debug.Log("DeadZone: " + enemyStats.killByPlayer);
            }
            characterStats.KillEntity();
        }
        else
            Destroy(collision.gameObject);
    }
}
