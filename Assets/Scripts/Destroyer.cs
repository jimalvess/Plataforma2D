using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pra destruir o player quando cair em buracos:
public class Destroyer : MonoBehaviour
{
//Detectar a colisão
    private void OnTriggerEnter2D(Collider2D collision){
        //Se a colisão deste objeto for com o Player:
        if(collision.gameObject.CompareTag("Player")){
//Obtem o PlayerLife dele, acessa a função GameOver, pra chaar o menu:
            collision.GetComponent<PlayerLife>().LoadGameOver();
        }
    }
}
