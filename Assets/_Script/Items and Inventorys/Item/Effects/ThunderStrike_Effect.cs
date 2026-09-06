using UnityEngine;

[CreateAssetMenu(fileName = "Thunder Strike Effect", menuName = "Data/Item Effect/Thunder Strike Effect")]
public class ThunderStrike_Effect : ItemEffect
{
    [SerializeField] private GameObject thunderStrikePrefab;


    public override void ExecuteEffect(Transform _enemyPosition)
    {
        GameObject newThunderStrike = Instantiate(thunderStrikePrefab, _enemyPosition.position, Quaternion.identity);
        Destroy(newThunderStrike, .8f);
    }
}
