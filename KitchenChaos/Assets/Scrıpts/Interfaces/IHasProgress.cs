using System;

public interface IHasProgress
{
    public event EventHandler<OnProgressChangedEventsArgs> OnProgressChanged;

    public class OnProgressChangedEventsArgs : EventArgs
    {
        public float progressNormalized;
    }

}
