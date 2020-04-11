using Bakana.ServiceModels.Commands;
using Bakana.TestData.DomainModels;
using ServiceStack;

namespace Bakana.TestData.ServiceModels
{
    public static class CreateCommandVariables
    {
        public static CreateCommandVariableRequest Extract = CommandVariables.DemoArg.ConvertTo<CreateCommandVariableRequest>();
        public static CreateCommandVariableRequest HelpArg = CommandVariables.HelpArg.ConvertTo<CreateCommandVariableRequest>();
        public static CreateCommandVariableRequest OverrideArg = CommandVariables.OverrideArg.ConvertTo<CreateCommandVariableRequest>();
        public static CreateCommandVariableRequest OutArg = CommandVariables.OutArg.ConvertTo<CreateCommandVariableRequest>();
        public static CreateCommandVariableRequest ConnectionString = CommandVariables.ConnectionString.ConvertTo<CreateCommandVariableRequest>();
    }

    public static class UpdateCommandVariables
    {
        public static UpdateCommandVariableRequest Extract = CommandVariables.DemoArg.ConvertTo<UpdateCommandVariableRequest>();
        public static UpdateCommandVariableRequest HelpArg = CommandVariables.HelpArg.ConvertTo<UpdateCommandVariableRequest>();
        public static UpdateCommandVariableRequest OverrideArg = CommandVariables.OverrideArg.ConvertTo<UpdateCommandVariableRequest>();
        public static UpdateCommandVariableRequest OutArg = CommandVariables.OutArg.ConvertTo<UpdateCommandVariableRequest>();
        public static UpdateCommandVariableRequest ConnectionString = CommandVariables.ConnectionString.ConvertTo<UpdateCommandVariableRequest>();
    }

    public static class CreateCommandOptions
    {
        public static CreateCommandOptionRequest Optional1 =
            CommandOptions.Optional1.ConvertTo<CreateCommandOptionRequest>();
        public static CreateCommandOptionRequest Optional2 =
            CommandOptions.Optional2.ConvertTo<CreateCommandOptionRequest>();
        public static CreateCommandOptionRequest Debug =
            CommandOptions.Debug.ConvertTo<CreateCommandOptionRequest>();
        public static CreateCommandOptionRequest Production =
            CommandOptions.Production.ConvertTo<CreateCommandOptionRequest>();
    }

    public static class UpdateCommandOptions
    {
        public static UpdateCommandOptionRequest Optional1 =
            CommandOptions.Optional1.ConvertTo<UpdateCommandOptionRequest>();
        public static UpdateCommandOptionRequest Optional2 =
            CommandOptions.Optional2.ConvertTo<UpdateCommandOptionRequest>();
        public static UpdateCommandOptionRequest Debug =
            CommandOptions.Debug.ConvertTo<UpdateCommandOptionRequest>();
        public static UpdateCommandOptionRequest Production =
            CommandOptions.Production.ConvertTo<UpdateCommandOptionRequest>();
    }

    
    public static class CreateCommands
    {
        public static CreateCommandRequest DotNetRestore = TestData.DomainModels.Commands.DotNetRestore.ConvertTo<CreateCommandRequest>();
        public static CreateCommandRequest DotNetBuild = TestData.DomainModels.Commands.DotNetBuild.ConvertTo<CreateCommandRequest>();
        public static CreateCommandRequest Deploy = TestData.DomainModels.Commands.Deploy.ConvertTo<CreateCommandRequest>();
        public static CreateCommandRequest Test = TestData.DomainModels.Commands.Test.ConvertTo<CreateCommandRequest>();
    }

    public static class UpdateCommands
    {
        public static UpdateCommandRequest DotNetRestore = TestData.DomainModels.Commands.DotNetRestore.ConvertTo<UpdateCommandRequest>();
        public static UpdateCommandRequest DotNetBuild = TestData.DomainModels.Commands.DotNetBuild.ConvertTo<UpdateCommandRequest>();
        public static UpdateCommandRequest Deploy = TestData.DomainModels.Commands.Deploy.ConvertTo<UpdateCommandRequest>();
        public static UpdateCommandRequest Test = TestData.DomainModels.Commands.Test.ConvertTo<UpdateCommandRequest>();
    }
}