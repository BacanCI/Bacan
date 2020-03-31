using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Bakana.Core.Entities;
using ServiceStack.OrmLite;

namespace Bakana.Core.Repositories
{
    public static class ArtifactExtensions
    {
        internal static async Task<List<StepArtifact>> GetAllStepArtifacts(this IDbConnection db, string stepId)
        {
            return await db.LoadSelectAsync<StepArtifact>(a => a.StepId == stepId);
        }

        internal static async Task<List<StepArtifact>> GetAllInputStepArtifacts(this IDbConnection db, string stepId)
        {
            return await db.LoadSelectAsync<StepArtifact>(a => a.StepId == stepId && !a.OutputArtifact);
        }
        
        internal static async Task<List<StepArtifact>> GetAllOutputStepArtifacts(this IDbConnection db, string stepId)
        {
            return await db.LoadSelectAsync<StepArtifact>(a => a.StepId == stepId && a.OutputArtifact);
        }
    }
}