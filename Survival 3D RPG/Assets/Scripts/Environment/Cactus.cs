using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cactus : MonoBehaviour
{
    public int damage;
    public float damageRate;

    private List<IDamageable> damageables = new List<IDamageable>();

    public void Start()
    {
        StartCoroutine(InflictDamage());
    }

    IEnumerator InflictDamage()
    {
        while (true)
        {
            for (int i = 0; i < damageables.Count; i++)
            {
                damageables[i].TakeDamage(damage);
            }

            yield return new WaitForSeconds(damageRate);
        }
    }

    //adding elements with IDamageable interface when they collide with a cactus
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            damageables.Add(collision.gameObject.GetComponent<IDamageable>());
        }
    }

    //removing elements with IDamageable interface when they stop colliding with a cactus
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.GetComponent<IDamageable>() != null)
        {
            damageables.Remove(collision.gameObject.GetComponent<IDamageable>());
        }
    }
}
