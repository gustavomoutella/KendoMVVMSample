using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vsol.Api.AppSecurity.Domain.Repositories;
using Vsol.Api.Shared.Domain;

namespace Vsol.Api.AppSecurity.Domain.Commands.Handlers
{
    public class FeatureCommandHandler
    {
        private readonly IFeatureRepository _featureRepository;

        public FeatureCommandHandler(IFeatureRepository featureRepository)
        {
            _featureRepository = featureRepository;
        }

        private async Task<string> getRecursiveName(Guid? parentId, string recursiveName, IList<Guid> parentsId, NotificationResult result)
        {
            if (parentId.HasValue)
            {
                if (parentsId.Contains(parentId.Value))
                {
                    result.AddError("Inválido.");
                    return recursiveName;
                }

                var parent = await _featureRepository.GetByIdAsync(parentId.Value);

                if (parent != null)
                {
                    parentsId.Add(parentId.Value);
                    recursiveName = parent.FeatureName + " > " + recursiveName;

                    return await getRecursiveName(parent.IdFeatureParent, recursiveName, parentsId, result);
                }
            }

            return recursiveName;
        }
    }
}