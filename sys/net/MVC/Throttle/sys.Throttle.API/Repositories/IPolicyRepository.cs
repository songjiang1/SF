using System;
namespace  sys.Throttle.API
{
    public interface IPolicyRepository
    {
        ThrottlePolicy FirstOrDefault(string id);

        void Remove(string id);

        void Save(string id, ThrottlePolicy policy);
    }
}
