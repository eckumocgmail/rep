using System.Collections.Concurrent;
using System;
using System.Linq;

namespace Console_InputApplication.ConsolePrograms
{


    /// <summary>
    /// 
    /// </summary>
    public class RoutingProgram : IDisposable
    {

        private static ConcurrentBag<string> State = new ConcurrentBag<string>();
        private string StateName { get; set; }


        public RoutingProgram() { }
        public RoutingProgram(string stateName)
        {
            BeginState(StateName = stateName);
        }


        /// <summary>
        /// route="{route}/{name}"
        /// </summary>        
        private static void BeginState(string name)
        {
            State.Add(name);
            string Title = "/";
            foreach (var next in State)
                Title += next + "/";
            Console.Title = Title;
        }


        /// <summary>
        /// route="{route}/{name}"
        /// </summary>     
        private static void EndState(string name)
        {
            if (State.Last() != name)
                throw new Exception("");
            State.Append(name);
            string Title = "/";
            foreach (var next in State)
                Title += next + "/";
            Console.Title = Title;
        }

        public void Dispose() => EndState(StateName);

        internal static void Run(string v)
        {
            throw new NotImplementedException();
        }
    }
}
