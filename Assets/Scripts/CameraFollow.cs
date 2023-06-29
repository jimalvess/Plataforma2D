using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Pra camera seguir o player do jeito certo
public class CameraFollow : MonoBehaviour{

    //Pra ter acesso ao component transform do player
    //e poder arrastar ele na interface pra dentro da variável
    public Transform player;

    //Pra ter acesso aos limites em X da camera:
    public float minX, maxX;

    //Pra interpolação de suavização da câmera:
    public float timeLerp;

    //Método pra que a atualização siga uma taxda fixa de quadros
    private void FixedUpdate(){

    //Variavel pra câmera poder pegar a cena toda
    Vector3 newPosition = player.position + new Vector3(0,0,-10);

    //Mudando o eixo y pra que não fique borda aparecendo
    newPosition.y = 0.1f;
    /*
    Aplicando atraso na câmera pra seguir o player. 
    Lerp cria uma interpolação linear entre dois pontos. 
    Vai ser da posição atual da câmera até a nova posição, 
    passa também em quanto tempo vai ser feita a interpolação
    */
    newPosition = Vector3.Lerp(transform.position, newPosition, timeLerp);
    //Aplica a posição já com o eixo y modificado
    transform.position = newPosition;

    /*
    Pra limitar a camera no eixo X usa minX até maxX
    a fim de que não apareça nada fora do jogo.
    Pega a posição da câmera e aplica um Vetor3.
    A estrutura Mathf acessa funções de matemática. 
    Usa Clamp (prender) pra dar os valores min e max.
    Isso recebe como parâmetro a posição atual no X, 
    mínimo e máximo de abrangencia. Para o valor de Y e Z
    do Vector3, passa a posição atual
    */
    transform.position = new Vector3(Mathf.Clamp(transform.position.x,minX,maxX), transform.position.y, transform.position.z);
    }
}
