using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VIDE_Data;

public class CountGoodness : MonoBehaviour
{
    [SerializeField] PlayerData playerData;
    public void StartDialogue(){
        if(playerData.nekAyu){
            VD.SetNode(1);
            playerData.nekAyu = false;
        }else if(playerData.diana){
            VD.SetNode(2);
            playerData.diana = false;
        }else if(playerData.jonas){
            VD.SetNode(3);
            playerData.jonas = false;
        }else if(playerData.maya){
            VD.SetNode(4);
            playerData.maya = false;
        }else if(playerData.leon){
            VD.SetNode(6);
            playerData.leon = false;
        }else if(playerData.buNina){
            VD.SetNode(7);
            playerData.buNina = false;
        }else if(playerData.currentGoodness <= 4){
            VD.SetNode(9);
        }else if(playerData.currentGoodness >= 5){
            VD.SetNode(10);
        }else{
            VD.SetNode(16);
        }
    }
}
