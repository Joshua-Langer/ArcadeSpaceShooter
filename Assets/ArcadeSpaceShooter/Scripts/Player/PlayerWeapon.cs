using ArcadeShooter.Managers;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Specs")]
    [SerializeField] float rateOfFire = 0.5f;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] GameObject bulletPrefab;

    [Header("Effects")]
    //Sound


    float shotTimer = 0f;
    bool isFireButtonDown = false;
    public bool IsFireButtonDown{get {return isFireButtonDown;} set {isFireButtonDown = value;}}

    protected virtual void Start()
    {

    }

    public virtual void FireBullet()
    {
        Instantiate(bulletPrefab, muzzleTransform.position, Quaternion.Euler(90f, 0f, 0f));
    }

    protected virtual void Update()
    {
        if(GameManager.IsGameOver())
        {
            return;
        }

        shotTimer += Time.deltaTime;
        if(shotTimer >= rateOfFire && isFireButtonDown)
        {
            FireBullet();
            shotTimer = 0f;
        }
    }
}
