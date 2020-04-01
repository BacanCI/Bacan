using System.Collections.Generic;
using Bakana.Core;
using Bakana.Core.Entities;

namespace Bakana.IntegrationTests.TestData
{
    public static class CommandVariables
    {
        public static CommandVariable DemoArg => new CommandVariable
        {
            VariableId = "DemoArg",
            Description = "Demo Arg",
            Value = "--demo"
        };
        
        public static CommandVariable HelpArg => new CommandVariable
        {
            VariableId = "HelpArg",
            Description = "Help Arg",
            Value = "--help"
        };

        public static CommandVariable OverrideArg => new CommandVariable
        {
            VariableId = "OverrideArg",
            Description = "Override Arg",
            Value = "--override"
        };

        public static CommandVariable OutArg => new CommandVariable
        {
            VariableId = "OutArg",
            Description = "Out Arg",
            Value = "--out"
        };

        public static CommandVariable ConnectionString => new CommandVariable
        {
            VariableId = "ConnectionString",
            Description = "Connection String",
            Value = "localhost:8000"
        };
    }

    public static class CommandOptions
    {
        public static CommandOption Optional1 => new CommandOption
        {
            OptionId = "CMDOPT1",
            Description = "Optional1",
            Value = "CMDOPT1VAL"
        };

        public static CommandOption Optional2 => new CommandOption
        {
            OptionId = "CMDOPT2",
            Description = "Optional2",
            Value = "CMDOPT2VAL"
        };

        public static CommandOption Debug => new CommandOption
        {
            OptionId = "Debug",
            Description = "Debug Mode",
            Value = "True"
        };

        public static CommandOption Production => new CommandOption
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
