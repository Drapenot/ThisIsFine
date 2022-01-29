using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurningAnimal : MonoBehaviour, ICanBeSetOnFire
{
    public float startHP;
    private float _currentHP;
    public float burnDPS = 1;
    private BurningAnimalMovement _burningAnimalMovement;
    private bool _isFalling = true;

    public ParticleSystem _fire;

    private CharacterController _characterController;
    private NavMeshAgent _navMeshAgent;
    private Collider _collider;
    private BurningAnimalMovement _wanderScript;

    private float _timeSinceLastBurning = 0;
    private float _burningCooldownTime = 5f;

    [SerializeField]
    [Range(0,1)]
    private float _burnChance = 0.5f;

    public float BurnChance
	{
        get
		{
            if(_timeSinceLastBurning >= _burningCooldownTime)
			{
                return _burnChance;
            }
			else
			{
                return 0;
			}
		}
	}


    // Start is called before the first frame update
    void Start()
    {
        _burningAnimalMovement = GetComponentInChildren<BurningAnimalMovement>();
        _currentHP = startHP;

        _characterController = GetComponentInChildren<CharacterController>();
        _navMeshAgent = GetComponentInChildren<NavMeshAgent>();
        _wanderScript = GetComponentInChildren<BurningAnimalMovement>();
        _collider = GetComponent<Collider>();

        _characterController.enabled = false;
        _navMeshAgent.enabled = false;
        _wanderScript.enabled = false;

        _fire = GetComponentInChildren<ParticleSystem>();
        _isFalling = true;
    }

    // Update is called once per frame
    void Update()
    {
        _fire.transform.position = _navMeshAgent.transform.position;
        _fire.transform.eulerAngles = Vector3.zero;

        if (_burningAnimalMovement.burnState == BurningAnimalMovement.BurnState.burning && !_isFalling)
		{
            _currentHP -= burnDPS * Time.deltaTime;

            if(_currentHP <= 0)
			{
                _burningAnimalMovement.burnState = BurningAnimalMovement.BurnState.dead;
                _fire.Stop();
                //Sound when dying here!
            }
		}
    }

    public void Extinguish()
	{
        _burningAnimalMovement.burnState = BurningAnimalMovement.BurnState.extinguished;
        _fire.Stop();
        //Sound when getting extinguished here!
	}

    public void SetOnFire()
	{
        _burningAnimalMovement.burnState = BurningAnimalMovement.BurnState.burning;
        _fire.Play();
        //sound when being set on fire here!
    }

    public bool IsBurning()
	{
        return _burningAnimalMovement.burnState == BurningAnimalMovement.BurnState.burning;
    }

    bool once = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6 && !once)
        {
            _characterController.enabled = true;
            _navMeshAgent.enabled = true;
            _wanderScript.enabled = true;
            _collider.enabled = false;

            var rigidbody = GetComponent<Rigidbody>();
            rigidbody.angularVelocity = Vector3.zero;
            rigidbody.velocity = Vector3.zero;

            _isFalling = false;


            Destroy(rigidbody);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(IsBurning())
		{
            var burnable = other.GetComponent<ICanBeSetOnFire>();
            if (burnable != null && !burnable.IsBurning() && Random.value < burnable.BurnChance)
            {
                burnable.SetOnFire();
            }
        }
    }
}
