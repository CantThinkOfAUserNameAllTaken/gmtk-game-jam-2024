
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.UI;
using UnityEngine;
using System.Threading;

public class PlayerHealth : BaseHealth
{
    [SerializeField]
    private float _immunityTimeAfterDamage;

    private bool _damagable = true;

    [SerializeField]
    private HealthBar _healthBar;

    float time = -888;

    private float _lagFillAmount;

    private Event OnUpdate;

    [SerializeField]
    private intVariable _sceneOnDeath;

    [SerializeField]
    private GameObjectList _PlayerList;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        OnHealthDecrease += ImmunityPeriod;
        OnHealthChanged += UpdateHealthBar;

    }

    private void OnEnable()
    {
        _PlayerList.Register(gameObject);
    }

    private void OnDisable()
    {
        _PlayerList.UnRegister(gameObject);
    }

    async void ImmunityPeriod()
    {
        await Task.Run(() =>
        {
            _damagable = false;
            int delay = (int)(_immunityTimeAfterDamage * 1000);
            Task.Delay(delay).Wait();
            _damagable = true;

        });

    }

    private void UpdateHealthBar()
    {
        float fillAmount = (float)CurrentHealth / (float)MaxHealth;
        _healthBar.healthBar.fillAmount = fillAmount;
        OnUpdate += UpdateHealthBarLag;
    }

    void UpdateHealthBarLag()
    {
        _lagFillAmount = Mathf.Lerp(_healthBar.healthBarLag.fillAmount,
            _healthBar.healthBar.fillAmount, _healthBar.healthBarLagDuration * Time.deltaTime);
       // Debug.Log(_lagFillAmount);
        _healthBar.healthBarLag.fillAmount = _lagFillAmount;
        if (_healthBar.healthBar.fillAmount == _healthBar.healthBarLag.fillAmount)
        {
            OnUpdate -= UpdateHealthBarLag;
        }
    }

    protected override void Death()
    {
        base.Death();
        _sceneOnDeath.HeldData = UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex;
        UnityEngine.SceneManagement.SceneManager.LoadScene("GameOver");
    }

    public override void DamageTaken(int damage)
    {
        if (!_damagable)
        {
            return;
        }
        base.DamageTaken(damage);
    }
    // Update is called once per frame
    void Update()
    {
        /*if (Time.time - 5 > time)
        {
            DamageTaken(10);
            time = Time.time;
        }*/
        if (OnUpdate != null)
        {
            OnUpdate();
        }

        
    }

    [Serializable]
    private struct HealthBar 
    {
        public Image healthBar;
        public Image healthBarLag;
        public float healthBarLagDuration;
    }

}
