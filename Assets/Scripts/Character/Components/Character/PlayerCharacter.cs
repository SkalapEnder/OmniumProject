using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character
{
    public CinemachineVirtualCamera m_Camera { get; set; }
    [SerializeField] private float intensityShake;
    [SerializeField] private float timeShake;
    private CinemachineBasicMultiChannelPerlin m_MultiChannelPerlin;
    private float timer;

    public override Character Target 
    { 
        get
        {
            Character target = null;
            float minDistance = float.MaxValue;
            List<Character> list = GameManager.Instance.CharacterFactory.ActiveCharacters;

            for(int i = 0; i < list.Count; i++) 
            {
                if (list[i].CharacterType == CharacterType.Player)
                    continue;

                float distanceBetween = Vector3.Distance(list[i].transform.position, transform.position);
                if (distanceBetween < minDistance)
                {
                    target = list[i];
                    minDistance = distanceBetween;
                }
                 
            }

            if (minDistance > 20) return null;
            else return target;
        } 
    }

    public override void Initialize()
    {
        base.Initialize();

        CharacterInput = GetComponent<PlayerInput>();
        AudioSource = GetComponent<AudioSource>();

        InputService = new PlayerInputService();
        InputService.Initialize(this);

        AttackComponent = new WeaponAttackComponent();
        AttackComponent.Initialize(this);

        ControlComponent = new PlayerControlComponent();
        ControlComponent.Initialize(this);

        m_Camera = Camera.main.GetComponent<CinemachineVirtualCamera>();
        m_MultiChannelPerlin = m_Camera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        m_Camera.LookAt = transform;
        m_Camera.Follow = transform;



        //DamageComponent = new CharacterDamageComponent();
        //DamageComponent.Initialize(this);
    }

    public override void Update()
    {
        if (!LiveComponent.IsAlive || !GameManager.Instance.IsGameActive) 
        {
            return;
        }

        InputService.OnUpdate();
        ControlComponent.OnUpdate();

        if (timer < 0)
        {
            if(m_MultiChannelPerlin.m_AmplitudeGain != 0f)
                m_MultiChannelPerlin.m_AmplitudeGain = 0f;
            return;
        }
        timer -= Time.deltaTime;
    }

    public override void CameraShake()
    {
        timer = timeShake;
        m_MultiChannelPerlin.m_AmplitudeGain = intensityShake;
    }
}
