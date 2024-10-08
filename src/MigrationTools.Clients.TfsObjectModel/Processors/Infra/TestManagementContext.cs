﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.TeamFoundation.TestManagement.Client;
using MigrationTools;
using MigrationTools.Clients;

namespace MigrationTools.Processors.Infrastructure
{
    public class TestManagementContext
    {
        private readonly string testPlanQuery;
        private readonly IMigrationClient _source;
        private ITestManagementService tms;

        internal ITestManagementTeamProject Project { get; }

        public TestManagementContext(TfsTeamProjectEndpoint source) : this(source, null)
        {
        }

        public TestManagementContext(TfsTeamProjectEndpoint source, string testPlanQuery)
        {
            this.testPlanQuery = testPlanQuery;
            _source = source;
            tms = _source.GetService<ITestManagementService>();
            Project = tms.GetTeamProject(source.Options.Project);
        }

        internal List<ITestPlan> GetTestPlans()
        {
            var query = (string.IsNullOrWhiteSpace(testPlanQuery))
                ? "Select * From TestPlan"
                : $"Select * From TestPlan Where {testPlanQuery}";

            var testPlans = Project.TestPlans.Query(query);

            return testPlans.Select(tp => Project.TestPlans.Find(tp.Id)).ToList();
        }

        internal List<ITestRun> GetTestRuns()
        {
            return Project.TestRuns.Query("Select * From TestRun").ToList();
        }

        internal ITestPlan CreateTestPlan()
        {
            return Project.TestPlans.Create();
        }
    }
}