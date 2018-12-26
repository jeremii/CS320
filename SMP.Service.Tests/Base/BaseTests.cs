using System;
using Xunit;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using Newtonsoft.Json;
using SMP.Models.Entities;
using SMP.Models.ViewModels;
using SMP.Service.Tests.Base;
using System.Threading.Tasks;
using SMP.DAL.Initializers;
using System.Text;
using System.Net.Http.Headers;

namespace SMP.Service.Tests.Base
{
    public abstract class BaseTests : IDisposable
    {
        public static string ServiceAddress = "http://localhost:40001/";
        protected string RootAddress = String.Empty;
        protected static string firstName = "Joe";
        protected static string lastName = "Schmoe";
        protected static string firstName2 = "Johnny";
        protected static string lastName2 = "Basic";
        public static string FindIdRoute = $"api/User/FindIdByName/{firstName}/{lastName}";
        public static string FindIdRoute2 = $"api/User/FindIdByName/{firstName2}/{lastName2}";


        public virtual void Dispose()
        {
        }
    }
}
