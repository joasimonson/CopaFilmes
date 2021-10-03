using System.Collections.Generic;
using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CopaFilmes.Tests.Integration.XunitExtensions
{
    public class XunitTestFrameworkExecutorDependencyFixture : XunitTestFrameworkExecutor
    {
        public XunitTestFrameworkExecutorDependencyFixture(
            AssemblyName assemblyName,
            ISourceInformationProvider sourceInformationProvider,
            IMessageSink diagnosticMessageSink)
            : base(
                assemblyName,
                sourceInformationProvider,
                diagnosticMessageSink)
        { }

        protected override async void RunTestCases(
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink executionMessageSink,
            ITestFrameworkExecutionOptions executionOptions)
        {
            using var assemblyRunner = new XunitTestAssemblyRunnerDependencyFixture(TestAssembly, testCases, DiagnosticMessageSink, executionMessageSink, executionOptions);
            await assemblyRunner.RunAsync();
        }
    }
}
