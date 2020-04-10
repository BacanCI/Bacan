using System;
using ServiceStack;

namespace Bakana.ServiceInterface
{
    public class Err
    {
        public static Exception BatchNotFound(string batchId) =>
            HttpError.NotFound(ErrMsg.BatchNotFound(batchId));
        public static Exception BatchVariableAlreadyExists(string VariableName) =>
            HttpError.Conflict(ErrMsg.BatchVariableAlreadyExists(VariableName));
        public static Exception BatchVariableNotFound(string VariableName) =>
            HttpError.NotFound(ErrMsg.BatchVariableNotFound(VariableName));
        public static Exception BatchOptionAlreadyExists(string OptionName) =>
            HttpError.Conflict(ErrMsg.BatchOptionAlreadyExists(OptionName));
        public static Exception BatchOptionNotFound(string OptionName) =>
            HttpError.NotFound(ErrMsg.BatchOptionNotFound(OptionName));
        public static Exception BatchArtifactAlreadyExists(string ArtifactName) =>
            HttpError.Conflict(ErrMsg.BatchArtifactAlreadyExists(ArtifactName));
        public static Exception BatchArtifactNotFound(string ArtifactName) =>
            HttpError.NotFound(ErrMsg.BatchArtifactNotFound(ArtifactName));
        public static Exception BatchArtifactOptionAlreadyExists(string OptionName) =>
            HttpError.Conflict(ErrMsg.BatchArtifactOptionAlreadyExists(OptionName));
        public static Exception BatchArtifactOptionNotFound(string OptionName) =>
            HttpError.NotFound(ErrMsg.BatchArtifactOptionNotFound(OptionName));
        
        public static Exception StepAlreadyExists(string StepName) =>
            HttpError.Conflict(ErrMsg.StepAlreadyExists(StepName));
        public static Exception StepNotFound(string StepName) =>
            HttpError.NotFound(ErrMsg.StepNotFound(StepName));
        public static Exception StepVariableAlreadyExists(string VariableName) =>
            HttpError.Conflict(ErrMsg.StepVariableAlreadyExists(VariableName));
        public static Exception StepVariableNotFound(string VariableName) =>
            HttpError.NotFound(ErrMsg.StepVariableNotFound(VariableName));
        public static Exception StepOptionAlreadyExists(string OptionName) =>
            HttpError.Conflict(ErrMsg.StepOptionAlreadyExists(OptionName));
        public static Exception StepOptionNotFound(string OptionName) =>
            HttpError.NotFound(ErrMsg.StepOptionNotFound(OptionName));
        public static Exception StepArtifactAlreadyExists(string ArtifactName) =>
            HttpError.Conflict(ErrMsg.StepArtifactAlreadyExists(ArtifactName));
        public static Exception StepArtifactNotFound(string ArtifactName) =>
            HttpError.NotFound(ErrMsg.StepArtifactNotFound(ArtifactName));
        public static Exception StepArtifactOptionAlreadyExists(string OptionName) =>
            HttpError.Conflict(ErrMsg.StepArtifactOptionAlreadyExists(OptionName));
        public static Exception StepArtifactOptionNotFound(string OptionName) =>
            HttpError.NotFound(ErrMsg.StepArtifactOptionNotFound(OptionName));
        
        public static Exception CommandAlreadyExists(string CommandName) =>
            HttpError.Conflict(ErrMsg.CommandAlreadyExists(CommandName));
        public static Exception CommandNotFound(string CommandName) =>
            HttpError.NotFound(ErrMsg.CommandNotFound(CommandName));
        public static Exception CommandVariableAlreadyExists(string VariableName) =>
            HttpError.Conflict(ErrMsg.CommandVariableAlreadyExists(VariableName));
        public static Exception CommandVariableNotFound(string VariableName) =>
            HttpError.NotFound(ErrMsg.CommandVariableNotFound(VariableName));
        public static Exception CommandOptionAlreadyExists(string OptionName) =>
            HttpError.Conflict(ErrMsg.CommandOptionAlreadyExists(OptionName));
        public static Exception CommandOptionNotFound(string OptionName) =>
            HttpError.NotFound(ErrMsg.CommandOptionNotFound(OptionName));
    }
    
    public static class ErrMsg
    {
        public static string BatchNotFound(string batchId) => $"Batch {batchId} not found";
        public static string BatchVariableAlreadyExists(string VariableName) => $"Batch Variable {VariableName} already exists";
        public static string BatchVariableNotFound(string VariableName) => $"Batch Variable {VariableName} not found";
        public static string BatchOptionAlreadyExists(string OptionName) => $"Batch Option {OptionName} already exists";
        public static string BatchOptionNotFound(string OptionName) => $"Batch Option {OptionName} not found";
        public static string BatchArtifactAlreadyExists(string ArtifactName) => $"Batch Artifact {ArtifactName} already exists";
        public static string BatchArtifactNotFound(string ArtifactName) => $"Batch Artifact {ArtifactName} not found";
        public static string BatchArtifactOptionAlreadyExists(string OptionName) => $"Batch Artifact Option {OptionName} already exists";
        public static string BatchArtifactOptionNotFound(string OptionName) => $"Batch Artifact Option {OptionName} not found";

        public static string StepAlreadyExists(string StepName) => $"Step {StepName} already exists";
        public static string StepNotFound(string StepName) => $"Step {StepName} not found";
        public static string StepVariableAlreadyExists(string VariableName) => $"Step Variable {VariableName} already exists";
        public static string StepVariableNotFound(string VariableName) => $"Step Variable {VariableName} not found";
        public static string StepOptionAlreadyExists(string OptionName) => $"Step Option {OptionName} already exists";
        public static string StepOptionNotFound(string OptionName) => $"Step Option {OptionName} not found";
        public static string StepArtifactAlreadyExists(string ArtifactName) => $"Step Artifact {ArtifactName} already exists";
        public static string StepArtifactNotFound(string ArtifactName) => $"Step Artifact {ArtifactName} not found";
        public static string StepArtifactOptionAlreadyExists(string OptionName) => $"Step Artifact Option {OptionName} already exists";
        public static string StepArtifactOptionNotFound(string OptionName) => $"Step Artifact Option {OptionName} not found";

        public static string CommandAlreadyExists(string CommandName) => $"Command {CommandName} already exists";
        public static string CommandNotFound(string CommandName) => $"Command {CommandName} not found";
        public static string CommandVariableAlreadyExists(string VariableName) => $"Command Variable {VariableName} already exists";
        public static string CommandVariableNotFound(string VariableName) => $"Command Variable {VariableName} not found";
        public static string CommandOptionAlreadyExists(string OptionName) => $"Command Option {OptionName} already exists";
        public static string CommandOptionNotFound(string OptionName) => $"Command Option {OptionName} not found";
    }
}