using System;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;


public enum Mood
{
    Chaising,
    Escaping,
    Patrolling
}

namespace Enemy
{
    public class AiEnemyController : MonoBehaviour
    {
        public NavMeshAgent _agent;
        [SerializeField] private NavMeshSurface map;
        [SerializeField] private Transform[] _waypoints;
        private Animator _animator;
        private Vector3 _walkPoint;
        private float _walkPointRange = 5f;
        public GameObject _player;

        private bool _walkPointSet = false;
        private bool _isAttacked = false;
        private bool _playerInSightRange;
        bool _isSafe = false;
        private const float RangeAgainstPlayer = 5f;
        private Mood _mood = Mood.Patrolling;
        private Transform _currentWayPoint = null;
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
            GameObject mapObject = GameObject.Find("NavMesh Surface");
            map = mapObject.GetComponent<NavMeshSurface>();
            _player = GameObject.FindWithTag("Player");


        }

        void Start()
        {
            GameObject waypointsObject = GameObject.Find("Waypoints");

            if (waypointsObject != null)
            {
                _waypoints = waypointsObject.GetComponentsInChildren<Transform>();
                _waypoints = System.Array.FindAll(_waypoints, t => t != waypointsObject.transform);
            }
        }

        public void changeState(Mood state)
        {
            _mood = state;
            if (_mood == Mood.Chaising)
            {
                _isAttacked = true;
            }
            else
            {
                _isAttacked = false;
            }
            Action();

        }

        private bool getIfCurrentWayPointIsReached()
        {

            if (_currentWayPoint == null)
                return false;


            Vector3 distanceToWayPoint = transform.position - _currentWayPoint.position;
            if (distanceToWayPoint.magnitude < 6f)
            {
                _walkPointSet = false;
                return true;

            }


            return false;
        }

        private void FixedUpdate()
        {
            Action();
        }

        private void Action()
        {
            Transform playerTransform = _player.transform;
            Vector3 distanceToPlayer = transform.position - playerTransform.position;
            float speed = _agent.velocity.magnitude;
            // Vérifie si l'IA a atteint un waypoint sûr
            _isSafe = getIfCurrentWayPointIsReached();
            if (_isSafe && !_isAttacked)
            {
                _mood = Mood.Patrolling;
            }
            switch (_mood)
            {
                case Mood.Escaping:
                    if (distanceToPlayer.magnitude < RangeAgainstPlayer && !_isSafe)
                    {
                        Escape();
                    }
                    else if (_isSafe)
                    {
                        _mood = Mood.Patrolling;
                    }
                    break;

                case Mood.Patrolling:
                    if (distanceToPlayer.magnitude < RangeAgainstPlayer)
                    {
                        _mood = Mood.Escaping;
                    }
                    else
                    {
                        Patroling();
                    }
                    break;

                case Mood.Chaising:
                    ChaisePlayer();
                    break;
            }

        }

        // chaising the player
        private void ChaisePlayer()
        {
            _animator.SetFloat("speed", 1.1f);
            _agent.speed = 14f;
            _agent.SetDestination(_player.transform.position);
        }



        private void Escape()
        {
            Transform playerTransform = _player.transform;
            Transform fleeWaypoint = SelectFleeWaypoint(playerTransform);
            _animator.SetFloat("speed", 1.1f);

            _agent.speed = 14f;
            if (fleeWaypoint != null)
            {
                _agent.SetDestination(fleeWaypoint.position);
            }

        }






        private Transform SelectFleeWaypoint(Transform playerTransform)
        {
            Transform bestWaypoint = null;
            float maxDistance = 0f;

            foreach (Transform waypoint in _waypoints)
            {
                float distanceToPlayer = Vector3.Distance(playerTransform.position, waypoint.position);
                float distanceToEnemy = Vector3.Distance(transform.position, waypoint.position);

                // Vérifier si le waypoint est plus loin du joueur et plus proche de l'ennemi
                if (distanceToPlayer > maxDistance && distanceToEnemy < distanceToPlayer)
                {
                    bestWaypoint = waypoint;
                    _currentWayPoint = waypoint;
                    maxDistance = distanceToPlayer;
                }
            }

            return bestWaypoint;
        }


        private void ContinuousEscape()
        {
        }

        private void Patroling()
        {
            _animator.SetFloat("speed", 0.49f);

            _agent.speed = 6f;

            if (!_walkPointSet)
                SearchWalkPoint();

            if (_walkPointSet)
                _agent.SetDestination(_walkPoint);

            Vector3 distanceToWalkPoint = transform.position - _walkPoint;

            if (distanceToWalkPoint.magnitude < 2f)
            {
                _walkPointSet = false;
            }
        }



        private void SearchWalkPoint()
        {
            // Calculer une position aléatoire dans la plage définie
            float randomZ = Random.Range(-_walkPointRange, _walkPointRange);
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);

            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
            _walkPointSet = true;


        }

    }
}
