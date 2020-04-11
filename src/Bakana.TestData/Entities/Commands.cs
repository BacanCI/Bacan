using System.Collections.Generic;
using Bakana.Core;
using Bakana.Core.Entities;

namespace Bakana.TestData.Entities
{
    public static class CommandVariables
    {
        public static CommandVariable DemoArg => new CommandVariable
        {
            Name = "DemoArg",
            Description = "Demo Arg",
            Value = "--demo"
        };
        
        public static CommandVariable HelpArg => new CommandVariable
        {
            Name = "HelpArg",
            Description = "Help Arg",
            Value = "--help"
        };

        public static CommandVariable OverrideArg => new CommandVariable
        {
            Name = "OverrideArg",
            Description = "Override Arg",
            Value = "--override"
        };

        public static CommandVariable OutArg => new CommandVariable
        {
            Name = "OutArg",
            Description = "Out Arg",
            Value = "--out"
        };

        public static CommandVariable ConnectionString => new CommandVariable
        {
            Name = "ConnectionString",
            Description = "Connection String",
            Value = "localhost:8000"
        };
    }

    public static class CommandOptions
    {
        public static CommandOption Optional1 => new CommandOption
        {
            Name = "CMDOPT1",
            Description = "Optional1",
            Value = "CMDOPT1VAL"
        };

        public static CommandOption Optional2 => new CommandOption
        {
            Name = "CMDOPT2",
            Description = "Optional2",
            Value = "CMDOPT2VAL"
        };

        public static CommandOption Debug => new CommandOption
        {
            Name = "Debug",
            Description = "Debug Mode",
            Value = "True"
        };

        public static CommandOption Production => new CommandOption
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
            State = CommandState.Running,
            Variables = new List<CommandVariable>
            {
                CommandVariables.DemoArg,
                CommandVariables.HelpArg,
            },
            Options = new List<CommandOption>
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
            Options = new List<CommandOption>
            {
                CommandOptions.Production
            }
        };

        public static Command Deploy => new Command
        {
            Name = "DeployCmd",
            Description = "Deploy Command",
            Run = "runner -deploy",
            Variables = new List<CommandVariable>
            {
                CommandVariables.ConnectionString
            }
        };
        
        public static Command Test => new Command
        {
            Name = "TestCmd",
            Description = "Test Command",
            Run = "runner -test",
            Variables = new List<CommandVariable>
            {
                CommandVariables.OverrideArg,
                CommandVariables.OutArg
            },
            Options = new List<CommandOption>
            {
                CommandOptions.Debug
            }
        };
    }
}
