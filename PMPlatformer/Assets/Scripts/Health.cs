using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class Health : MonoBehaviour
{
    //store the players health
    public float health = 10;
    float maxHealth;
    //we need a reference to out healthBar game object
    AudioSource audiosource;
    public Image healthBar;
    public AudioClip hitsound;
    //if we collide with something tagged as Enemy, take damage
    //if health gets too low, we die (reload the level)
    //if we collide with something tagged as health pack, increase health
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (audiosource != null && hitsound != null)
            {
                audiosource.PlayOneShot(hitsound);
            }
            health--;
            healthBar.fillAmount = health / maxHealth;
            //if our health gets too low, we should die (reload level)
            if (health <= 0)
            {
                //reload the level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
        // if we collide with the health pack collectable
        if (collision.gameObject.tag == "HealthPack")
        {
            //increase the health value
            health++;
            healthBar.fillAmount = health / maxHealth;
            Destroy(collision.gameObject);
            //if our health is trying to exceed our max health
            if (health > maxHealth)
            {
                //cap out health at max health
                health = maxHealth;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            Destroy(collision.gameObject);
            health--;
            healthBar.fillAmount = health / maxHealth;
            //if our health gets too low, we should die (reload level)
            if (health <= 0)
            {
                //reload the level
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxHealth = health;
        healthBar.fillAmount = health / maxHealth;
        audiosource = Camera.main.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
