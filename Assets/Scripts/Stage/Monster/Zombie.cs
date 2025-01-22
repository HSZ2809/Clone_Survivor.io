using UnityEngine;
using UnityEngine.TextCore.Text;
using ZUN;

public class Zombie : Monster
{
    EXPObjPool EXPPool;

    [Header("EXP Type")]
    [SerializeField] private EXPShard.Type shardType;

    private void Start()
    {
        EXPPool = GameObject.FindGameObjectWithTag("Manager_Stage").GetComponent<EXPObjPool>();
    }

    private void Update()
    {
        Vector3 moveDirection = (character.position - transform.position).normalized;
        moveDirection *= moveSpeed;

        transform.Translate(moveDirection * Time.deltaTime);
    }

    public override void Hit(float damage)
    {
        hp -= damage;

        if (hp < 0)
        {
            EXPPool.SetShard(shardType, transform.position);
            gameObject.SetActive(false);
        }    
    }
}