using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pra manipular cenas:
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    //Pra saber se estamosna última fase
    public bool ultimaFase;

    //Anexa o clipe de áudio de vitória pra ser reproduzido pelo AudioSource:
    public AudioClip finish;

    //Pra ter acesso ao objeto AudioSource do NextLevel
    public AudioSource audioS;

    void Start()
    {
        
    }

    //Pra identificar a colisão do NextLevel com o Player
    //e mostrar o texto de parabéns na cena
    private void OnTriggerEnter2D(Collider2D collision){
    
    //Se a colisão é com o objeto que tem a tag "Player"
        if(collision.CompareTag("Player")){
    //Referencia o objeto de texto e deixa ele ativo
            GameController.gc.textNextLevel.SetActive(true);
    //Procura o objeto com o nome de MusicPlayer, pega o componente de AudioSource e pára a música:        
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Stop();
    //Desabilita o script do Player, bloqueando os comandos dele:
            collision.GetComponent<Player>().enabled = false;
    //Desabilita a velocidade do Player, pra ele não se movimentar mais:
            collision.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    //Desativa a animação de "andando" do Player:
            collision.GetComponent<Animator>().SetBool("Walk", false);
    //Ativa a animação do Player parado:
            collision.GetComponent<Animator>().Play("Player_idle");
    //Pega o componente AudioSource e seta o som de "finish" nele:
            GetComponent<AudioSource>().clip = finish;
    //Toca o "finish":
            GetComponent<AudioSource>().Play();
    //Chama a próxima fase epós o tempo em sefundos:
            Invoke("NextScenes", 5f);
        }
    }

    //Criar o atraso da Fase1 para a Fase2
    //Toda a lógica de passar de fase vem aqui
    void NextScenes(){
    //Pra saber se é a última fase
        if(ultimaFase){
    //Carrega a cena Menu
            SceneManager.LoadScene("Menu"); 
    //Desativa a frase de parabéns
            GameController.gc.textNextLevel.SetActive(false);
    //zera a contagem regressiva
            GameController.gc.timeCount = 0;
    //zera a quantidade de vidas
            GameController.gc.lives = 0;
    //zera a quantidade de moedas
            GameController.gc.coins = 0;
    //atualiza os valores na tela
            GameController.gc.RefreshScreen();
    //Pega o objeto "MusicPlayer", propriedade AudioSource e pára a música da fase:
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().enabled = false;
        } else {
    //Pega a cena ativa, acessa o índice das cenas em construção
    //na interface e soma mais 1, vai carregar a próxima cena
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    //Aí, desativa o texto de parabéns
            GameController.gc.textNextLevel.SetActive(false);
    //Pra que a próxima fase venha com a contagem regressiva, seta ela novamente
            GameController.gc.timeCount = 30f;
    //Faz a música da fase voltar a tocar, já que estamos em uma fase que não é a última:
            GameObject.Find("MusicPlayer").GetComponent<AudioSource>().Play();
        }
    }
}
