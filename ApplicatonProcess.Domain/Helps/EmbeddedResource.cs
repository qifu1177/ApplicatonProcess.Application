using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ApplicatonProcess.Domain.Helps
{
    public static class EmbeddedResource
    {
        public static string GetApiRequestFile(Assembly assembly,string namespaceAndFileName)
        {
            try
            {                
                using (var stream = assembly.GetManifestResourceStream(namespaceAndFileName))
                {
                    if (stream != null)
                        using (var reader = new StreamReader(stream, Encoding.UTF8))
                            return reader.ReadToEnd();
                }

            }

            catch (Exception exception)
            {
                throw new Exception($"Failed to read Embedded Resource {namespaceAndFileName}");
            }
            return "";
        }
    }
}
