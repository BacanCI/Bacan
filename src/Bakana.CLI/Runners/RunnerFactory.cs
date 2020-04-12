using System;

namespace Bakana.Runners
{
    public class RunnerFactory
    {
        public static IRunner Create(Mode mode)
        {
            return mode switch
            {
                Mode.All => (IRunner) new All(),
                Mode.Client => new Client(),
                Mode.Loader => new Loader(),
                Mode.Producer => new Producer(),
                Mode.Tracker => new Tracker(),
                Mode.Worker => new Worker(),
                _ => throw new ArgumentException("Unknown value for Mode")
            };
        }
    }
}