using Microsoft.EntityFrameworkCore;
using System;

namespace Vsol.Api.Shared.Infra.Data.Contexts
{
    public class DataContext : DbContext
    {
        public void InitConfigurations()
        {
            this.ChangeTracker.AutoDetectChangesEnabled = false;
            this.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }
    }
}