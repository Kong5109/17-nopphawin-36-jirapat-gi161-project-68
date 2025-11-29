using UnityEngine;

public class FastScout : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float _waveFrequency = 1f;
    [SerializeField] private float _waveMagnitude = 3f;

    [Header("Screen Bounds")]
    [SerializeField] private float _minX = -8f; 
    [SerializeField] private float _maxX = 8f;  

    private float _startX;

    protected override void Start()
    {
        base.Start();
        _startX = transform.position.x;
        _shootInterval = 1.0f; 
    }

    public override void AttackPattern()
    {
        transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);

        float rawX = _startX + Mathf.Sin(Time.time * _waveFrequency) * _waveMagnitude;

        float clampedX = Mathf.Clamp(rawX, _minX, _maxX);

        transform.position = new Vector3(clampedX, transform.position.y, 0);

        Shoot();

        if (transform.position.y < -7f)
        {
            Destroy(gameObject);
        }
    }
}