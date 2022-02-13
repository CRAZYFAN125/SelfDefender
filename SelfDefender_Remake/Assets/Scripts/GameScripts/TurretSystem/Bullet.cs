using UnityEngine;


public class Bullet : MonoBehaviour
{
    public enum BulletType
    {
        Normal,
        Rocket
    }
    public BulletType bulletType;
    private Transform target;

    public float speed = 10f;
    public bool isRotating = false;
    private float dmAmount;

    public GameObject HitParticle;
    [HideInInspector] public float boomRadius = 3f;

    public void Init(Transform _target, float _dmAmount)
    {
        target = _target;
        dmAmount = _dmAmount;
    }


    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 lookDir = target.position - transform.position;

        float distanceThisFrame = speed * Time.deltaTime;
        if (isRotating)
        {
            float angleZ = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angleZ);
        }
        if (lookDir.magnitude <= distanceThisFrame)
        {
            HitTarget();
            return;
        }
        transform.Translate(lookDir.normalized * distanceThisFrame, Space.World);
    }
    void HitTarget()
    {
        Instantiate(HitParticle, transform.position, Quaternion.identity);
        switch (bulletType)
        {
            case BulletType.Normal:
                Enemy en = target.GetComponent<Enemy>();
                if (en != null)
                {
                    en.Hit(dmAmount);
                }

                break;
            case BulletType.Rocket:
                Explode();
                break;
            default:
                break;
        }
        //if (bulletType==BulletType.Normal)
        //{
        //    Enemy en = target.GetComponent<Enemy>();
        //    if (en != null)
        //    {
        //        en.Hit(dmAmount);
        //    }
        //}
        //else if (bulletType==BulletType.Rocket)
        //{
        //    Explode();
        //}
        Destroy(gameObject);
    }
    void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, boomRadius);
        foreach (Collider item in colliders)
        {
            if (item.gameObject.tag == "Enemy")
            {
                //if (item.transform == target)
                //{
                item.GetComponent<Enemy>().Hit(dmAmount);
                //}
                //else
                //{
                //item.GetComponent<Enemy>().Hit(dmAmount / 2);
                //}
            }
        }
    }
}
