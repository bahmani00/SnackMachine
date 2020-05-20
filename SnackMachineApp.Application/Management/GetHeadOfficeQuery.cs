﻿using Microsoft.Extensions.DependencyInjection;
using SnackMachineApp.Application.Seedwork;
using SnackMachineApp.Domain.Management;
using SnackMachineApp.Domain.SeedWork;
using System;

namespace SnackMachineApp.Application.Management
{
    public class GetHeadOfficeQuery : IQuery<HeadOffice>
    {
        public GetHeadOfficeQuery(long headOfficeId)
        {
            HeadOfficeId = headOfficeId;
        }

        public long HeadOfficeId { get; }
    }

    internal class GetHeadOfficeQueryHandler : IQueryHandler<GetHeadOfficeQuery, HeadOffice>
    {
        private readonly IServiceProvider serviceProvider;

        public GetHeadOfficeQueryHandler(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public HeadOffice Handle(GetHeadOfficeQuery request)
        {
            using (var repository = serviceProvider.GetService<IRepository<HeadOffice>>())
                return repository.GetById(request.HeadOfficeId);
        }
    }
}
