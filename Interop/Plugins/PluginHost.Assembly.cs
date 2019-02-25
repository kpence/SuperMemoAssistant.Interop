﻿#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the 
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Created On:   2019/02/22 19:04
// Modified On:  2019/02/24 21:21
// Modified By:  Alexis

#endregion




using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using SuperMemoAssistant.Interop.SuperMemo;

namespace SuperMemoAssistant.Interop.Plugins
{
  public partial class PluginHost
  {
    #region Constants & Statics

    private static string PluginInterfaceFullName { get; } = typeof(ISMAPlugin).FullName;
    private static HashSet<Type> SMAInterfaceTypes { get; } = new HashSet<Type>
    {
      typeof(ISuperMemoAssistant)
      // Insert subsequent versions here
    };
    private static HashSet<Type> PluginMgrInterfaceTypes { get; } = new HashSet<Type>
    {
      typeof(ISMAPluginManager)
      // Insert subsequent versions here
    };

    #endregion




    #region Methods

    private ISMAPlugin LoadAssembliesAndCreatePluginInstance(
      IEnumerable<string> dependenciesAssembliesPaths,
      IEnumerable<string> pluginAssembliesPaths)
    {
      foreach (var assemblyPath in dependenciesAssembliesPaths)
        Assembly.LoadFrom(assemblyPath);

      return CreatePluginInstance(pluginAssembliesPaths.Select(Assembly.LoadFrom));
    }

    private ISMAPlugin CreatePluginInstance(IEnumerable<Assembly> pluginAssemblies)
    {
      foreach (var pluginAssembly in pluginAssemblies)
      {
        Type pluginType = FindPluginType(pluginAssembly);

        if (pluginType == null)
          continue;

        return (ISMAPlugin)Activator.CreateInstance(pluginType);
      }

      return null;
    }

    private Type FindPluginType(Assembly pluginAssembly)
    {
      var exportedTypes = pluginAssembly.GetExportedTypes();

      return exportedTypes.FirstOrDefault(t => t.IsAbstract == false && t.GetInterface(PluginInterfaceFullName) != null);
    }

    public bool InjectPropertyDependencies(ISMAPlugin          plugin,
                                           ISuperMemoAssistant sma,
                                           ISMAPluginManager   pluginMgr,
                                           Guid                sessionGuid)
    {
      bool smaSet  = false;
      bool mgrSet  = false;
      bool guidSet = false;
      var  type    = plugin.GetType();

      while (type != null && type != typeof(ISMAPlugin))
      {
        var props = type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

        foreach (var prop in props)
          if (SMAInterfaceTypes.Contains(prop.PropertyType))
          {
            prop.SetValue(plugin, sma /*Convert.ChangeType(sma, prop.PropertyType)*/);
            smaSet = true;
          }

          else if (PluginMgrInterfaceTypes.Contains(prop.PropertyType))
          {
            prop.SetValue(plugin, pluginMgr /*Convert.ChangeType(pluginMgr, prop.PropertyType)*/);
            mgrSet = true;
          }

          else if (prop.PropertyType == typeof(Guid))
          {
            prop.SetValue(plugin, sessionGuid);
            guidSet = true;
          }

        type = type.BaseType;
      }

      return smaSet && mgrSet && guidSet;
    }

    #endregion
  }
}
