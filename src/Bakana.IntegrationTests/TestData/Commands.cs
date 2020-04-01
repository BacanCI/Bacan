using System.Collections.Generic;
using Bakana.Core;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests.TestData
{
    public static class CommandVariables
    {
        public static CommandVariable DemoArg => new CommandVariable
        {
            Description = "Demo Arg",
            Name = "DemoArg",
            Value = "--demo"
        };
        
        public static CommandVariable HelpArg => new CommandVariable
        {
            Description = "Help Arg",
            Name = "HelpArg",
            Value = "--help"
        };

        public static CommandVariable OverrideArg => new CommandVariable
        {
            Description = "Override Arg",
            Name = "OverrideArg",
            Value = "--override"
        };

        public static CommandVariable OutArg => new CommandVariable
        {
            Description = "Out Arg",
            Name = "OutArg",
            Value = "--out"
        };

        public static CommandVariable ConnectionString => new CommandVariable
        {
            Description = "Connection String",
            Name = "ConnectionString",
            Value = "localhost:8000"
        };
    }

    public static class CommandOptions
    {
        public static CommandOption Optional1 => new CommandOption
        {
            Description = "Optional1",
            Name = "CMDOPT1",
            Value = "CMDOPT1VAL"
        };

        public static CommandOption Optional2 => new CommandOption
        {
            Description = "Optional2",
            Name = "CMDOPT2",
            Value = "CMDOPT2VAL"
        };

        public static CommandOption Debug => new CommandOption
        {
            Description = "Debug Mode",
            Name = "Debug",
            Value = "True"
        };

        public static CommandOption Production => new CommandOption
        {
            Description = "Production Mode",
            Name = "Production",
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
            CommandId = "BuildCmd",
            Description = "Dot Net Build",
            Item = "dot net build",
            Options = new List<CommandOption>
            {
                CommandOptions.Production
            }
        };

        public static Command Deploy => new Command
        {
            CommandId = "DeployCmd",
            Description = "Deploy Command",
            Item = "runner -deploy",
            Variables = new List<CommandVariable>
            {
                CommandVariables.ConnectionString
            }
        };
        
        public static Command Test => new Command
        {
            CommandId = "TestCmd",
            Description = "Test Command",
            Item = "runner -test",
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
