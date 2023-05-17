using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Reflection;
using XrmToolBox.Extensibility;
using XrmToolBox.Extensibility.Interfaces;

namespace Schiavone.XrmToolBox.CloneUserSetup
{
    // Do not forget to update version number and author (company attribute) in AssemblyInfo.cs class
    // To generate Base64 string for Images below, you can use https://www.base64-image.de/
    [Export(typeof(IXrmToolBoxPlugin)),
        ExportMetadata("Name", "Clone User Setup"),
        ExportMetadata("Description", "Copies Business Unit, Security Roles, and Teams from source user to target user."),
        // Please specify the base64 content of a 32x32 pixels image
        ExportMetadata("SmallImageBase64", "iVBORw0KGgoAAAANSUhEUgAAACAAAAAgCAYAAABzenr0AAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAQ3SURBVFhHzZZBiFxFEIaregZdYsBFM7MKC0aIsGLABQ0sGHAXA14iGMxBQSE5BBFc9KCgqPQ8cvCiGDDiRUggBiMK6smTuIsrGFkxooE9eFhhD2bGxA1sQpCZLv+q1zPOm30z83ZH0A9mqrtev9fV1VXVTf81HGU+d/kxd52eF+ZHWGhKmFZY5Kuwg07Sb8mNOGok+htwu59yJf4MralUkWEliDxGjeTX2N82LspNOMenIKaE6NPAcneo1xiT3qN91TviM0S+bIP/bUoT/qCr1oQrNfXAJrha+1Kfuwn/ZFRtm1wPBOGHVDqW90zRgyN5W6UIP2CKEcjfAqFxFS2m363fQ0cvtNPkCOQawCTnVTqh/abooa1nlh9MMQL5W+BoAaKJKd6kCb831UasDz2eB7FxI9E3DV3VP43HiHTaQOSfhlcuCvF9eOEIdHC9HAv15AMbfJuf5BK/xkwz6E1ja1ZRM5ZFY6WefGdj+jCwECHaP8GAw7HbzXJoyiG6kqxZJgi/D53GzQYm/wVyD768C7IJ409IvfYy2rnkGODLropVCr+Ap1n3Z1lAfZh3wj9aT+TV0EjesjZAKs8iS9SDk3g2j2cn0ydZegzwZa7yR3HVav3nKL3n4doLLbi15PRjNI40fRTuPevYYgEB2bUd3Uz43TDwZ23CY/eqx0zfRcaALpevBJKjA/fvVj/ubuY/0VpGldyXKjeDWHodXz4OLxyFF05HdYdOFpQq/nBncpF9w4KndJMFHBxCg4NMaFklDrTcohUN8GUMSFNLV95INlL9AJjGTEqU/XDUPi9yi5YZUKrQAYg9InRu2MrbtFq0ohJegyf6H0rtso40/skUPZgBcKNFOyrbtyoLcTlRA5bw5b3ImldSZQ8VPw0DX8IEN1C0zkVthmgA368Sgwqtvk1oyTEInAt8HAF8qlM1UZgQfC8iS77RLlb/DLY191yxLMDR+jXErJ77dClZVV1h0ouLvn9HqsjQRBo/1WokeofIJWMAYuAE/HFVdf3Q2oDVXLCOuZifQJ3Q8jxpuiwbGlc41j9u1fXcSHC+ZMkYoO2haD7jVoSJz2Dix6NWWYd167GtX9bs6PbKGrbsEGLH0rKNGaArQZWzO8AwWoHWsbd6XZvGbw0GvQGDlnLvh3f6XaVAMyHwszD2oAYjVvBcd0FKDdgC8JYG1n649kP5S+bpavLPqgfgKv4I0uxdNMes0MVt7FTCIqRHNGo/skUaKFgFJ1ds1Tiw0CzDg++kWtSgKAvBO+a+gBgLTuboWnIl1RZHri9+T7fMzsDtB9zOhxfl2uJqcQ9gP7Fhu5EFS1tO1S5QE86qlEAPqixsQKmZvoAvpCm4TUJ8H8XPDqfCBnTKtchFU2yXS4nemBCPaYoWzgKu+Boze1jyB7rDT8tBYCvxv4B7xNyWssDQu55+YJTf/weivwEkcIzMZzoxdwAAAABJRU5ErkJggg=="),
        // Please specify the base64 content of a 80x80 pixels image
        ExportMetadata("BigImageBase64", "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"),
        ExportMetadata("BackgroundColor", "Lavender"),
        ExportMetadata("PrimaryFontColor", "Black"),
        ExportMetadata("SecondaryFontColor", "Gray")]
    public class MyPlugin : PluginBase
    {
        public override IXrmToolBoxPluginControl GetControl()
        {
            return new MyPluginControl();
        }

        /// <summary>
        /// Constructor 
        /// </summary>
        public MyPlugin()
        {
            // If you have external assemblies that you need to load, uncomment the following to 
            // hook into the event that will fire when an Assembly fails to resolve
            // AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(AssemblyResolveEventHandler);
        }

        /// <summary>
        /// Event fired by CLR when an assembly reference fails to load
        /// Assumes that related assemblies will be loaded from a subfolder named the same as the Plugin
        /// For example, a folder named Sample.XrmToolBox.MyPlugin 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly AssemblyResolveEventHandler(object sender, ResolveEventArgs args)
        {
            Assembly loadAssembly = null;
            Assembly currAssembly = Assembly.GetExecutingAssembly();

            // base name of the assembly that failed to resolve
            var argName = args.Name.Substring(0, args.Name.IndexOf(","));

            // check to see if the failing assembly is one that we reference.
            List<AssemblyName> refAssemblies = currAssembly.GetReferencedAssemblies().ToList();
            var refAssembly = refAssemblies.Where(a => a.Name == argName).FirstOrDefault();

            // if the current unresolved assembly is referenced by our plugin, attempt to load
            if (refAssembly != null)
            {
                // load from the path to this plugin assembly, not host executable
                string dir = Path.GetDirectoryName(currAssembly.Location).ToLower();
                string folder = Path.GetFileNameWithoutExtension(currAssembly.Location);
                dir = Path.Combine(dir, folder);

                var assmbPath = Path.Combine(dir, $"{argName}.dll");

                if (File.Exists(assmbPath))
                {
                    loadAssembly = Assembly.LoadFrom(assmbPath);
                }
                else
                {
                    throw new FileNotFoundException($"Unable to locate dependency: {assmbPath}");
                }
            }

            return loadAssembly;
        }
    }
}