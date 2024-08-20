﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using MigrationTools._EngineV1.Configuration;
using MigrationTools.ConsoleDataGenerator.ReferenceData;
using MigrationTools.Options;
using Newtonsoft.Json.Linq;
using MigrationTools;
using System.Configuration;
using Newtonsoft.Json;
using MigrationTools.Tools.Infrastructure;
using System.Security.AccessControl;
using Microsoft.Extensions.Options;

namespace MigrationTools.ConsoleDataGenerator
{
    public class ClassDataLoader
    {
        private DataSerialization saveData;
        private static CodeDocumentation codeDocs = new CodeDocumentation("../../../../../docs/Reference/Generated/");
        private static CodeFileFinder codeFinder = new CodeFileFinder("../../../../../src/");
        private IConfiguration configuration;
        public ClassDataLoader(DataSerialization saveData, Microsoft.Extensions.Configuration.IConfiguration configuration)
        {

            this.saveData = saveData;
            this.configuration = configuration;
        }

        public List<ClassData> GetClassData(List<Type> targetTypes, List<Type> allTypes, Type type, string apiVersion, string dataTypeName, bool findConfig = true, string configEnd = "Options")
        {
            Console.WriteLine();
            Console.WriteLine($"ClassDataLoader::populateClassData:: {dataTypeName}");
            List<ClassData> data = new List<ClassData>();
            var founds = targetTypes.Where(t => type.IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface && t.IsPublic).OrderBy(t => t.Name).ToList();
            Console.WriteLine($"ClassDataLoader::populateClassData:: ----------- Found {founds.Count}");
            // Each File
            foreach (var item in founds)
            {
                Console.WriteLine($"ClassDataLoader::populateClassData::-PROCESS {item.Name}");
                data.Add(CreateClassData(targetTypes, allTypes, type, apiVersion, dataTypeName, item, findConfig, configEnd));
            }
            Console.WriteLine("ClassDataLoader::populateClassData:: -----------");
            return data;
        }

        public List<ClassData> GetClassDataFromOptions<TOptionsInterface>(List<Type> allTypes, string dataTypeName)
            where TOptionsInterface : IOptions
        {
            Console.WriteLine();
            Console.WriteLine($"ClassDataLoader::GetOptionsData:: {dataTypeName}");
            List<ClassData> data = new List<ClassData>();
            var founds = allTypes.Where(t => typeof(TOptionsInterface).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface && t.IsPublic).OrderBy(t => t.Name).ToList();
            Console.WriteLine($"ClassDataLoader::GetOptionsData:: ----------- Found {founds.Count}");
            // Each File
            foreach (var item in founds)
            {
                Console.WriteLine($"ClassDataLoader::CreateClassDataFromOptions::-PROCESS {item.Name}");
                var itemData = CreateClassDataFromOptions<TOptionsInterface>(allTypes, dataTypeName, item);
                if (itemData != null)
                {
                    data.Add(itemData);
                }
                else
                {
                    Console.WriteLine($"BOOM::CreateClassDataFromOptions");
                }

            }
            Console.WriteLine("ClassDataLoader::GetOptionsData:: -----------");
            return data;
        }

        private ClassData CreateClassDataFromOptions<TOptionsInterface>(List<Type> allTypes, string dataTypeName, Type optionInFocus)
            where TOptionsInterface : IOptions
        {
            TOptionsInterface instanceOfOption = (TOptionsInterface)Activator.CreateInstance(optionInFocus);
            string targetOfOption = instanceOfOption.ConfigurationOptionFor;
            var typeOftargetOfOption = allTypes.Where(t => t.Name == targetOfOption && !t.IsAbstract && !t.IsInterface).SingleOrDefault();
            if (typeOftargetOfOption == null)
            {
                Console.WriteLine($"ClassDataLoader::CreateClassDataFromOptions:: {optionInFocus.Name} - {targetOfOption} not found");
                return null;
            }
            ClassData data = new ClassData();
            data.ClassName = typeOftargetOfOption.Name;
            data.ClassFile = codeFinder.FindCodeFile(typeOftargetOfOption);
            data.TypeName = dataTypeName;
            data.Description = codeDocs.GetTypeData(typeOftargetOfOption);
            data.Status = codeDocs.GetTypeData(typeOftargetOfOption, "status");
            data.ProcessingTarget = codeDocs.GetTypeData(typeOftargetOfOption, "processingtarget");


            if (optionInFocus != null)
            {
                data.OptionsClassFullName = optionInFocus.FullName;
                data.OptionsClassName = optionInFocus.Name;
                data.OptionsClassFile = codeFinder.FindCodeFile(optionInFocus);
                var ConfigurationSectionName = (string)optionInFocus.GetProperty("ConfigurationSectionName")?.GetValue(instanceOfOption);
                var ConfigurationSectionListName = (string)optionInFocus.GetProperty("ConfigurationSectionListName")?.GetValue(instanceOfOption);
                if (instanceOfOption is ToolOptions)
                {
                    dynamic man = OptionsManager.GetOptionsManager(optionInFocus, "appsettings.json", ConfigurationSectionName, optionInFocus.Name, "sectionListPath", "objectTypeFieldName");

                    var o= man.LoadFromSectionPath();

                    Console.WriteLine("Is Tool Options");
                }
                if (!string.IsNullOrEmpty(ConfigurationSectionName))
                {
                    Console.WriteLine("Processing as ConfigurationSectionName");
                    var section = configuration.GetSection(ConfigurationSectionName);
                    section.Bind(instanceOfOption);
                    data.ConfigurationSamples.Add(new ConfigurationSample() { Name = "defaults", SampleFor = data.OptionsClassFullName, Code = ConvertSectionWithPathToJson(configuration, section).Trim() });
                }

                Console.WriteLine("targetItem");
                JObject joptions = (JObject)JToken.FromObject(instanceOfOption);
                data.Options = populateOptions(instanceOfOption, joptions);
                data.ConfigurationSamples.Add(new ConfigurationSample() { Name = "Classic", SampleFor = data.OptionsClassFullName, Code = saveData.SeraliseDataToJson(instanceOfOption).Trim() });
            }
            else
            {

            }
            return data;
        }


