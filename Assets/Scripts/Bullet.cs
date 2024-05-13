using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public float velocidade = 5f;
    public float tempoVida = 3f;
    public Vector3 direcao;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("DestruirBala", tempoVida);
        GetComponent<Rigidbody>().velocity = Camera.main.transform.forward * velocidade;
    }

    void DestruirBala()
    {
        Destroy(gameObject);
    }
}
