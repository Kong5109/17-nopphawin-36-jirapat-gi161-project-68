using UnityEngine;

public class FastScout : Enemy
{
    [Header("Circular Movement Settings")]
    [SerializeField] private Vector3 _centerPosition = new Vector3(0, 2, 0); 
    [SerializeField] private float _radius = 3.0f;        
    [SerializeField] private float _rotationSpeed = 3.0f; 

    private float _angle; 

    protected override void Start()
    {
        base.Start();
        _shootInterval = 0.5f; 
        
        _angle = 0;
    }

    public override void AttackPattern()
    {
        _angle += _rotationSpeed * Time.deltaTime;

      
        float x = _centerPosition.x + Mathf.Cos(_angle) * _radius;
        float y = _centerPosition.y + Mathf.Sin(_angle) * _radius;

        transform.position = new Vector3(x, y, 0);

        Shoot();
    }
}