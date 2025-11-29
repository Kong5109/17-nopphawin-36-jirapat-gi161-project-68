using UnityEngine;

public class BasicWarShip : Enemy
{
    [Header("Movement Settings")]
    [SerializeField] private float _stopYPosition = 3.0f;
    [SerializeField] private float _swaySpeed = 2.0f;
    [SerializeField] private float _swayWidth = 2.5f;

    [Header("Screen Bounds")]
    [SerializeField] private float _minX = -2.25f; // ขอบซ้ายสุด (ลองลากยานไปวัดดู)
    [SerializeField] private float _maxX = 2.25f;  // ขอบขวาสุด

    private float _startX;

    protected override void Start()
    {
        base.Start();
        _startX = transform.position.x;
    }

    public override void AttackPattern()
    {
        // 1. บินลงมา
        if (transform.position.y > _stopYPosition)
        {
            transform.Translate(Vector3.down * _moveSpeed * Time.deltaTime);
        }
        else
        {
            // 2. คำนวณจุดที่จะส่ายไป
            float rawX = _startX + Mathf.Sin(Time.time * _swaySpeed) * _swayWidth;

            // 3. (สำคัญ!) ล็อกค่าไม่ให้ออกนอกขอบเขต Min/Max
            float clampedX = Mathf.Clamp(rawX, _minX, _maxX);

            transform.position = new Vector3(clampedX, transform.position.y, 0);
        }

        Shoot();
    }
}