using UnityEngine;

public class Combat : MonoBehaviour
{
    public Projectile Projectile;
    public uint FireRateRPM;
    public int MaxHitPoints;
    public int CurrentHitPoints;
    [SerializeField]
    private float _pauseBetweenShots;
    [SerializeField]
    private float _shotTimer;

    public GameObject HealthBar;

    public CombatManager CombatManager;

    private void Start()
    {
        _pauseBetweenShots = 60f / FireRateRPM;
        _shotTimer = 0;

        CurrentHitPoints = MaxHitPoints;
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
        if (CurrentHitPoints == 0)
        {
            CombatManager.Kill(gameObject);
        }
    }

    private void ChangeHitPoints(int delta)
    {
        CurrentHitPoints = CurrentHitPoints + delta;
        HealthBar.transform.localScale = new Vector3(CurrentHitPoints * 1.0f / MaxHitPoints, 0.1f, 1);
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
