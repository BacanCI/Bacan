using System.Collections.Generic;
using Bakana.ServiceModels;

namespace Bakana.TestData.ServiceModels
{
    public static class CommandVariables
    {
        public static Variable DemoArg => new Variable
        {
            VariableId = "DemoArg",
            Description = "Demo Arg",
            Value = "--demo"
        };
        
        public static Variable HelpArg => new Variable
        {
            VariableId = "HelpArg",
            Description = "Help Arg",
            Value = "--help"
        };

        public static Variable OverrideArg => new Variable
        {
            VariableId = "OverrideArg",
            Description = "Override Arg",
            Value = "--override"
        };

        public static Variable OutArg => new Variable
        {
            VariableId = "OutArg",
            Description = "Out Arg",
            Value = "--out"
        };

        public static Variable ConnectionString => new Variable
        {
            VariableId = "ConnectionString",
            Description = "Connection String",
            Value = "localhost:8000"
        };
    }

    public static class CommandOptions
    {
        public static Option Optional1 => new Option
        {
            OptionId = "CMDOPT1",
            Description = "Optional1",
            Value = "CMDOPT1VAL"
        };

        public static Option Optional2 => new Option
        {
            OptionId = "CMDOPT2",
            Description = "Optional2",
            Value = "CMDOPT2VAL"
        };

        public static Option Debug => new Option
        {
            OptionId = "Debug",
            Description = "Debug Mode",
            Value = "True"
        };

        public static Option Production => new Option
        {
            OptionId = "Production",
            Description = "Production Mode",
            Value = "True"
        };
    }
    
    public static class Commands
    {
        public static Command DotNetRestore => new Command
        {
            CommandId = "RestoreCmd",
            Description = "Dot Net Restore",
            Item = "dot net restore",
            Variables = new List<Variable>
            {
                CommandVariables.DemoArg,
                CommandVariables.HelpArg,
            },
            Options = new List<Option>
            {
                CommandOptions.Optional1,
                CommandOptions.Optional2
            }
        };

        public static Command DotNetBuild => new Command
        {
            CommandId = "BuildCmd",
            Description = "Dot Net Build",
            Item = "dot net build",
            Options = new List<Option>
            {
                CommandOptions.Production
            }
        };

        public static Command Deploy => new Command
        {
            CommandId = "DeployCmd",
            Description = "Deploy Command",
            Item = "runner -deploy",
            Variables = new List<Variable>
            {
                CommandVariables.ConnectionString
            }
        };
        
        public static Command Test => new Command
        {
            CommandId = "TestCmd",
            Description = "Test Command",
            Item = "runner -test",
            Variables = new List<Variable>
            {
                CommandVariables.OverrideArg,
                CommandVariables.OutArg
            },
            Options = new List<Option>
            {
                CommandOptions.Debug
            }
        };
    }
}
