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

    public ParticleSystem[] _fires;
    private List<Transform> _fireParents = new List<Transform>();

    private CharacterController _characterController;
    private NavMeshAgent _navMeshAgent;
    private Collider _collider;
    private BurningAnimalMovement _wanderScript;

    private float _timeSinceLastBurning = 0;
    private float _burningCooldownTime = 5f;

    [SerializeField]
    [Range(0,1)]
    private float _burnChance = 0.5f;

    public GameObject _animalSoundPrefab;
    private GameObject _animalSoundInstance;

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

    void Awake()
	{

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

        _fires = GetComponentsInChildren<ParticleSystem>(true);

        for (int i = 0; i < _fires.Length; i++)
		{
            var parent = _fires[i].transform.parent;
            if(!_fireParents.Contains(parent))
			{
                _fireParents.Add(parent);
                //var parentOffset = parent.localPosition;
                //foreach (var child in parent.transform.GetComponentsInChildren<ParticleSystem>())
                {
                 //   child.transform.localPosition = parentOffset;
                }
                //parent.localPosition = Vector3.zero;
            }
		}

        foreach(var fireParent in _fireParents)
		{
            var parentOffset = fireParent.localPosition;
            foreach (var child in fireParent.transform.GetComponentsInChildren<ParticleSystem>())
            {
                child.transform.localPosition = parentOffset;
            }
            fireParent.localPosition = Vector3.zero;
        }

        _isFalling = true;

        _animalSoundInstance = GameObject.Instantiate(_animalSoundPrefab, transform);        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var fireParent in _fireParents)
		{
            fireParent.rotation = _navMeshAgent.transform.rotation;
            fireParent.position = _navMeshAgent.transform.position;
        }

        if (_burningAnimalMovement.burnState == BurningAnimalMovement.BurnState.burning && !_isFalling)
		{
            _currentHP -= burnDPS * Time.deltaTime;

            if(_currentHP <= 0)
			{
                _burningAnimalMovement.burnState = BurningAnimalMovement.BurnState.dead;
                foreach (var fire in _fires)
                {
                    fire.Stop();
                }
                //Sound when dying here!
                var foundGameObject = GetSoundComponentWithName("dyingSFX");
                foundGameObject.SetActive(true);
                var burningSFX = GetSoundComponentWithName("burningSFX");
                burningSFX.SetActive(true);
            }
		}
    }

    public void Extinguish()
	{
        _burningAnimalMovement.burnState = BurningAnimalMovement.BurnState.extinguished;
        foreach (var fire in _fires)
        {
            fire.Stop();
        }
        //Sound when getting extinguished here!
        var foundGameObject = GetSoundComponentWithName("happySFX");
        foundGameObject.SetActive(true);
    }

    public void SetOnFire()
	{
        _burningAnimalMovement.burnState = BurningAnimalMovement.BurnState.burning;
        foreach (var fire in _fires)
        {
            fire.Play();
        }
        //sound when being set on fire here!
        var foundGameObject = GetSoundComponentWithName("burningSFX");
        foundGameObject.SetActive(true);
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

    private GameObject GetSoundComponentWithName(string name)
	{
        for(int i = 0; i < _animalSoundInstance.transform.childCount; i++)
		{
            var child = _animalSoundInstance.transform.GetChild(i);

            if(child.name == name)
			{
                return child.gameObject;
			}
		}

        Debug.LogError("Sound Object with name " + name + " could not be found");
        return null;
	}
}
