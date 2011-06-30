using Raven.Client;
using Raven.Client.Client;
using Raven.Client.Document;

namespace Plumbing.RavenDb
{
    public static class InitializeRavenDb
    {
        static IDocumentStore documentStore;

        public static IDocumentStore InitializeEmbedded(string dataDirectory = "data")
        {
            documentStore = new EmbeddableDocumentStore { DataDirectory = dataDirectory };
            documentStore.Initialize();
            return documentStore;
        }

        public static IDocumentStore InitializeServer(string url)
        {
            documentStore = new DocumentStore { Url = url };
            documentStore.Initialize();
            return documentStore;
        }

        public static void Shutdown()
        {
            if (documentStore == null) return;

            documentStore.Dispose();
            documentStore = null;
        }
    }
}
