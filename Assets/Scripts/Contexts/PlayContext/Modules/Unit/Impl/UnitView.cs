using PathCreation;

using strange.extensions.mediation.impl;

using UnityEngine;

public class UnitView : View
{
    private float _distanceTravelled;
    private Animator _animator;

    [SerializeField] private PathCreator _pathCreator;
    [SerializeField] private EndOfPathInstruction _endOfPathInstruction;
    [SerializeField] private float _speed = 5;

    [Inject] public UnitModelInteractedSignal UnitModelInteractedSignal { get; set; }

    private void Start()
    {
        base.Start();

        _animator = GetComponent<Animator>();
        if (_pathCreator != null)
        {
            // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
            _pathCreator.pathUpdated += OnPathChanged;
        }
    }

    private void Update()
    {
        _animator.SetFloat("Forward", 10f, 0.1f, Time.deltaTime);

        if (_pathCreator != null)
        {
            _distanceTravelled += _speed * Time.deltaTime;

            var newPosition = _pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
            newPosition.y = transform.position.y;
            
            transform.position = newPosition;
            transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
        }
    }

    // If the path changes during the game, update the distance travelled so that the follower's position on the new path
    // is as close as possible to its position on the old path
    private void OnPathChanged()
    {
        _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(transform.position);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            UnitModelInteractedSignal.Dispatch(interactable);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
        {
            UnitModelInteractedSignal.Dispatch(interactable);
        }
    }
}
