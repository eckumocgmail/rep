using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_DataConnector.DataModule.DataApi
{


    public class SuperType :
            Dictionary<string, IDictionary<string, string>>,
            IDictionary<string, IDictionary<string, string>>
    {
        public IDictionary<string, string> GetTypeAttributes() => this.GetType().GetAttributes();
        public IDictionary<string, IDictionary<string, string>> GetPropertiesAttributes() =>
            new Dictionary<string, IDictionary<string, string>>(
                this.GetType().GetPropertyNames().Select(
                    name => new KeyValuePair<string, IDictionary<string, string>>(
                        name, this.GetType().GetPropertyAttributes(name)
                    )
                )
            );
        public IDictionary<string, IDictionary<string, string>> GetMethodAttributes()
            => new Dictionary<string, IDictionary<string, string>>(
                this.GetType().GetOwnMethodNames().Select(
                    name => new KeyValuePair<string, IDictionary<string, string>>(
                        name, this.GetType().GetMethodAttributes(name)
                    )
                )
            );
    }
}
