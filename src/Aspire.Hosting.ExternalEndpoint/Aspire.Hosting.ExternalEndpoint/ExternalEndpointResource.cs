using Aspire.Hosting.ApplicationModel;
using System;

namespace Aspire.Hosting.ExternalEndpoint;

/// <summary>
/// Represents an external endpoint that is reachable under a specific URI.
/// The endpoint is not managed by Aspire, but can be used to reference external services.
/// </summary>
public class ExternalEndpointResource : Resource, IResourceWithServiceDiscovery
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalEndpointResource"/> class.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="uri">The URI of the external endpoint.</param>
    public ExternalEndpointResource(string name, Uri uri)
        : base(name)
    {
        Uri = uri;

        var endpointAnnotation = new EndpointAnnotation(System.Net.Sockets.ProtocolType.Tcp, uri.Scheme, targetPort: uri.Port, isExternal: true, isProxied: false);
        var allocatedEndpoint = new AllocatedEndpoint(endpointAnnotation, uri.Host, uri.Port);
        endpointAnnotation.AllocatedEndpoint = allocatedEndpoint;
        Annotations.Add(endpointAnnotation);

        var initialState = new ResourceSnapshotAnnotation(new CustomResourceSnapshot
        {
            ResourceType = "External",
            Properties = [],
            Urls = [new(name, uri.AbsoluteUri, false)]
        });
        Annotations.Add(initialState);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ExternalEndpointResource"/> class.
    /// </summary>
    /// <param name="name">The name of the resource.</param>
    /// <param name="uri">The URI string of the external endpoint.</param>
    public ExternalEndpointResource(string name, string uri)
        : this(name, new Uri(uri))
    {
    }

    /// <summary>
    /// Gets the URI of the external endpoint.
    /// </summary>
    public Uri Uri { get; }
}
