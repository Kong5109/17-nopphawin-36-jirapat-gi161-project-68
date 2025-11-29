using UnityEngine;

public class BasicWarShip : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float _stopYPosition = 3.0f; 
    [SerializeField] private float _swaySpeed = 2.0f;     
    [SerializeField] private float _swayWidth = 3.0f;     

    protected override void Start()
    {
        base.Start();
        _shootInterval = 2.0f;
    }

    public override void AttackPattern()
    {
        if (transform.position.y > _stopYPosition)
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

          
            float xToCenter = Mathf.MoveTowards(transform.position.x, 0f, _moveSpeed * Time.deltaTime);
            transform.position = new Vector3(xToCenter, transform.position.y, 0);
        }
        else
        {
            
            
            float newX = 0f + Mathf.Sin(Time.time * _swaySpeed) * _swayWidth;

            transform.position = new Vector3(newX, transform.position.y, 0);
        }

        Shoot();
    }
}