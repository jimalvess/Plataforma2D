using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Pra poder manipular as cenas do jogo
using UnityEngine.SceneManagement;

public class Enemy : MonoBehaviour
{
    //Pra manipular a velocidade do inimigo na interface:
    public float speed;
    //Pra saber se o inimigo tá no chão ou não:
    public bool ground = true;
    //Pra ter acesso ao transform do objeto GroudCheck, no pé do inimigo
    public Transform groundChek;
    //Camada pisada pelo inimigo
    public LayerMask groundLayer;
    //Pra saber se a face do inimigo está para a direita ou não
    public bool facingRight = true;

    void Start()
    {
        
    }

    void Update()
    {
    /*
    Move o transform na direção e distância de translação
    recebe o eixo que se quer movimentar (queremos o X)
    O Time.deltaTime acerta a taxa de frames
    */
        transform.Translate(Vector2.right * speed * Time.deltaTime);        
    /*
    Na interface, dentro do objeto Enemy, tem um GroundCheck
    Ele tá entre o pé do cara e o chão. Physics2D permite nosso acesso 
    às físicas do jogo. Linecast() é usado pra criar uma linha 
    imaginária entre 2 pontos na cena (recebe um que começa e um que termina).
    O início é a posição do GroundCheck, a posição do inimigo 
    e a camada que vai ser pisada (groundLayer)
    */
        ground = Physics2D.Linecast(groundChek.position, transform.position, groundLayer);

        if(ground == false){ 
            speed *= -1; //Faz o inimigo ir e voltar automaticamente
        }
        if(speed > 0 && !facingRight){ //Se tá andando pra direita e a face virada pra esquerda
            Flip();
        }
        else if (speed < 0 && facingRight){ //Se tá andando para a esquerda e a face virada pra direita
            Flip();
        }
    }
    //Pra fazer a face do inimigo virar
    void Flip(){
        facingRight = !facingRight; //a variável recebe o contrário dela
        //Pega a escala local do inimigo
        Vector3 scale = transform.localScale;
        //Acessa o eixo X da escala e vira ele
        scale.x *= -1;
        //Atualiza a escala local
        transform.localScale = scale;
    }
    //Pra matar o player
    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.gameObject.tag == "Player"){ //Se a colisão for com um objeto de jogo chamado player
    //Pega o objeto da colisão (player), obtem o script de PlayerLife e acessa a função de perder vida (public)
        collision.gameObject.GetComponent<PlayerLife>().LoseLife();
        }
    }
    //Pra carregar a cena depois que o player morre
    void LoadScene(){
        //manda carregar cena e passa o nome da cena que quer
        SceneManager.LoadScene("Fase1");
    }
}
