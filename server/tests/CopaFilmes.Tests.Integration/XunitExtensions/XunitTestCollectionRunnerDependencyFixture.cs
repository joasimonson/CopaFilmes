using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace CopaFilmes.Tests.Integration.XunitExtensions
{
    public class XunitTestCollectionRunnerDependencyFixture : XunitTestCollectionRunner
    {
        public XunitTestCollectionRunnerDependencyFixture(
            ITestCollection testCollection,
            IEnumerable<IXunitTestCase> testCases,
            IMessageSink diagnosticMessageSink,
            IMessageBus messageBus,
            ITestCaseOrderer testCaseOrderer,
            ExceptionAggregator aggregator,
            CancellationTokenSource cancellationTokenSource)
            : base(
                testCollection,
                testCases,
                diagnosticMessageSink,
                messageBus,
                testCaseOrderer,
                aggregator,
                cancellationTokenSource)
        { }

        protected override void CreateCollectionFixture(Type fixtureType)
        {
            var ctors = fixtureType.GetTypeInfo().DeclaredConstructors.Where(ci => !ci.IsStatic && ci.IsPublic).ToList();
            if (ctors.Count != 1)
            {
                Aggregator.Add(new TestClassException("Collection fixture type '" + fixtureType.FullName + "' may only define a single public constructor."));
            }
            else
            {
                var ctor = ctors[0];
                var missingParameters = new List<ParameterInfo>();
                var ctorArgs = ctor.GetParameters().Select(p =>
                {
                    if (!CollectionFixtureMappings.TryGetValue(p.ParameterType, out var arg))
                        missingParameters.Add(p);
                    return arg;
                }).ToArray();

                if (missingParameters.Count > 0)
                {
                    var parameters = string.Join(", ", missingParameters.Select(p => $"{p.ParameterType.Name} {p.Name}"));
                    Aggregator.Add(new TestClassException(
                        $"Class fixture type '{fixtureType.FullName}' had one or more unresolved constructor arguments: {parameters}"
                    ));
                }
                else
                {
                    Aggregator.Run(() => CollectionFixtureMappings[fixtureType] = ctor.Invoke(ctorArgs));
                }
            }
        }
    }
}
