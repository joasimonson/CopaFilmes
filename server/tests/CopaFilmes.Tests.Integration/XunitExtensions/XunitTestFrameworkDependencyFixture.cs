using System.Reflection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CopaFilmes.Tests.Integration.XunitExtensions;

public class XunitTestFrameworkDependencyFixture : XunitTestFramework
{
	public XunitTestFrameworkDependencyFixture(IMessageSink messageSink)
		: base(messageSink)
	{ }

	protected override ITestFrameworkExecutor CreateExecutor(AssemblyName assemblyName)
		=> new XunitTestFrameworkExecutorDependencyFixture(assemblyName, SourceInformationProvider, DiagnosticMessageSink);
}
