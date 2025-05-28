using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    [SerializeField]
    private Animator _anim;

    private AudioSource _audioSource;

    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null) {
            Debug.LogError("The Player is NULL.");
        }

        _anim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, -1, 0) * _speed * Time.deltaTime);

        if (transform.position.y < -5.0f) {
            transform.position = new Vector3(Random.Range(-8f,8f), 7, 0);
        }

    }

    private void OnTriggerEnter2D(Collider2D other) {
        //Debug.Log("Hit: " + other.transform.name);
        if(other.tag == "Player") {
            Player player = other.transform.GetComponent<Player>();
            if(player != null) {
                player.Damage();
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 2.8f);
        }

        if(other.tag == "Laser") {
            Destroy(other.gameObject);
            if(_player != null) {
                _player.AddScore(10);
            }
            _anim.SetTrigger("OnEnemyDeath");
            _speed = 0;
            _audioSource.Play();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }

}
