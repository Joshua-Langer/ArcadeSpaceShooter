using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using ArcadeShooter.Managers;

public class PlayerWeapon : MonoBehaviour
{
    [Header("Specs")]
    [SerializeField] float rateOfFire = 0.5f;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] GameObject bulletPrefab;

    [Header("Effects")]
    //Sound

    EntityManager entityManager;
    Entity bulletEntityPrefab;

    float shotTimer = 0f;
    bool isFireButtonDown = false;
    public bool IsFireButtonDown{get {return isFireButtonDown;} set {isFireButtonDown = value;}}

    protected virtual void Start()
    {
        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, null);
        bulletEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(bulletPrefab, settings);
    }

    public virtual void FireBullet()
    {
        Entity bullet = entityManager.Instantiate(bulletEntityPrefab);
        entityManager.SetComponentData(bullet, new Translation { Value = muzzleTransform.position});
        entityManager.SetComponentData(bullet, new Rotation { Value = muzzleTransform.rotation});
        //Play Sound
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
