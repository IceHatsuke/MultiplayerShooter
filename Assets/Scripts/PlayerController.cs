using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    // Instancia dos prefabs da bala
    public Transform bulletPrefab;
    public Transform bulletPoint;

    // Movimento e pulo
    public float velocidadeMovimento = 5f;
    public float velociadeRotacao = 5f;
    public float forcaPulo = 5f;
    public float tempoPulo = 1.5f;

    // Rotação do personagem
    public float rotacaoX = 0;
    public float limiteVisaoY = 0;
    public float gravidade = 9.0f;


    public float frequenciaTiro = 0.5f;
    bool podeAtirar = true;

    Vector3 direcaoMovimento = Vector3.zero;
    CharacterController cc;


    private void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float direcaoX = Input.GetAxis("Horizontal");
        float direcaoZ = Input.GetAxis("Vertical");
        float direcaoY = direcaoMovimento.y;

        if(Input.GetKey(KeyCode.Mouse0) && podeAtirar == true)
        {
            Atirando();
        }

        Vector3 frente = transform.TransformDirection(Vector3.forward);
        Vector3 direita = transform.TransformDirection(Vector3.right);

        // Movimento do jogador
        direcaoMovimento = (frente * direcaoZ) + (direita * direcaoX);
        direcaoMovimento *= velocidadeMovimento;

        // Pular
        if(Input.GetKeyDown(KeyCode.Space))
        {
            direcaoMovimento.y = forcaPulo;
        } 
        else
        {
            direcaoMovimento.y = direcaoY;
        }

        // Aplicando gravidade
        direcaoMovimento.y -= gravidade * Time.deltaTime;
        // Move o jogador
        cc.Move(direcaoMovimento * Time.deltaTime);

        // Rotação Jogador
        rotacaoX -= Input.GetAxis("Mouse Y") * velociadeRotacao;
        rotacaoX = Mathf.Clamp(rotacaoX, - limiteVisaoY, limiteVisaoY);
        Camera.main.transform.localRotation = Quaternion.Euler(rotacaoX, 0, 0);
        transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * velociadeRotacao, 0);

    }

    void Atirando()
    {
        podeAtirar = false;
        Transform instancia = Instantiate(bulletPrefab, bulletPoint.position, Quaternion.Euler(Camera.main.transform.forward));

        Invoke("ResetarTiro", frequenciaTiro);
    }


    void ResetarTiro()
    {
        podeAtirar = true;
    }

}
