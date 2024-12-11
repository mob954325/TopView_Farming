using UnityEngine;

public class Factory_Enemy : Factory<EnemyBase>
{
    public EnemyBase SpawnEnemy(Vector3 pos, Quaternion rot)
    {
        EnemyBase enemy = SpawnProduct(pos, rot);

        return enemy;
    }
}