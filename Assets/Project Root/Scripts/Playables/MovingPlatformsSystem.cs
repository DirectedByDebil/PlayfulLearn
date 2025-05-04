using System.Collections.Generic;

namespace Playables
{
    public sealed class MovingPlatformsSystem
    {

        private readonly IEnumerable<Patrol> _movingPlatform;


        public MovingPlatformsSystem(IEnumerable<Patrol> movingPlatforms)
        {

            _movingPlatform = movingPlatforms;
        }


        public void StartPlatformMovement(float platformInterpolationTime)
        {

            foreach (Patrol platform in _movingPlatform)
            {

                platform.StartPatrol(platformInterpolationTime);
            }
        }


        public void StopPlatformMovement()
        {

            foreach (Patrol platform in _movingPlatform)
            {

                platform.StopPatrol();
            }
        }
    }
}
