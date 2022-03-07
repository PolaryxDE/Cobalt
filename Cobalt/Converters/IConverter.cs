using System.Reflection;

namespace Cobalt.Converters
{
    /// <summary>
    /// The converter allows the custom converting of specific types given a string. Its used in the parameter
    /// type converting when parsing the parameters of a command.
    /// </summary>
    public interface IConverter
    {
        /// <summary>
        /// Checks if the given parameter should be handled by this converter.
        /// </summary>
        /// <param name="parameter">The reflection object of the parameter.</param>
        /// <returns>True, if this converter should handle the converting.</returns>
        bool ShouldHandle(ParameterInfo parameter);

        /// <summary>
        /// Converts the given string value to the object of the respective type.
        /// </summary>
        /// <param name="val">The value which should be parsed.</param>
        /// <param name="parameter">The wanted parameter.</param>
        /// <returns>The converted object.</returns>
        object Convert(string val, ParameterInfo parameter);
    }
}