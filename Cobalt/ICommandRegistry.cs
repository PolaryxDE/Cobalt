using System.Collections.Generic;
using System.Threading.Tasks;
using Cobalt.Converters;

namespace Cobalt
{
    /// <summary>
    /// The command registry manages all commands (wow what a miracle).
    /// </summary>
    public interface ICommandRegistry
    {
        /// <summary>
        /// Adds the given converter to the list of custom <see cref="IConverter"/>s.
        /// </summary>
        /// <param name="converter">The converter to be added to the parameter converting process.</param>
        void AddConverter(IConverter converter);
        
        /// <summary>
        /// Scans through the object and registers all <see cref="Command"/>s. Only non-static methods will be registered.
        /// </summary>
        /// <param name="obj">The object which owns the commands.</param>
        void Scan(object obj);

        /// <summary>
        /// Scans through the class and registers all <see cref="Command"/>s. Only static methods will be registered.
        /// </summary>
        /// <typeparam name="T">The class which owns the commands.</typeparam>
        void Scan<T>();

        /// <summary>
        /// Returns all names of the available commands.
        /// </summary>
        IEnumerable<string> GetCommandNames();

        /// <summary>
        /// Returns all usages of the available commands.
        /// </summary>
        IEnumerable<string> GetCommandUsages();

        /// <summary>
        /// Processes the given line and tries to find and execute a matching <see cref="Command"/> handler.
        /// </summary>
        /// <param name="line">The line to process.</param>
        Task<bool> ProcessAsync(string line);
    }
}