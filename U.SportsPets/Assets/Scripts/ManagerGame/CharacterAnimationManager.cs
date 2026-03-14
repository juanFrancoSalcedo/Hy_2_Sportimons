using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MyBox;

public class CharacterAnimationManager: MonoBehaviour
{
    public Animator animatorCharacter;
    [SerializeField] bool useRigidVelocyAnimat;
    public TextureCreatureController textureControl;
    [ConditionalField(nameof(useRigidVelocyAnimat))] [SerializeField] Rigidbody rigidbodyCompetitor;
    [ConditionalField(nameof(useRigidVelocyAnimat))] [SerializeField] string nameAnimationVelocy;
    
    void Update()
    {
        if (useRigidVelocyAnimat)
        {
            SetAnimationCharacter(nameAnimationVelocy, rigidbodyCompetitor.linearVelocity.magnitude);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            SetAnimationCharacter("Dead", true);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            SetAnimationCharacter("Dash", true);
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            SetAnimationCharacter("Dash", false);
        }
    }

    public void SetAnimationCharacter(string _animacionName, bool _value)
    {
        animatorCharacter.SetBool(_animacionName, _value);
    }

    public void SetAnimationCharacter(string _animacionName)
    {
        animatorCharacter.SetTrigger(_animacionName);
    }

    public void SetAnimationCharacter(string _animacionName, float _value)
    {
        animatorCharacter.SetFloat(_animacionName, _value);
    }
}
