using System;

namespace UserInteraction.Switchers
{

    public interface ISwitchable
    {

        public event Action<bool> Switched;
    }
}