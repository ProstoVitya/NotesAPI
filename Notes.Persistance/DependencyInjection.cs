﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Notes.Persistance
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services,
            IConfiguration configuration)
        {
            var connectionString = configuration["DbConnection"];
            services.AddDbContext<NotesDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });

            services.AddScoped<INotesDbContext>(provider =>
                provider.GetService<NotesDbContext>());
            return services;
        }
    }
}
