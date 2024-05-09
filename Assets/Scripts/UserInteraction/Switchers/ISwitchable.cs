namespace UserInteraction.Switchers
{
    public interface ISwitchable
    {
        delegate void SwitchHandler(bool value);
        event SwitchHandler Switched;
    }
}