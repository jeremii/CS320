using System;
namespace SMP.Service.Tests.Base
{
    public abstract class BaseTests : IDisposable
    {
        protected string ServiceAddress = "http://localhost:40001/";
        protected string RootAddress = String.Empty;


        public virtual void Dispose()
        {
        }
    }
}
