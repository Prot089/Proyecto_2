using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class habilidades_jugador : MonoBehaviour
{

    public float hp;
    public float max_hp;

    [Header("mp")]
    public float max_mp;
    public float mp;
    public float mp_a_recuperar_over_time;
    public float mp_a_recuperar_arma;
    public float mp_a_recuperar_escudo;

    [Header("weapon")]
    public GameObject weapon;
    public float weapon_time = 0.5f;
    public float cooldown_weapon;
    public bool puede_usar_weapon;
    public bool atacando;

    [Header("shield")]
    public GameObject shield;
    public float shield_time = 0.5f;
    public float cooldown_shield;
    public bool puede_usar_shield;
    public bool usando_escudo = false;

    [Header("invencibilidad")]
    public bool invencible;
    public float tiempo_invencible;

    [Header("over time")]
    public float mp_loss_over_time;
    public float hp_loss_over_time;

    [Header("melee atack")]
    public float mp_loss_melee_atack;
    public float hp_loss_melee_atack;

    [Header("enemy damage")]
    public float mp_loss_enemy_damage;
    public float hp_loss_enemy_damage;
    private Rigidbody2D rigidbody;
    public bool recibiendo_danho = false;
    public bool muerto = false;




    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        muerto = false;
        recibiendo_danho = false;
        mp = max_mp;
        hp = max_hp;
    }

    public void Over()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        if (muerto) return;

        if (hp <= 0)
        {

            muerto = true;
            GetComponent<AudioSource>().PlayOneShot(jugador_derrotado);
            Invoke("Over", 0.5f);
        }
    }

    void Desactivar_arma()
    {
        weapon.SetActive(false);
        Invoke("End_cooldown_arma", cooldown_weapon);
    }

    void Desactivar_escudo()
    {
        shield.SetActive(false);
        usando_escudo = false;
        Invoke("End_cooldown_escudo", cooldown_shield);
    }

    public void Recuperar_Barra_demonio(float recuperar)
    {
        if (mp < max_mp)
        {
            mp += recuperar;
        }
    }
    public void Recuperar_Barra_demonio_over_time(float recuperar)
    {
        if (mp < max_mp)
        {
            mp += recuperar * Time.deltaTime;
        }
    }

    void Deshacer_invencible()
    {
        invencible = false;
    }

    public void dejar_de_recibir_danho()
    {
        recibiendo_danho = false;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject go = collision.gameObject;
        if ((go.CompareTag("Enemy") || go.CompareTag("Spikes") || go.CompareTag("ProjectileEnemy")) && !invencible)
        {
            GetComponent<AudioSource>().PlayOneShot(jugador_ouch);
            if (demonio.modo_demonio)
            {
                if (mp > mp_loss_enemy_damage)
                {
                    mp -= mp_loss_enemy_damage;
                }
                else
                {
                    hp -= hp_loss_enemy_damage - mp;//usar barra de vida cuando la de demonio se acabe
                    mp = 0;
                }
            }
            else
            {
                hp -= hp_loss_enemy_damage;
            }
            invencible = true;
            recibiendo_danho = true;
            Invoke("Deshacer_invencible", tiempo_invencible);
        }
    }

    public void Knock(Rigidbody2D rb, float knockTime, float thrustForce)
    {
        Vector3 difference = rb.transform.position - transform.position;
        difference = difference.normalized * thrustForce;
        rigidbody.DOMove(rb.transform.position - difference, knockTime);
        StartCoroutine(KnockCo(rigidbody, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D rb, float knockTime)
    {
        if (rb != null)
        {
            yield return new WaitForSeconds(knockTime);
            rb.velocity = Vector2.zero;
            rb.velocity = Vector2.zero;
        }
    }
}