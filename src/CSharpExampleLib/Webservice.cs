using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharpExampleLib
{
    public class ServicePayload
    {
        public ServicePayload(int index, string data)
        {
            this.Index = index;
            this.Data = data;
        }

        public int Index { get; private set; }

        public string Data { get; private set; }
    }

    /// <summary>
    /// Lets make a really unfriendly F# API using lots of method overloading.
    /// </summary>
    public class WebserviceUsingOverloads
    {
        public readonly IEnumerable<ServicePayload> servicePayloads =
            new[]
            {
                new ServicePayload(1, "TEST1"),
                new ServicePayload(2, "TEST2"),
                new ServicePayload(3, "TEST3"),
            };
        
        public IEnumerable<ServicePayload> GetAll()
        {
            return this.servicePayloads;
        }

        public IEnumerable<ServicePayload> Get(string name)
        {
            return this.servicePayloads.Where(x => x.Data == name);
        }

        public IEnumerable<ServicePayload> Get(int index)
        {
            return this.servicePayloads.Where(x => x.Index == index);
        }

        public IEnumerable<ServicePayload> Get(Func<string, bool> filter)
        {
            return this.servicePayloads.Where(x => filter(x.Data));
        }
        /*
        public IEnumerable<ServicePayload> Get(Func<int, bool> filter)
        {
            return this.servicePayloads.Where(x => filter(x.Index));
        }*/
    }

    /// <summary>
    /// No overloads make it easier to consume from F#
    /// </summary>
    public class NiceWebservice
    {
        public readonly IEnumerable<ServicePayload> servicePayloads =
            new[]
            {
                new ServicePayload(1, "TEST1"),
                new ServicePayload(2, "TEST2"),
                new ServicePayload(3, "TEST3"),
            };

        public IEnumerable<ServicePayload> GetAll()
        {
            return this.servicePayloads;
        }

        public IEnumerable<ServicePayload> GetByName(string name)
        {
            return this.servicePayloads.Where(x => x.Data == name);
        }

        public IEnumerable<ServicePayload> GetByIndex(int index)
        {
            return this.servicePayloads.Where(x => x.Index == index);
        }

        public IEnumerable<ServicePayload> GetByStringFilter(Func<string, bool> filter)
        {
            return this.servicePayloads.Where(x => filter(x.Data));
        }

        public IEnumerable<ServicePayload> GetByIndexFilter(Func<int, bool> filter)
        {
            return this.servicePayloads.Where(x => filter(x.Index));
        }
    }
}
