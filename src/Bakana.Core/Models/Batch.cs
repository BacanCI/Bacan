using System;
using System.Collections.Generic;
using ServiceStack.DataAnnotations;
using ServiceStack.OrmLite;

namespace Bakana.Core.Models
{
    public class Batch
    {
        [PrimaryKey]
        public string Id { get; set; }
        
        public string UserBatchId { get; set; }
        
        public string Description { get; set; }
        
        [Reference]
        public List<BatchOption> Options { get; set; }
        
        [Reference]
        public List<BatchVariable> Variables { get; set; }
        
        [Reference]
        public List<BatchArtifact> InputArtifacts { get; set; }
        
        [Reference]
        public List<Step> Steps { get; set; }
        
        [Default(OrmLiteVariables.SystemUtc)]
        public DateTime CreatedOn { get; set; }    
        
        public DateTime? ExpiresOn { get; set; }
    }
}
