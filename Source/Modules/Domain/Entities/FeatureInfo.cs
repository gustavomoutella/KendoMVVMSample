using System;
using System.Collections.Generic;
using Vsol.Api.Shared.Domain;
using Vsol.Api.AppSecurity.Domain.Commands.Inputs.Feature;

namespace Vsol.Api.AppSecurity.Domain.Entities
{
	public class FeatureInfo : EntityInfo<FeatureInfo>
	{
        public FeatureInfo() { }
		
		public FeatureInfo(InsertFeatureCommand command)
		{
			Map(command, this);
			IdFeature = Guid.NewGuid();
            InitCollections();
		}
		
		public FeatureInfo(UpdateFeatureCommand command)
		{
			Map(command, this);
            InitCollections();
		}

        private void InitCollections()
        {
            Authorizations = new List<AuthorizationInfo>();
            Children = new List<FeatureInfo>();
        }

        public Guid IdFeature { get; private set; }
		
		public Guid? IdFeatureParent { get; private set; }
		
		public string FeatureName { get; private set; }
		
		public string FeatureKey { get; private set; }
		
		public string Description { get; private set; }
		
		public string RecursiveName { get; private set; }

        public void SetRecursiveName(string recursiveName)
        {
            RecursiveName = recursiveName;
        }

        #region Navigation properties

        public virtual FeatureInfo Parent { get; set; }
		
		public virtual ICollection<AuthorizationInfo> Authorizations { get; set; }
		
		public virtual ICollection<FeatureInfo> Children { get; set; }
		
		#endregion
	}
}