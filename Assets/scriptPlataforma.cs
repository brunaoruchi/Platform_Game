using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptPlataforma : MonoBehaviour
{
    private float count = 0;
    public float velocidade = 3;
    private Vector2 posInicial;
    public float altura = 1.8f;
    public float largura = 1.8f;
    
    // Start is called before the first frame update
    void Start()
    {
        posInicial = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        count += velocidade * Time.deltaTime;

        float posX = Mathf.Cos(count) * largura;
        float posY = Mathf.Sin(count) * altura;

        transform.position = new Vector2(posInicial.x + posX, posInicial.y + posY);

        if (count >= 2 * Mathf.PI)
            count = 2 * Mathf.PI - count;
    }
}