        private ClassData CreateClassData(List<Type> targetTypes, List<Type> allTypes, Type type, string apiVersion, string dataTypeName, Type item, bool findConfig = true, string configEnd = "Options")
        {
            Type typeOption = item;
            string objectName = item.Name;
            ClassData data = new ClassData();
            data.ClassName = item.Name;
            data.ClassFile = codeFinder.FindCodeFile(item);
            data.TypeName = dataTypeName;
            data.Architecture = apiVersion;
            data.Description = codeDocs.GetTypeData(item);
            data.Status = codeDocs.GetTypeData(item, "status");
            data.ProcessingTarget = codeDocs.GetTypeData(item, "processingtarget");
            if (findConfig)
            {
                objectName = objectName.Replace("Context", "");
                typeOption = allTypes.Where(t => t.Name == $"{objectName}{configEnd}" && !t.IsAbstract && !t.IsInterface).SingleOrDefault();

            }
            else
            {
                data.OptionsClassName = "";
                data.OptionsClassFullName = "";
                Console.WriteLine("No config");
            }

            if (typeOption != null)
            {
                data.OptionsClassFullName = typeOption.FullName;
                data.OptionsClassName = typeOption.Name;
                data.OptionsClassFile = codeFinder.FindCodeFile(typeOption);
                object targetItem = null;
                var instanceOfOptions = Activator.CreateInstance(typeOption);
                var ConfigurationSectionName = (string)typeOption.GetProperty("ConfigurationSectionName")?.GetValue(instanceOfOptions);
                if (!string.IsNullOrEmpty(ConfigurationSectionName))
                {
                    Console.WriteLine("Processing as ConfigurationSectionName");
                    var section = configuration.GetSection(ConfigurationSectionName);
                    targetItem = (IOptions)instanceOfOptions;
                    section.Bind(targetItem);
                    data.ConfigurationSamples.Add(new ConfigurationSample() { Name = "defaults", SampleFor = data.OptionsClassFullName, Code = ConvertSectionWithPathToJson(configuration, section).Trim() });
                }
                if (typeOption.GetInterfaces().Contains(typeof(IOptions)))
                {
                    Console.WriteLine("Processing as IOptions");
                    var options = (IOptions)instanceOfOptions;
                    targetItem = options;
                }
                if (targetItem != null)
                {
                    Console.WriteLine("targetItem");
                    JObject joptions = (JObject)JToken.FromObject(targetItem);
                    data.Options = populateOptions(targetItem, joptions);
                    data.ConfigurationSamples.Add(new ConfigurationSample() { Name = "Classic", SampleFor = data.OptionsClassFullName, Code = saveData.SeraliseDataToJson(targetItem).Trim() });
                }

            }
            else
            {

            }
            return data;
        }


        private List<OptionsItem> populateOptions(object item, JObject joptions)
        {
            List<OptionsItem> options = new List<OptionsItem>();
            if (!(joptions is null))
            {
                var jpropertys = joptions.Properties().OrderBy(t => t.Name);
                foreach (JProperty jproperty in jpropertys)
                {
                    OptionsItem optionsItem = new OptionsItem();
                    optionsItem.ParameterName = jproperty.Name;
                    optionsItem.Type = codeDocs.GetPropertyType(item, jproperty);
                    optionsItem.Description = codeDocs.GetPropertyData(item, joptions, jproperty, "summary");
                    optionsItem.DefaultValue = codeDocs.GetPropertyData(item, joptions, jproperty, "default");
                    options.Add(optionsItem);
                }
            }
            return options;
        }

        static JObject ConvertSectionToJson2(IConfigurationSection section)
        {
            var jObject = new JObject();

            foreach (var child in section.GetChildren())
            {
                if (child.GetChildren().Any()) // Check if the child has its own children
                {
                    jObject[child.Key] = ConvertSectionToJson2(child);
                }
                else
                {
                    jObject[child.Key] = child.Value;
                }
            }

            return jObject;
        }

        static string ConvertSectionWithPathToJson(IConfiguration configuration, IConfigurationSection section)
        {
            var pathSegments = section.Path.Split(':');
            JObject root = new JObject();
            JObject currentObject = root;

            // Walk down the path from the root to the target section
            for (int i = 0; i < pathSegments.Length; i++)
            {
                string key = pathSegments[i];
                IConfigurationSection currentSection = configuration.GetSection(string.Join(':', pathSegments, 0, i + 1));

                JObject parentObject = new JObject();

                if (i == pathSegments.Length - 1)
                {
                    // We are at the target section, so only serialize this section
                    foreach (var child in currentSection.GetChildren())
                    {
                        if (child.Value != null)
                        {
                            parentObject[child.Key] = child.Value;
                        }
                        else
                        {
                            parentObject[child.Key] = ConvertSectionToJson(child);
                        }
                    }
                }

                currentObject[key] = parentObject;
                currentObject = parentObject;
            }

            return root.ToString(Formatting.Indented);
        }

        static JObject ConvertSectionToJson(IConfigurationSection section)
        {
            var jObject = new JObject();

            foreach (var child in section.GetChildren())
            {
                if (child.Value != null)
                {
                    jObject[child.Key] = child.Value;
                }
                else
                {
                    jObject[child.Key] = ConvertSectionToJson(child);
                }
            }

            return jObject;
        }

    }
}
