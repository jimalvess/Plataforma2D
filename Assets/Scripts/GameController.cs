using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Lib pra ter acesso aos objetos de UI:
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    //Pra acessar esta classe através de outra:
    public static GameController gc;
    //Pra acessar o texto das moedas:
    public Text coinsText;
    //Pra manipular a quantidade de moedas
    public int coins;
    //Pra manipular as vidas do player
    public Text lifeText;
    //Pra armazenar a quantidade de vidas do player
    public int lives = 3;
    //Pra manipular a contagem de tempo (acessar o TimeCount na interface)
    public Text timeText;
    //Pra definir o tempo máximo pra contagem refressiva
    public float timeCount = 30;
    //Pra verificar se o tempo terminou ou não
    public bool timeOver = false;
    //Pra ter acesso ao componente de texto do NextLevel
    public GameObject textNextLevel;

    //Pras instruções serem executadas antes do começo do jogo
    //Se o gc for nulo, recebe esta classe GameController:
    void Awake(){
        if(gc == null){
            gc = this;
    //Sempre que carregar uma nova cena, tudo que pertence ao canvas será carregado:
            DontDestroyOnLoad(gameObject); //O parâmetro é o objeto de jogo que contém este script (o canvas)
        }
        else if (gc != this){ //Se for diferente desta classe
            Destroy(gameObject); //Apaga o canvas. Aí fica só esta classe mesmo no jogo
        }
    //Atualiza a tela antes do jogo começar
        RefreshScreen();
    }

    //Pra atualizar elementos de tela
    private void Update(){
    //Chama a função de tempo na tela
        TimeCount();
    }

    //Pra definir as vidas
    public void SetLives(int life){
    //Soma a variável lives com o parâmetro recebido
        lives += life;
        if(lives >=0){ //se ainda estiver vivo
    //Atualiza o valor na tela
            RefreshScreen();
        }
    
    }

    //Pra ganhar vida com acúmulo de moedas:
    public void SetCoins(int coin){
        coins += coin;
    //A cada 10 moedas
        if(coins >= 10){
    //Se zera o número de moedas:
            coins = 0;
    //E aumenta uma vida:
            lives += 1;
        }
    //E atualiza os valores na tela:
        RefreshScreen();
    }

    //Pra atualizar a tela
    public void RefreshScreen(){
        //Atualiza a quantidade de moedas
        coinsText.text = coins.ToString();
        //Atualiza a quantidade de vidas
        lifeText.text = lives.ToString();
        //Atualiza a contagem de tempo na tela
        timeText.text = timeCount.ToString("F0"); //F0 dá só uma casa decimal
    }

    //Pra executar a contagem
    void TimeCount(){
        timeOver = false;
    //Se o tempo não acabou e a contagem é maior do que zero 
        if(!timeOver && timeCount > 0){
    //A contagem vai ser diminuída em 1 durante o tempo de jogo   
            timeCount -= Time.deltaTime;
    //Atualiza o valor na tela
            RefreshScreen();
    //Se não tiver mais tempo
            if(timeCount <= 0){
    //Zera a contagem
                timeCount = 0;
    //Pega o objeto de jogo (Player), pega o script (PlayerLife) e tira uma vida dele
                GameObject.Find("Player").GetComponent<PlayerLife>().LoseLife();
    //Aí, a contagem termina
                timeOver = true;
            }
        }
    
    }
    
}
