using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pra poder manipular as cenas do jogo
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    //Pra ver se o player tá vivo ou não
    public bool alive = true;
    void Start()
    {
        
    }

    void Update()
    {
        //Atualiza a tela sempre que iniciar o jogo
        GameController.gc.RefreshScreen();
    }

    //Pra perder vida. Vai ser chamada no script do Enemy
    public void LoseLife(){
        if(alive){
    //Pega o Clip da propriedade AudioSource do Player, atribuindo o som do array de morte
    GetComponent<Player>().audioS.clip = GetComponent<Player>().Sounds[3];
    //Toca o som 4 do array do player
    GetComponent<Player>().audioS.Play();
    //Obtem o componente animador e seta o Trigger para Dead
            gameObject.GetComponent<Animator>().SetTrigger("Dead");
    //Obtem o componente de corpo rígido, acessa a propriedade de velocidade e zera
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //Obtem o componente de cápsula 2D e desabilita ele
            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
    //Obtem o componente de corpo rígido, acessa a propriedade de tipo de corpo,
    //acessa a propriedade Kinematic pra que ele não seja mais afetado pela ação de gravidade e outras forças
            gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
    //Obtem o componente de scrip do player e desabilita
            gameObject.GetComponent<Player>().enabled = false;
    //Obtem o componente animador, seta o parâmetro bool, passa o Jump e mete falso
            gameObject.GetComponent<Animator>().SetBool("Jump", false);
    //Acessa a função de setar vidas (classe GameController) e remove uma vida
            GameController.gc.SetLives(-1);

    //Se o player ainda tem vida
            if(GameController.gc.lives >=0){
    //Invoca o método de resetar a fase. Invoke pede o tempo de espera (no caso, um segundo)
                Invoke("LoadScene", 1f);
            }
            else {
    //Morreu: zera as vidas:
                GameController.gc.lives = 0;            
    //Chama a cena do Game Over com delay (de 1 seg)
                Invoke("LoadGameOver", 1f);
            }
        }
    }

    //Pra dar um delay quando o player morre e ele ficar morto por um tempo:
    public void LoadGameOver(){
    //Desativa o áudio da fase pra tocar apenas o áudio de game over:
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().enabled = false;
    //chama a cena do Game Over
        SceneManager.LoadScene("GameOver");
    //zera a contagem regressiva
        GameController.gc.timeCount = 0;
    //zera a quantidade de vidas
        GameController.gc.lives = 0;
    //zera a quantidade de moedas
        GameController.gc.coins = 0;
    //atualiza os valores na tela
        GameController.gc.RefreshScreen();
    }

        //Pra carregar a cena depois que o player morre
    void LoadScene(){
     //manda carregar cena e passa o nome da cena que quer
        SceneManager.LoadScene("Fase1");
     //Pra contagem reiniciar junto com a fase
        GameController.gc.timeCount = 30;
    }

}
