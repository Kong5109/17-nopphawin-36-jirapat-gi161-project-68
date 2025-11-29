using UnityEngine;

public class BasicWarShip : Enemy
{
    
    public override void AttackPattern()
    {
        
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        
        Shoot();

        
        if (transform.position.y < -6f) 
        {
            Destroy(gameObject);
        }
    }
}