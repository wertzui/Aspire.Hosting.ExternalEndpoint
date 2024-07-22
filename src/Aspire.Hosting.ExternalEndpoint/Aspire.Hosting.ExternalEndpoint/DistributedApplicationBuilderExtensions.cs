using Aspire.Hosting.ApplicationModel;
using Aspire.Hosting.ExternalEndpoint;
using System;

namespace Aspire.Hosting;

/// <summary>
/// Contains extension methods for <see cref="IDistributedApplicationBuilder"/> to add <see cref="ExternalEndpointResource"/>s.
/// </summary>
public static class DistributedApplicationBuilderExtensions
{
    /// <summary>
    /// Adds an <see cref="ExternalEndpointResource"/> to the <see cref="IDistributedApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The builder to add the external endpoint to.</param>
    /// <param name="name">The name of the external endpoint.</param>
    /// <param name="uri">The URI under which the endpoint is reachable.</param>
    /// <returns>The resource builder for further configuration.</returns>
    public static IResourceBuilder<ExternalEndpointResource> AddExternalEndpoint(this IDistributedApplicationBuilder builder, string name, Uri uri)
    {
        var project = new ExternalEndpointResource(name, uri);
        var resourceBuilder = builder
            .AddResource(project);

        return resourceBuilder;
    }

    /// <summary>
    /// Adds an <see cref="ExternalEndpointResource"/> to the <see cref="IDistributedApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The builder to add the external endpoint to.</param>
    /// <param name="name">The name of the external endpoint.</param>
    /// <param name="uri">The URI under which the endpoint is reachable.</param>
    /// <returns>The resource builder for further configuration.</returns>
    public static IResourceBuilder<ExternalEndpointResource> AddExternalEndpoint(this IDistributedApplicationBuilder builder, string name, string uri)
        => builder.AddExternalEndpoint(name, new Uri(uri));

    /// <summary>
    /// Adds an <see cref="ExternalEndpointResource"/> to the <see cref="IDistributedApplicationBuilder"/>.
    /// </summary>
    /// <param name="builder">The builder to add the external endpoint to.</param>
    /// <param name="parameter">The name of the parameter which holds the URI pointing to the external endpoint.</param>
    /// <param name="name">The name of the external endpoint. Defaults to the name of the parameter if none is given.</param>
    /// <returns>The resource builder for further configuration.</returns>
    public static IResourceBuilder<ExternalEndpointResource> AddExternalEndpointFromParameter(this IDistributedApplicationBuilder builder, string parameter, string name = null)
    {
        var parameterResource = builder.AddParameter(parameter);
        var uri = parameterResource.Resource.Value;

        var resourceBuilder = builder
            .AddExternalEndpoint(name ?? parameter, uri);

        return resourceBuilder;
    }
}