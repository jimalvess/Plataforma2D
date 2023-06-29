using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//pra manipular as cenas:
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
//Função pra ser acessada no OnClick() do botão ButtonStart:
    public void LoadScenes(string cena){
/*  A cena a ser carregada depende do parâmetro.
    Na interface, no inspetor do ButtonStart, 
    no OnClick(), arrasta o Canvas abaixo do Runtime
    (o Canvas é que tem este script aqui), procura 
    este método e passa o parâmetro de cena 
    (no caso deste botão, a Fase1)
*/
        SceneManager.LoadScene(cena);
//Pro contador de tempo iniciar junto com a cena
        GameController.gc.timeCount = 30;
//Para que as vidas fiquem com seu valor padrão
        GameController.gc.lives = 3;
//Moedas padrão inicial
        GameController.gc.coins = 0;
//Quando apertar em Start Game, a música da fase vai ser reproduzida:
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().enabled = true;
    }
//Pra evitar que duas musicas se sobreponham no menu:
    public void ButtonReturn(string cena){
        SceneManager.LoadScene(cena); //Chama a cena, dependendo do parâmetro
        //Quando se tecla Return, a música da fase é desativada:
        GameObject.Find("MusicPlayer").GetComponent<AudioSource>().enabled = false;
    }


//Para o ButtonQuit (no OnClick dele):
    public void Quit(){
        Application.Quit();
    }    
}
