using CharacterController = ShootEmUp.Controller.CharacterController;
using ShootEmUp.Controller;
using ShootEmUp.Model;
using UnityEngine;
using System;
using Zenject;

public class GameplayManager : MonoBehaviour
{
    private IDeltaUpdatable[] _deltaUpdatable;
    private IControl[] _control;

    private bool _isInitialized;

    [Inject]
    private void Construct(CharacterController characterController,EnemysController enemysController,ObserverController observerController,BulletController bulletController,GameController gameController)
    {
        if (_isInitialized)
            return;

        _deltaUpdatable = new IDeltaUpdatable[]
        {
            characterController,
            enemysController,
            bulletController
        };
        _control = new IControl[]
        {
            characterController,
            enemysController,
            bulletController,
            observerController,
            gameController
        };

        _isInitialized = true;
    }

    public void Enable()
    {
        if(enabled == false)
            enabled = true;
    }

    public void Disable() 
    {
        if(enabled)
            enabled = false; 
    }

    private void OnEnable()
    {
        if (_isInitialized == false)
            throw new InvalidOperationException();

        for (int i = 0; i < _control.Length; i++)
            _control[i].Enable();
    }

    private void OnDisable()
    {
        if (_isInitialized == false)
            throw new InvalidOperationException();

        for (int i = 0; i < _control.Length; i++)
            _control[i].Disable();
    }

    private void Update()
    {
        for (int i = 0; i < _deltaUpdatable.Length; i++)
            _deltaUpdatable[i].Update(Time.deltaTime);
    }

    private void OnValidate()
    {
        if(enabled)
            enabled = false;
    }
}