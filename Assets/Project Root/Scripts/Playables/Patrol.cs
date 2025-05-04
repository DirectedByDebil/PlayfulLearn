using UnityEngine;
using System.Collections.Generic;
using System.Collections;

namespace Playables
{
    public sealed class Patrol : MonoBehaviour
    {

        [SerializeField, Space]
        private List<Transform> _patrolPoints;


        private Vector3 NextPoint
        {
            get
            {

                if(CurrentIndex == _patrolPoints.Count - 1)
                {

                    return _patrolPoints[0].position;
                }

                return _patrolPoints[CurrentIndex +1 ].position;
            }
        }


        private int CurrentIndex
        {
            get => _currentIndex;

            set
            {

                _currentIndex = value;


                if (_currentIndex == _patrolPoints.Count)
                {

                    _currentIndex = 0;
                }
            }
        }
       

        private int _currentIndex;


        private float _interpolationTime;

        private float _elapsedTime;


        private bool _isPlaying;


        public void StartPatrol(float interpolationTime)
        {
            
            _interpolationTime = interpolationTime;
            

            if(!_isPlaying)
            {

                _isPlaying = true;

                StartCoroutine(Move());
            }
        }


        public void StopPatrol()
        {

            StopCoroutine(Move());

            _isPlaying = false;
        }


        private IEnumerator Move()
        {

            while(_isPlaying)
            {

                _elapsedTime += Time.deltaTime;
            
                float diff = _elapsedTime / _interpolationTime;


                transform.position = Vector3.Lerp(_patrolPoints[CurrentIndex].position, NextPoint, diff);


                if (_elapsedTime > _interpolationTime)
                {

                    _elapsedTime = 0f;

                    CurrentIndex++;
                }


                yield return 0;
            }
        }
    }
}
