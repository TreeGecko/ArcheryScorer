using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

namespace TreeGecko.Archery.Server.Bootstrapper
{
    public class CustomBootstrapper : DefaultNancyBootstrapper
    {
        

        protected override void ApplicationStartup(TinyIoCContainer _container, IPipelines _pipelines)
        {
            CookieBasedSessions.Enable(_pipelines);
            Nancy.Security.Csrf.Enable(_pipelines);
        }

        protected override void ConfigureConventions(NancyConventions _conventions)
        {
            base.ConfigureConventions(_conventions);

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("css", @"css")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("js", @"js")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("js/3rdParty", @"js/3rdParty")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("js/controllers", @"js/controllers")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("js/services", @"js/services")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("images", @"images")
            );

            _conventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("views", @"ClientViews")
            );
        }
    }
}