using System;
using System.Collections.Generic;
using System.Reflection;

namespace Cobalt.Converters
{
    internal static class ParameterConverter
    {
        internal static List<IConverter> CustomConverters { get; } = new List<IConverter>();

        public static object[] Convert(ParameterInfo[] parameters, string[] argStrings)
        {
            object[] args = new object[parameters.Length];
            for (int i = 0; i < args.Length; i++)
            {
                ParameterInfo parameter = parameters[i];
                bool isGreedy = parameter.GetCustomAttribute<Param>()?.IsGreedy ?? false;
                if (parameter.IsOptional && i >= argStrings.Length)
                {
                    args[i] = Type.Missing;
                    continue;
                }

                if (isGreedy)
                {
                    string s = argStrings[i];
                    int j = i;
                    while (++j < argStrings.Length)
                    {
                        s += " " + argStrings[j];
                    }

                    args[i] = s;
                    break;
                }

                if (EnumConverter.IsHandlerFor(parameter))
                {
                    args[i] = EnumConverter.Convert(argStrings[i], parameter.ParameterType);
                }
                else
                {
                    var converter = GetCustomConverter(parameter);
                    if (converter != null)
                    {
                        args[i] = converter.Convert(argStrings[i], parameter);
                    }
                    else
                    {
                        args[i] = BasicConverter.Convert(argStrings[i], parameter.ParameterType);
                    }
                }
            }

            return args;
        }

        private static IConverter GetCustomConverter(ParameterInfo parameter)
        {
            return CustomConverters.SelectFirst(x => x.ShouldHandle(parameter));
        }
    }
}