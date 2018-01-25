namespace Zebble.Device
{
    using System;
    using System.Threading.Tasks;

    public abstract partial class Sensor<TValue>
    {
        public const SenrorDelay DEFAULT_DELAY = SenrorDelay.Game;
        public bool IsActive { get; protected set; }
        public readonly AsyncEvent<TValue> Changed = new AsyncEvent<TValue>();

        protected void OnChanged(TValue value) => Thread.Pool.Run(() => Changed.Raise(value));

        public async Task Start(SenrorDelay delay = SenrorDelay.Game, OnError errorAction = OnError.Toast)
        {
            try
            {
                DoStart(delay);
                IsActive = true;
            }
            catch (Exception ex) { await errorAction.Apply(ex); }
        }

        public void Stop(bool removeListeners)
        {
            if (removeListeners) Changed.ClearHandlers();
            Stop();
        }
    }
}
