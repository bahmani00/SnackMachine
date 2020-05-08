using System.Reflection;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.AcceptanceCriteria;
using FluentNHibernate.Conventions.Helpers;
using FluentNHibernate.Conventions.Instances;
using NHibernate;
using NHibernate.Event;

namespace SnackMachineApp.Infrastructure.Data.NHibernate
{
    internal class SessionFactory
    {
        private readonly ISessionFactory _factory;

        public SessionFactory(CommandsConnectionProvider connectionStringProvider, IDomainEventDispatcher domainEventDispatcher)
        {
            _factory = BuildSessionFactory(connectionStringProvider.Value, domainEventDispatcher);
        }

        public ISession OpenSession()
        {
            return _factory.OpenSession();
        }

        private static ISessionFactory BuildSessionFactory(string connectionString, IDomainEventDispatcher domainEventDispatcher)
        {
            FluentConfiguration configuration = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(connectionString))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly())
                    .Conventions.Add(
                        ForeignKey.EndsWith("Id"),
                        ConventionBuilder.Property
                            .When(criteria => criteria.Expect(x => x.Nullable, Is.Not.Set), x => x.Not.Nullable()))
                    .Conventions.Add<TableNameConvention>()
                    .Conventions.Add<HiLoConvention>()
                )
                .ExposeConfiguration(x =>
                 {
                     //to create db tables
                     //new SchemaExport(x).Execute(true, true, false);
                     x.EventListeners.PostCommitUpdateEventListeners =
                         new IPostUpdateEventListener[] { new NHibernateDbEventListener(domainEventDispatcher) };
                     x.EventListeners.PostCommitInsertEventListeners =
                         new IPostInsertEventListener[] { new NHibernateDbEventListener(domainEventDispatcher) };
                     x.EventListeners.PostCommitDeleteEventListeners =
                         new IPostDeleteEventListener[] { new NHibernateDbEventListener(domainEventDispatcher) };
                     x.EventListeners.PostCollectionUpdateEventListeners =
                         new IPostCollectionUpdateEventListener[] { new NHibernateDbEventListener(domainEventDispatcher) };
                 });

            return configuration.BuildSessionFactory();
        }

        private class TableNameConvention : IClassConvention
        {
            public void Apply(IClassInstance instance)
            {
                instance.Table("[dbo].[" + instance.EntityType.Name + "]");
            }
        }

        private class HiLoConvention : IIdConvention
        {
            public void Apply(IIdentityInstance instance)
            {
                instance.Column(instance.EntityType.Name + "Id");
                instance.GeneratedBy.HiLo("[dbo].[Ids]", "NextHigh", "9", "EntityName = '" + instance.EntityType.Name + "'");
            }
        }
    }
}
