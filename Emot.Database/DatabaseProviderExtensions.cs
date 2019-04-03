using Emot.Database.Context;
using Emot.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Emot.Database
{
    public static class DatabaseProviderExtensions
    {
        public static IServiceCollection AddEmotDatabase(this IServiceCollection services)
        {
            // TODO: provide connection string thru configuration.
            var connection = @"Server=(localdb)\mssqllocaldb;Database=Emot;Trusted_Connection=True;ConnectRetryCount=0";
            services.AddDbContext<EmotDbContext>(options =>
            {
                options.UseSqlServer(connection);
            });
            services.AddTransient<IUnigramRepository, UnigramRepository>();
            services.AddTransient<IOpinionRepository, OpinionRepository>();
            return services;
        }
    }
}