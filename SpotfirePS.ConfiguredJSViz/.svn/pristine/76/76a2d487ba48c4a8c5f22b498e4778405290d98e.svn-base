using System.IO;
using System.Linq;
using System.Reflection;
using Spotfire.Dxp.Application;
using Spotfire.Dxp.Application.Extension;
using SpotfirePS.ConfiguredJSViz.Properties;
using SpotfirePS.Framework.JSVisualization;
using SpotfirePS.Framework.JSVisualization.Core;


namespace SpotfirePS.ConfiguredJSViz
{
    internal sealed class ConfiguredVisualizationFactory : ConfiguredVisualFactory<JSVisualizationModel>
    {

        public ConfiguredVisualizationFactory() :
            base(
            JSVisualizationIdentifiers.JSVisualization,
            ConfiguredVisualizationTypeIdentifier.ConfiguredVisualizationIdentifier,
            VisualCategory.Visualization,
            Resources.doughnut,
            null)
        {
            
        }

        protected override void AutoConfigureCore(JSVisualizationModel visual)
        {   
            visual.AddGeneralPropertyPage();
            visual.AddPropertyPage("Doughtnut", "SpotfirePS.ConfiguredJSVizForms", "SpotfirePS.ConfiguredJSVizForms.DoughnutPropertyPage");

            base.AutoConfigureCore(visual);
            
            var doc = visual.Context.GetAncestor<Document>();
            

            // Add the files;
            var cr = doc.CustomNodes.AddNewIfNeeded<ContentRepository>();

            var content = System.Text.Encoding.UTF8.GetString(GetEmbeddedResource("SpotfirePS.ConfiguredJSViz.scripts.jQuery.js"));
            var urlReference = new UrlReference(@"scripts.jQuery.js", string.Empty, content, ContentType.JS, true);
            cr["scripts.jQuery.js"] = urlReference;
            visual.UrlInclusions.Add("scripts.jQuery.js");

            content = System.Text.Encoding.UTF8.GetString(GetEmbeddedResource("SpotfirePS.ConfiguredJSViz.scripts.d3.v3.min.js"));
            urlReference = new UrlReference(@"scripts.d3.v3.min.js", string.Empty, content, ContentType.JS, true);
            cr["scripts.d3.v3.min.js"] = urlReference;
            visual.UrlInclusions.Add("scripts.d3.v3.min.js");

            content = System.Text.Encoding.UTF8.GetString(GetEmbeddedResource("SpotfirePS.ConfiguredJSViz.scripts.DoughnutChart.js"));
            urlReference = new UrlReference(@"scripts.DoughnutChart.js", string.Empty, content, ContentType.JS, true);
            cr["scripts.DoughnutChart.js"] = urlReference;
            visual.UrlInclusions.Add("scripts.DoughnutChart.js");

            content = System.Text.Encoding.UTF8.GetString(GetEmbeddedResource("SpotfirePS.ConfiguredJSViz.scripts.JSViz.js"));
            urlReference = new UrlReference(@"scripts.JSViz.js", string.Empty, content, ContentType.JS, true);
            cr["scripts.JSViz.js"] = urlReference;
            visual.UrlInclusions.Add("scripts.JSViz.js");

            visual.AutoConfigure();

        }


        /// <summary>
        /// Gets the embedded resource specified by the name.
        /// </summary>
        /// <param name="resourceName">Name of the resource.</param>
        /// <returns>
        /// The resource bytes, or null.
        /// </returns>
        public static new byte[] GetEmbeddedResource(string resourceName)
        {
            if (string.IsNullOrEmpty(resourceName))
            {
                return null;
            }

            var assembly = Assembly.GetCallingAssembly();

            var names = assembly.GetManifestResourceNames();
            if (!names.Contains(resourceName))
            {
                return null;
            }
            using (var stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        CopyContent(stream, ms);
                        return ms.ToArray();
                    }
                }
            }

            return null;
        }

        internal static void CopyContent(Stream fromStream, Stream toStream)
        {

            byte[] buf = new byte[8 * 1024]; // Arbitrary buffer size.
            while (true)
            {
                int n = fromStream.Read(buf, 0, buf.Length);
                if (n <= 0)
                {
                    break;
                }

                toStream.Write(buf, 0, n);
            }
        }
    }
}
