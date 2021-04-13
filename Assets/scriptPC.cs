using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class scriptPC : MonoBehaviour
{
    private Rigidbody2D rbd;
    private Animator anim;
    public float velocidade = 5;
    public float pulo = 300;
    public bool chao = false;
    private bool direita = true;
    private AudioSource som;
    public LayerMask mascara;
    public LayerMask mascaraAgua;
    public LayerMask mascaraMoeda;
    private bool pausado = false;

    // Start is called before the first frame update
    void Start()
    {
        rbd = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        som = GetComponent<AudioSource>();
    }


	private void OnCollisionEnter2D(Collision2D collision)
	{
        chao = true;
        transform.parent = collision.transform;
        anim.SetBool("pulando", false);
    }

	private void OnCollisionExit2D(Collision2D collision)
	{
        chao = false;
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {
        Andando();
        Pulando();

        if(Input.GetKeyDown(KeyCode.Escape))
		{
			if (pausado)
            {
                Time.timeScale = 1;
                SceneManager.UnloadSceneAsync(2);
            }
			else
			{
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            }
            pausado = !pausado;
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -transform.up, 0.8f, mascaraAgua);
        if (hit.collider != null)
        {
            Destroy(gameObject);
            SceneManager.LoadScene(3);
        }

        hit = Physics2D.Raycast(transform.position, transform.right, 0.3f, mascaraMoeda);
        if (hit.collider != null)
        {
            Destroy(hit.collider.gameObject);
            SceneManager.LoadScene(4);
            Destroy(gameObject);
        }
    }

    void Andando()
	{
        float x = Input.GetAxis("Horizontal");

        if (x == 0)
            anim.SetBool("parado", true);
        
        else
            anim.SetBool("parado", false);

        if (direita && x < 0 || !direita && x > 0)
        {
            direita = !direita;
            transform.Rotate(new Vector2(0, 180));
        }
    }

    void Pulando()
	{
        float velY;

        if (chao)
            velY = 0;
        else
            velY = rbd.velocity.y;

        if(rbd.velocity.y < 0)
            rbd.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidade, velY - 0.05f);
        else
            rbd.velocity = new Vector2(Input.GetAxis("Horizontal") * velocidade, velY);


        if (Input.GetKeyDown(KeyCode.Space) && chao)
        {
            anim.SetBool("pulando", true);
            chao = false;
            rbd.AddForce(new Vector2(0, pulo));
        }

        RaycastHit2D hit;
        hit = Physics2D.Raycast(transform.position, -transform.up, 0.8f, mascara);
        if (hit.collider != null)
		{
            Destroy(hit.collider.gameObject);
            som.Play();
        }
            
    }
}
