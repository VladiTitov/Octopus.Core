﻿using Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Common.Interfaces;

namespace Octopus.Core.Loader.WebApi.Infrastructure.Migrations.Interfaces
{
    public interface IMigrationService
    {
        IProviderMigration GetMigrationForProvider();
    }
}