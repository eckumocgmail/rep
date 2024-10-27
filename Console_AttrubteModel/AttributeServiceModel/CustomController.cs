using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_AttributeModel.AttributeServiceModel
{
    [ApiController]
    [Route("/api/[controller]/[action]")]
    public class CustomController : Controller, ICustomService
    {
        private readonly CustomService customService;



        public CustomController(CustomService customService)
        {
            this.customService = customService;
        }

        public Dictionary<string, string> GetAttributes(string typeName)
        {
            return ((ICustomService)customService).GetAttributes(typeName);
        }
         

        public Dictionary<string, Dictionary<string, string>> GetMembersAttributes(string typeName)
        {
            return ((ICustomService)customService).GetMembersAttributes(typeName);
        }

        public Dictionary<string, string> GetMethodAttributes(string typeName, string method)
        {
            return ((ICustomService)customService).GetMethodAttributes(typeName, method);
        }

        public Dictionary<string, string> GetParameterAttributes(string typeName, string method, string parameter)
        {
            return ((ICustomService)customService).GetParameterAttributes(typeName, method, parameter);
        }

        public Dictionary<string, string> GetPropertyAttributes(string typeName, string property)
        {
            return ((ICustomService)customService).GetPropertyAttributes(typeName, property);
        }
    }
}
