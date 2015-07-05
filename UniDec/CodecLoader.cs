using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UniDecAPI;

namespace UniDec
{
    class CodecLoader
    {
        private readonly string _path;

        public CodecLoader(string path)
        {
            _path = path;
        }

        public List<ICodec> LoadCodecs()
        {
            var list = new List<ICodec>();

            var filePaths = Directory.GetFiles(_path, "*.Codec.dll");
            if (filePaths.Length == 0)
            {
                Console.WriteLine("No codecs detected.");
                return list;
            }

            foreach (var types in filePaths.Select(Assembly.LoadFrom).Select(assembly => assembly.GetExportedTypes()))
            {
                list.AddRange(types.Select(ActivateCodec));
            }
            return list;
        }

        private static ICodec ActivateCodec(Type assemblyType)
        {
            if (assemblyType.GetInterface(typeof(ICodec).Name) == null)
                return null;

            return Activator.CreateInstance(assemblyType) as ICodec;
        }
    }
}
