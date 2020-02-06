using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;

namespace PeterKApplication.Helpers
{
    public class FakeByteArrayImage
    {
        public static byte[] GetFakeFromResource(string r)
        {
            var assembly = Assembly.GetCallingAssembly();
            byte[] buffer = null;

            using (Stream s = assembly.GetManifestResourceStream(r))
            {
                if (s != null)
                {
                    buffer = new byte[s.Length];
                    s.Read(buffer, 0, (int)s.Length);
                }
            }

            return buffer;
        }
    }
}
