using Microsoft.Extensions.Hosting;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Console_InputApplication.ProgramModel
{


    /// <summary>
    /// Хоть и называется ProgramCommander всё равно реализует управление паралелльными процессами
    /// поэтому может стать BackGroundService`ом
    /// </summary>

    public abstract class BaseCommander
    {
        public abstract string ListCommands();
        public abstract Task<string> RunCommand(string command);
    }

    /*
        public abstract string AddCommand(string name, Action todo);
        public abstract string AddCommand(string name, Action todo, Dictionary<string, MyParameterDeclarationModel> parameters);
        public abstract string TakeCommand();
        public abstract Task StartAsync(CancellationToken cancellationToken);
        public abstract Task StopAsync(CancellationToken cancellationToken);
        public abstract void Dispose();
    */

}
