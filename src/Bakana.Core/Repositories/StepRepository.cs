using System.Collections.Generic;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.Data;

namespace Bakana.Core.Repositories
{
    public class StepRepository : IStepRepository
    {
        private readonly IDbConnectionFactory dbConnectionFactory;

        public StepRepository(IDbConnectionFactory dbConnectionFactory)
        {
            this.dbConnectionFactory = dbConnectionFactory;
        }

        public Task<ulong> Create(Step step)
        {
            throw new System.NotImplementedException();
        }

        public Task Update(Step step)
        {
            throw new System.NotImplementedException();
        }

        public Task Delete(ulong stepId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Step> Get(ulong stepId)
        {
            throw new System.NotImplementedException();
        }

        public Task<Step> Get(string batchId, string stepId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<Step>> GetAll(string batchId)
        {
            throw new System.NotImplementedException();
        }

        public Task UpdateState(ulong id, StepState state)
        {
            throw new System.NotImplementedException();
        }

        public Task<ulong> CreateOrUpdateStepVariable(StepVariable variable)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepVariable> GetStepVariable(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepVariable> GetStepVariable(ulong stepId, string variableId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StepVariable>> GetAllStepVariables(ulong stepId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteStepVariable(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ulong> CreateOrUpdateStepOption(StepOption option)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepOption> GetStepOption(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepOption> GetStepOption(ulong stepId, string optionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StepOption>> GetAllStepOptions(ulong stepId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteStepOption(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ulong> CreateStepArtifact(StepArtifact stepArtifact)
        {
            throw new System.NotImplementedException();
        }

        public Task<ulong> UpdateStepArtifact(StepArtifact stepArtifact)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepArtifact> GetStepArtifact(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepArtifact> GetStepArtifact(ulong stepId, string artifactId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StepArtifact>> GetAllStepArtifacts(ulong stepId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteStepArtifact(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<ulong> CreateOrUpdateStepArtifactOption(StepArtifactOption option)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepArtifactOption> GetStepArtifactOption(ulong id)
        {
            throw new System.NotImplementedException();
        }

        public Task<StepArtifactOption> GetStepArtifactOption(ulong artifactId, string optionId)
        {
            throw new System.NotImplementedException();
        }

        public Task<List<StepArtifactOption>> GetAllStepArtifactOptions(ulong artifactId)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteStepArtifactOption(ulong id)
        {
            throw new System.NotImplementedException();
        }
    }
}