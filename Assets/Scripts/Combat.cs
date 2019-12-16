using UnityEngine;

public class Combat : MonoBehaviour
{
    public Projectile Projectile;
    public uint FireRateRPM;
    public int MaxHitPoints;
    [SerializeField]
    private int _currentHitPoints;
    [SerializeField]
    private float _pauseBetweenShots;
    [SerializeField]
    private float _shotTimer;

    public GameObject HealthBar;

    private void Start()
    {
        _pauseBetweenShots = 60f / FireRateRPM;
        _shotTimer = 0;

        _currentHitPoints = MaxHitPoints;
    }

    private void Update()
    {
        if (_shotTimer <= _pauseBetweenShots)
        {
            _shotTimer += Time.deltaTime;
        }
    }

    public void TakeHit(int damage)
    {
        ChangeHitPoints(-damage);
        if (_currentHitPoints == 0)
        {
            Destroy(gameObject);
        }
    }

    private void ChangeHitPoints(int delta)
    {
        _currentHitPoints = _currentHitPoints + delta;
        HealthBar.transform.localScale = new Vector3(_currentHitPoints * 1.0f / MaxHitPoints, 0.1f, 1);
    }

    public void Shoot(GameObject shooter)
    {
        if (_shotTimer >= _pauseBetweenShots)
        {
            Projectile projectile = Instantiate(Projectile);
            projectile.Shooter = shooter;
            _shotTimer = 0;
        }
    }
}
