using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissileStates
{
    ignition,
    homing
}

public class Missile : MonoBehaviour
{
    public Vector3 targetPos;
    public AudioSource audioSource;
    public AudioClip explosionClip;
    [SerializeField] private float force;
    [SerializeField] private float rotationForce;
    public float explosionRadius = 3f;
    public int damage = 5;
    public GameObject explosionVFX;
    private Rigidbody rb;
    [SerializeField] MissileStates state;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        state = MissileStates.ignition;
        audioSource = rb.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case MissileStates.ignition:
                IgnitionUpdate();
                break;
            case MissileStates.homing:
                HomingUpdate();
                break;
        }
    }

    void IgnitionUpdate()
    {
        if (transform.position.y > 3)
        {
            state = MissileStates.homing;
            return;
        }
        rb.velocity = transform.forward * force;
    }

    void HomingUpdate()
    {
        if (targetPos == Vector3.zero)
        {
            Destroy(gameObject);
            return;
        }
        Vector3 direction = targetPos - rb.position;
        direction.Normalize();
        Vector3 rotationAmount = Vector3.Cross(transform.forward, direction);
        rb.angularVelocity = rotationAmount * rotationForce;
        rb.velocity = transform.forward * force;
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explosion();
    }

    void Explosion()
    {
        audioSource.PlayOneShot(explosionClip);
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        if (colliders.Length > 0)
        {
            foreach (Collider col in colliders)
            {
                EnemyHealthManager ehm = col.GetComponent<EnemyHealthManager>();
                if (ehm != null)
                {
                    ehm.TakeDamage(damage);
                }
            }
        }
        Debug.Log("Explosion!");
        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
