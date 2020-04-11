using System.Collections.Generic;
using Bakana.DomainModels;
using Command = Bakana.DomainModels.Command;

namespace Bakana.TestData.DomainModels
{
    public static class CommandVariables
    {
        public static Variable DemoArg => new Variable
        {
            Name = "DemoArg",
            Description = "Demo Arg",
            Value = "--demo"
        };
        
        public static Variable HelpArg => new Variable
        {
            Name = "HelpArg",
            Description = "Help Arg",
            Value = "--help"
        };

        public static Variable OverrideArg => new Variable
        {
            Name = "OverrideArg",
            Description = "Override Arg",
            Value = "--override"
        };

        public static Variable OutArg => new Variable
        {
            Name = "OutArg",
            Description = "Out Arg",
            Value = "--out"
        };

        public static Variable ConnectionString => new Variable
        {
            Name = "ConnectionString",
            Description = "Connection String",
            Value = "localhost:8000"
        };
    }

    public static class CommandOptions
    {
        public static Option Optional1 => new Option
        {
            Name = "CMDOPT1",
            Description = "Optional1",
            Value = "CMDOPT1VAL"
        };

        public static Option Optional2 => new Option
        {
            Name = "CMDOPT2",
            Description = "Optional2",
            Value = "CMDOPT2VAL"
        };

        public static Option Debug => new Option
        {
            Name = "Debug",
            Description = "Debug Mode",
            Value = "True"
        };

        public static Option Production => new Option
        {
            Name = "Production",
            Description = "Production Mode",
            Value = "True"
        };
    }

    public static class Commands
    {
        public static Command DotNetRestore => new Command
        {
            Name = "RestoreCmd",
            Description = "Dot Net Restore",
            Run = "dot net restore",
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
            Name = "BuildCmd",
            Description = "Dot Net Build",
            Run = "dot net build",
            Options = new List<Option>
            {
                CommandOptions.Production
            }
        };

        public static Command Deploy => new Command
        {
            Name = "DeployCmd",
            Description = "Deploy Command",
            Run = "runner -deploy",
            Variables = new List<Variable>
            {
                CommandVariables.ConnectionString
            }
        };
        
        public static Command Test => new Command
        {
            Name = "TestCmd",
            Description = "Test Command",
            Run = "runner -test",
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
