using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BurningAnimalMovement : MonoBehaviour
{
    private CharacterController _characterController;
    private BurnState _burnState;
    public BurnState burnState
	{
		get
		{
            return _burnState;
		}
        set
		{
            if(value != _burnState)
			{
                _burnState = value;
                OnBurnStateChanged();
			}
		}
	}
    private Animator _animator;
    public float turnSpeed = 2;
    private float _timer = 0;
    private NavMeshAgent _navMeshAgent;
    public float minPanicDistance = 10;
    public float maxPanicDistance = 30;
    public string animatorMoveParameterName = "isRunning";


    public enum BurnState
	{
        burning,
        extinguished,
        dead
	}

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        OnBurnStateChanged();

    }

    // Update is called once per frame
    void Update()
    {
        if(_burnState == BurnState.burning)
		{
            if (_timer <= 0 || _navMeshAgent.remainingDistance <= 0.5f)
            {
                if (!_navMeshAgent.isOnNavMesh)
				{
                    return;
				}

                var targetPosition = new Vector3(Random.Range(minPanicDistance, maxPanicDistance) * Mathf.Sign(Random.Range(-1, 1)), 0, Random.Range(minPanicDistance, maxPanicDistance) * Mathf.Sign(Random.Range(-1, 1))) + transform.position;
                if(_navMeshAgent.isOnNavMesh)
				{
                    _navMeshAgent.SetDestination(targetPosition);
				}
                _timer = 4.5f;
            }

            _timer -= Time.deltaTime;

            FaceDirection(_navMeshAgent.destination);
        }
        if(_burnState == BurnState.extinguished)
		{
            FaceDirection(_navMeshAgent.destination);
        }



    }

    void FaceDirection(Vector3 facePosition)
    {
        transform.rotation = Quaternion.LookRotation(Vector3.ProjectOnPlane(Vector3.RotateTowards(transform.forward,
            facePosition, turnSpeed * Time.deltaTime * Mathf.Deg2Rad, 0.5f), Vector3.up), Vector3.up);
    }

    private void OnBurnStateChanged()
	{
        switch (_burnState)
        {
            case (BurnState.burning):
                _animator.SetBool(animatorMoveParameterName, true);
                break;
            case (BurnState.extinguished):
                _animator.SetBool(animatorMoveParameterName, true);
                _navMeshAgent.SetDestination(ExitZones.Instance.GetClosestExitZone(transform.position).GetPosition());
                //_navMeshAgent.isStopped = true;
                break;
            case (BurnState.dead):
                if(_navMeshAgent.isOnNavMesh)
				{
                    _navMeshAgent.isStopped = true;

				}
                _animator.SetBool("isDead", true);
                _animator.SetBool(animatorMoveParameterName, false);
                break;

            default:
                break;
        }
    }
}
