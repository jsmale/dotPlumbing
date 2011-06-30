using System;

namespace Plumbing.Utility
{
    public interface IDateTimeService
    {
        DateTime Now();
        DateTime UtcNow();
    }
}