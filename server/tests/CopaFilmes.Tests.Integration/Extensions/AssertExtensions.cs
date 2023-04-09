using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Web;
using System.Net;

namespace CopaFilmes.Tests.Integration.Extensions;

public static class AssertExtensions
{
	public static AndConstraint<HttpResponseMessageAssertions> Be422UnprocessableEntity(this HttpResponseMessageAssertions parent, string because = "", params object[] becauseArgs)
	{
		var expected = HttpStatusCode.UnprocessableEntity;
		Execute.Assertion
			.ForCondition(parent.Subject != null)
			.BecauseOf(because, becauseArgs)
			.FailWith("Expected an HTTP {context:response} to assert{reason}, but found <null>.");

		Execute.Assertion
			.BecauseOf(because, becauseArgs)
			.ForCondition(expected == parent.Subject.StatusCode)
			.FailWith("Expected HTTP {context:response} to be {0}{reason}, but found {1}.{2}", expected.ToString(), parent.Subject.StatusCode, parent.Subject);

		return new AndConstraint<HttpResponseMessageAssertions>(parent);
	}
}
