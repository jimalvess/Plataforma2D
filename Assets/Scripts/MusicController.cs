using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    //Pra música persistir entre todas as fases:
    private static MusicController mC;


    void Awake()
    {
        //Se não existe na cena
        if(mC == null){
            //Recebe esta classe e o objeto não vai ser destruído na cena (mantendo nas outras cenas)
            mC = this;
            DontDestroyOnLoad(gameObject);
        //Mas se for diferente desta classe
        }else if(mC != this){
            //Aí destrói o objeto (mantendo a persistência desta música aqui)
            Destroy(gameObject);
        }
        
    }
}
