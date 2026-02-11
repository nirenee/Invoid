using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
  Health health;
    public float damageReceived;
    private int enemycounter;
    private Coroutine damagecoroutine;
    
  
    private void Awake()
    {
        health = GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("deathwall"))
        {
            //Instead of calling scene manager from player health, I would much more prefer that you send some type of signal to the game where lot's of
            //components can react to it. This way player health does not need to manage scenes, meaning that you can have classes of single responsibility
            //which makes code easy to understand and refactor

            /*
            //AI says:
            [CreateAssetMenu(menuName = "Signals/DeathSignal")]
            public class DeathSignal : ScriptableObject
            {
                private event Action<> _listeners;

                public void Raise() => _listeners?.Invoke(value);
                public void Register(Action<> listener)   => _listeners += listener;
                public void Unregister(Action<> listener) => _listeners -= listener;
            }

            //use example:
            public class PlayerHealth : MonoBehaviour
            {
                [SerializeField] private DeathSignal deathSignal;
                public void Damage(int amount)
                {
                    //...
                    if(health == 0)
                    {
                        deathSignal.Raise();
                    }
                }
            }

            public class MySceneManager : MonoBehaviour
            {
                [SerializeField] private DeathSignal deathSignal;

                void OnEnable()  => deathSignal.Register(OnDied);
                void OnDisable() => deathSignal.Unregister(OnDied);

                void OnDied() {  } // react
            }
            */

            Destroy(this.gameObject);
            SceneManager.LoadScene("GameOver");

        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            //Is this spelled correctly?
            Debug.Log("colliison");
            enemycounter++;
            damagecoroutine = StartCoroutine(DamageOverTime());
            

        }
      
     

    }
    private void OnCollisionExit(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Enemy"))
        {
            return;
        }
        Debug.Log("no colliison");
        enemycounter = Mathf.Max(0, enemycounter - 1);
        if(enemycounter == 0)
        {
            StopCoroutine(DamageOverTime());
            damagecoroutine = null;
        }
        
    }

    private IEnumerator DamageOverTime()
    {
        //Isn't wait always 0.5f here? Did you want to use random between 0.01 and 0.05 instead of max?
        WaitForSeconds wait = new WaitForSeconds(Mathf.Max(0.01f, 0.5f));
    
        while(enemycounter > 0 && health.currenthealth > 0)
        {
            health.ApplyDamage(damageReceived);
            yield return wait;
        }
        if (health.currenthealth <= 0)
        {
            Destroy(this.gameObject);
            SceneManager.LoadScene("GameOver");
        }
        damagecoroutine = null;
     }
   
}
