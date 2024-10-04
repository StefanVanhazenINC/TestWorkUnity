using AxGrid.Base;
using AxGrid.Model;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviourExtBind
{

    [SerializeField] private Button _openButton;
    [SerializeField] private Button _closeButton;
    [SerializeField] private ParticleSystem _particleSystem;

    [Bind("InitUI")]
    public void InitUI()
    {
        _openButton.interactable = true;
        _closeButton.interactable = false;

    }
    [Bind("StopAnimChest")]
    public void EnableOpenButton()
    {
        _openButton.interactable = true;
    }
    [Bind("StopAnimChest")]
    public void ParticlePlay() 
    {
        _particleSystem.Play();
    }

    [Bind("EnableStopChest")]
    public void EnableStopButton()
    {
       _closeButton.interactable = true;
    }


    [Bind("StartViewSpining")]
    public void DisableOpenButton() 
    {
        _openButton.interactable = false;

    }
    [Bind("OnStopSpinClick")]
    public void DisableStopButton()
    {
        _closeButton.interactable = false;
    }



}
