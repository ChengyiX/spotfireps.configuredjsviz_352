using System;
using Spotfire.Dxp.Application.Extension;
using SpotfirePS.Framework.JSVisualization;

namespace SpotfirePS.ConfiguredJSViz
{
    public class ConfiguredVisualizationAddIn : AddIn
    {
        protected override void RegisterVisuals(VisualRegistrar registrar)
        {
            base.RegisterVisuals(registrar);
            try
            {
                registrar.Register(new JSVisualizationPlotFactory());
            }
            catch (Exception)
            {
                ;
                //throw;
            }
            registrar.Register(new ConfiguredVisualizationFactory());
        }
    }
}
