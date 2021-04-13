using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptInimigo : MonoBehaviour
{
    private Rigidbody2D rbd;
    private Animator anim;
    public float velocidade = 5;
    public LayerMask mascara;
    public LayerMask mascaraPC;
    public float distancia = 0.4f;
    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rbd.velocity = new Vector2(velocidade, 0);
        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, transform.right, distancia, mascara);
        if (hit.collider != null) {
            velocidade = velocidade * -1;
            rbd.velocity = new Vector2(velocidade, 0);
            transform.Rotate(new Vector2(0, 180));
        }
        RaycastHit2D hitKill;
        hitKill = Physics2D.Raycast(transform.position, transform.right, 0.8f, mascaraPC);
        if (hitKill.collider != null)
        {
            Destroy(hitKill.collider.gameObject);
            SceneManager.LoadScene(3);
        }
    }
}
