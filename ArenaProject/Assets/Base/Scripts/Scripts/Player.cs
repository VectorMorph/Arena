using UnityEngine.Networking;
using UnityEngine;
using System.Collections;

public class Player : NetworkBehaviour {

    [SyncVar]
    private bool _isDead = false;
    public bool isDead
    {
        get { return _isDead; }
        protected set { _isDead = value; }
    }

    [SerializeField]
    private int maxHealth = 100;

    [SyncVar]
    private int currentHealth;

    [SerializeField]
    private Behaviour[] disableOnDeath;
    private bool[] wasEnabled;

    [SerializeField]
    private GameObject[] disableGameobjectsOnDeath;

    [SerializeField]
    private GameObject deathEffect;

    public void Setup()
    {
        wasEnabled = new bool[disableOnDeath.Length];
        for (int i = 0; i < wasEnabled.Length; i++)
        {
            wasEnabled[i] = disableOnDeath[i].enabled;
        }

        SetDefaults();
    }

    //preformed on all clients
    [ClientRpc]
    public void RpcTakeDamage(int _amount)
    {

        if (isDead)
        {
            return;
        }
        currentHealth -= _amount;

        Debug.Log(transform.name + " now has " + currentHealth + " health.");

        if(currentHealth <= 0)
        {
            Die();
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.K))
        {
            RpcTakeDamage(999);
        }
    }

    private void Die()
    {
        isDead = true;

        //Disable components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = false;
        }

        //Disable gameobjects 
        for (int i = 0; i < disableGameobjectsOnDeath.Length; i++)
        {
            disableGameobjectsOnDeath[i].SetActive(false);
        }

        //disable the collider
        Collider _col = GetComponent<Collider>();
        if (_col != null)
        {
            _col.enabled = false;
        }

        //spawn death effect
        GameObject _gfxinst = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(_gfxinst, 3f);

        //Switch cameras
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(true);
        }

        Debug.Log(transform.name + " Is dead!");

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        yield return new WaitForSeconds(GameManager.instance.matchSettings.respawnTime);

        SetDefaults();
        Transform _respawnPoint = NetworkManager.singleton.GetStartPosition();
        transform.position = _respawnPoint.position;
        transform.rotation = _respawnPoint.rotation;

        Debug.Log(transform.name + " Player Respawned");
    }

    public void SetDefaults()
    {
        isDead = false;

        currentHealth = maxHealth;

        //enable components
        for (int i = 0; i < disableOnDeath.Length; i++)
        {
            disableOnDeath[i].enabled = wasEnabled[i];
        }

        //Enable gameobjects 
        for (int i = 0; i < disableGameobjectsOnDeath.Length; i++)
        {
            disableGameobjectsOnDeath[i].SetActive(true);
        }

        //enable collider
        Collider _col = GetComponent<Collider>();
        if(_col != null)
        {
            _col.enabled = true;
        }

        //Switch cameras
        if (isLocalPlayer)
        {
            GameManager.instance.SetSceneCameraActive(false);
        }
    }

}
