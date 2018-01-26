namespace Zebble.Device
{
    using Android.Content;
    using Android.Hardware;
    using Android.Runtime;
    using System;

    partial class Sensor<TValue> : Java.Lang.Object, ISensorEventListener
    {
        SensorType Type;
        protected Sensor(SensorType type) { Type = type; }

        static SensorManager sensorManager;
        protected Sensor _sensor;

        protected static SensorManager SensorManager
        {
            get
            {
                if (sensorManager == null)
                    sensorManager = UIRuntime.GetService<SensorManager>(Context.SensorService);

                return sensorManager;
            }
        }

        protected Sensor _Sensor
        {
            get
            {
                if (_sensor != null) return _sensor;

                _sensor = SensorManager?.GetDefaultSensor(Type);

                return _sensor;
            }
        }

        public bool IsAvailable() => _Sensor != null;

        void ISensorEventListener.OnAccuracyChanged(Sensor _, [GeneratedEnum] SensorStatus __) { }

        public abstract void OnSensorChanged(SensorEvent args);

        protected SensorDelay GetDelay(SenrorDelay delay)
        {
            switch (delay)
            {
                case SenrorDelay.Realtime: return SensorDelay.Fastest;
                case SenrorDelay.Game: return SensorDelay.Game;
                case SenrorDelay.UI: return SensorDelay.Ui;
                default: return SensorDelay.Normal;
            }
        }

        void DoStart(SenrorDelay delay)
        {
            if (_Sensor == null) throw new Exception(Type + " is not available on this device.");
            SensorManager.RegisterListener(this, _Sensor, GetDelay(delay));
        }

        public void Stop()
        {
            if (_Sensor == null) return;
            IsActive = false;
            SensorManager.UnregisterListener(this, _Sensor);
        }
    }
}