using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour{
    //Pra ter acesso ao rigidbody do player.
    private Rigidbody2D rbPlayer;
    //Pra alterar a velocidade na interface. Se é pública, aparece lá
    public float speed;
    //Pra ter acesso a renderização do player                                     =
    private SpriteRenderer sr;
    //Pra gerar a força do pulo. Pública, aparece na interface
    public float jumpForce;
    //Pra saber se o player tá no chão
    public bool inFlor = true;
    //Pra animação dos sprites (depois das animações prontas na interface)
    private Animator playerAnim;
    //Pro pulo duplo
    public bool doubleJump;
    //Pro pulo triplo
    public bool tripleJump;
    //Pra lincar com a classe GameController
    private GameController gcPlayer;
    //Pra ter acesso ao componente AudioSource do Player
    public AudioSource audioS;
    //Array de sons pq o Player vai ter mais de um som:
    public AudioClip[] Sounds;
    
    void Start(){
    //Primeira coisa é associar a variável gc da gameController aqui no player
        gcPlayer = GameController.gc;
    //Aí, com a classe já acessada, dá pra usar os atributos dela
        gcPlayer.coins = 0; 
    //Bem no início já seta os componentes de rigidbody na variável
        rbPlayer = GetComponent<Rigidbody2D>();
    //variável de render recebe o componente pra dar acesso aos lances dele na interface
        sr = GetComponent<SpriteRenderer>();
    //variável de animação recebe o componente pra dar acesso aos lances dele na interface
        playerAnim = GetComponent<Animator>();
    }

    //Método que vai chamar de forma fixa o update do movimento
    private void FixedUpdate(){
        MovePlayer();
    }

    void Update(){
    //Verifica o tempo todo se o boneco tá pulando
        Jump();
    }

    //Método de movimento
    void MovePlayer(){
    //Pega o input. O "Horizontal" deve estar escrito igual ao setado
    //na interface (Project Settings > Input Manager), como string
        float horizontalMovement = Input.GetAxisRaw("Horizontal");
    //O Vextor2 só trabalha em X e Y
    //(X multiplica o movimento pela velocidadee Y é só o Y mesmo)
        rbPlayer.velocity = new Vector2(horizontalMovement * speed, rbPlayer.velocity.y);

    //Pro player virar de lado, usando o componente de render
        if (horizontalMovement > 0){
            sr.flipX = false; //se estiver teclando -> ele não vira
            playerAnim.SetBool("Walk", true); //Chama animação de caminhar
        }
        else if (horizontalMovement < 0){
            sr.flipX = true; //aí vira e executa animação de caminhar
            playerAnim.SetBool("Walk", true); 
        }
        else{
            playerAnim.SetBool("Walk", false); //se não tá caminhando, fica parado =) 
        }
    }

    //Pro player pular
    void Jump(){
     //pega o botão que foi pressionado (olha o nome no input manager da interface)
        if (Input.GetButtonDown("Jump")){ //Se foi apertado o botão de pular
    /*
    Pega Rigidbody2D dele e adiciona a força do pulo no novo Vector2(X,Y).
    O ForceMode2D é o tipo de força. Pode ser força mesmo ou impulso.
    Como isso é aplicado na massa do player, tem que aumentar o peso dele,
    pra ele não desaparecer da tela (RigidBody na interface > Mass)
    */
        if (inFlor){ //Se estiver no chão
            audioS.clip = Sounds[1]; //Atribuo o elemento 1 do array à propriedade Clip do AudioSource
            audioS.Play(); //Toca o som de pular
            rbPlayer.velocity = Vector2.zero; //pra não dar diferença nos pulos 
            rbPlayer.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse); //dá o impulso
            playerAnim.SetBool("Jump", true); //faz a animação de pulo
            inFlor = false; //já tá no ar pq pulou. Aí não pula mais até cair
            doubleJump = true; //Pode dar o pulo duplo pq não tá no chão
        }
        else if (!inFlor && doubleJump) { // se já tá pulando e já deu o pulo duplo
            audioS.clip = Sounds[1]; //Atribuo o elemento 1 do array à propriedade Clip do AudioSource
            audioS.Play(); //Toca o som de pular
            rbPlayer.velocity = Vector2.zero; //pra não dar diferença nos pulos 
            rbPlayer.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse); //dá o impulso
            playerAnim.SetBool("Jump", true); //faz a animação de pulo
            inFlor = false; //já tá no ar pq pulou. Aí não pula mais até cair
            doubleJump = false; //Não pode dar o pulo duplo pq já deu
            tripleJump = true; //Mas o triplo pode
        }
        else if (!inFlor && tripleJump) { // se já tá pulando e já deu o pulo triplo
            audioS.clip = Sounds[1]; //Atribuo o elemento 1 do array à propriedade Clip do AudioSource
            audioS.Play(); //Toca o som de pular
            rbPlayer.velocity = Vector2.zero; //pra não dar diferença nos pulos 
            rbPlayer.AddForce(new Vector2(0,jumpForce), ForceMode2D.Impulse); //dá o impulso
            playerAnim.SetBool("Jump", true); //faz a animação de pulo
            inFlor = false; //já tá no ar pq pulou. Aí não pula mais até cair
            doubleJump = false; //Não pode dar o pulo duplo pq já deu
            tripleJump = false; //Nem o triplo, pq tbm já deu
        }
     }  
    }

    //Detecta se o player tá no chão pra ele não pular eternamente
    // ou se ele está em cima da plataforma. Aí, acontece a colisão
    private void OnCollisionEnter2D(Collision2D collision){
        if (collision.gameObject.layer == 6){ //O chão e a plataforma estão nesta camada (ground)
            inFlor = true;
            playerAnim.SetBool("Jump", false); //para a animação de pulo
            doubleJump = false; //Quando cair no chão, não pode dar pulo duplo
            tripleJump = false; //Quando cair no chão, não pode dar pulo triplo
        }
    //Se a colisão for com o objeto de jogo identificado como platform:
        if(collision.gameObject.tag == "Platform"){
    //Pega o transform do objeto de jogo, pega o pai dele (o Player)
    //e atribui a localização do objeto de colisão (que é a plataforma).
    //Assim, a localização do player tá amarrada com a da plataforma
    //e ele não vai ficar no ar caindo enquanto ela desce:
            gameObject.transform.parent = collision.transform;
        }
    }

    //O Player acaba acompanhando o movimento da plataforma
    //como resultado do método aí de cima. Para isso, tem
    //que resetar a posição dele quando não estiver nela:
    void OnCollisionExit2D(Collision2D collision){
    //Se está saindo da colisão com a plataforma:
        if(collision.gameObject.tag == "Platform"){
    //Pega o objeto de jogo que é pai do transform e atribui nulo pra ele voltar ao estado padrão:    
        gameObject.transform.parent = null;
        }
    }

    //Método da classe MonoBehavior pra saber quando o player
    //entrou em contato com o trigger da moeda
    private void OnTriggerEnter2D(Collider2D collision){

    //Se a colisão foi com o objeto de jogo com marcação igual a coins
        if(collision.gameObject.tag == "Coins"){
            audioS.clip = Sounds[0]; //Atribuo o elemento 0 do array à propriedade Clip do AudioSource
            audioS.Play(); //Toca o som de moeda
            Destroy(collision.gameObject);   //Destrói a moeda, objeto da colisão
    //Acessa a função setCoins() do GameController e manda uma moeda:
            gcPlayer.SetCoins(1);
    //Atualiza a quantidade de moedas na tela
            GameController.gc.RefreshScreen(); 
    }

    //Se a colisão for com o objeto de jogo com tag igual a Enemy, mata o inimigo:
        if(collision.gameObject.tag == "Enemy"){
            audioS.clip = Sounds[2]; //Atribuo o elemento 2 do array à propriedade Clip do AudioSource
            audioS.Play(); //Toca o som do bicho
            rbPlayer.velocity = Vector2.zero; //Elimina a interferência da velocidade
            rbPlayer.AddForce(Vector2.up * 5, ForceMode2D.Impulse); //Adiciona a força de impulso no eixo Y prum pulinho
            collision.gameObject.GetComponent<SpriteRenderer>().flipY = true; //Pega o render do inimigo e vira ele de cabeça pra baixo
            collision.gameObject.GetComponent<Enemy>().enabled = false; //Desabilita o Script do inimigo
            collision.gameObject.GetComponent<CapsuleCollider2D>().enabled = false; //Desabilita a cápsula collider do inimigo
            collision.gameObject.GetComponent<BoxCollider2D>().enabled = false; //Desabilita box de colisão do inimigo
            collision.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; //Desabilita gravidade e forças do inimigo
            Destroy(collision.gameObject, 1f); //destrói o inimigo após um segundo
        }

    }
}