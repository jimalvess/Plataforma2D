using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //Pra definir a velocidade que a plataforma vai se mover:
    public float moveSpeed = 2f;
    //pra ativar ou desativar as plataformas:
    public bool platform1;
    public bool platform2;
    //Pra verificar se a plataforma está se movendo nestas direções ou não:
    public bool moveRight = true;
    public bool moveUp = true;

    void Update()
    {
    //Se a plataforma 1 está ativada:
        if(platform1){
    //E se a posição dela for maior que -5 (limitando ela no deslocamento horizontal):
            if(transform.position.x > -5){
    //Para de se mover para a direita:
                moveRight = false;
            }
    //Mas se a posição atual dela for menor que -8:
            else if(transform.position.x < -8){
    //Deixa ela mover para a direita:
                moveRight = true;
            }
    //Agora, o movimento da plataforma:        
    //Se ela pode mover pra direita, que se mova:
             if(moveRight){
            //multiplica a movimentação pra direita pela velocidade e 
            //multiplica pelo deltaTime que normaliza as taxas de frames:
                transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);
            }
    //Se não for pra direita, aí é pra esquerda (seta em negativo a velocidade de movimento):
            else { 
                transform.Translate(Vector2.right * -moveSpeed * Time.deltaTime);
            }
        }

    //Agora, pra plataforma 2:
    //Se a plataforma 1 está ativada:
        if(platform2){
    //E se a posição dela for maior que -5 (limitando ela no deslocamento vertical):
            if(transform.position.y > 3){
    //Para de se mover para cima:
                moveUp = false;
            }
    //Mas se a posição atual dela for no limite de baixo:
            else if(transform.position.y < -0.80f){
    //Deixa ela mover para cima:
                moveUp = true;
            }
    //Agora, o movimento da plataforma:        
    //Se ela pode mover pra cima, que se mova:
             if(moveUp){
            //multiplica a movimentação pra cima pela velocidade e 
            //multiplica pelo deltaTime que normaliza as taxas de frames:
                transform.Translate(Vector2.up * moveSpeed * Time.deltaTime);
            }
    //Se não for pra cima, aí é pra baixo (seta em negativo a velocidade de movimento):
            else { 
                transform.Translate(Vector2.up * -moveSpeed * Time.deltaTime);
            }
        }
                
    }
}
