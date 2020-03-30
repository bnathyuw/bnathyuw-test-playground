using System;

namespace Bnathyuw.TestPlayground.App.Services
{
    public interface IClock
    {
        DateTime Now();
    }

    public class Clock : IClock
    {
        public DateTime Now() => DateTime.Now;
    }
}