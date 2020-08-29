using System;
using Spotfire.Dxp.Application.Extension;
using System.Windows.Forms;
using SpotfirePS.Framework.JSVisualization.Core;
using SpotfirePS.Framework.JSVisualizationForms.Forms;

namespace SpotfirePS.ConfiguredJSVizForms
{
    public class PropertiesAddIn : AddIn
    {
        protected override void RegisterViews(ViewRegistrar registrar)
        {
            base.RegisterViews(registrar);
            try
            {
                registrar.Register(typeof(Form), typeof(JSVisualizationModel), typeof(JSVisualizationPropertyDialog)); 
            }
            catch (Exception)
            {
                //Ignore this if it is already registered
                //throw;
            }
        }
    }
}
