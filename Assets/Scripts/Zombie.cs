using UnityEngine;
using UnityEngine.TextCore.Text;
using ZUN;

public class Zombie : Monster
{
    private void Update()
    {
        Vector3 moveDirection = (character.position - transform.position).normalized;
        moveDirection *= moveSpeed;

        transform.Translate(moveDirection * Time.deltaTime);
    }
}