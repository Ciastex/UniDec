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
            var filePaths = Directory.GetFiles(_path, "*.Codec.dll");
            if (filePaths.Length == 0)
            {
                Console.WriteLine("No codecs detected.");
                return new List<ICodec>();
            }

            return (
                from path 
                in filePaths 
                select Assembly.LoadFrom(path) 
                into assembly 
                from type 
                in assembly.GetExportedTypes() 
                select ActivateCodec(type)
            ).ToList();
        }

        private ICodec ActivateCodec(Type assemblyType)
        {
            if (assemblyType.GetInterface(typeof(ICodec).Name) == null)
                return null;

            return Activator.CreateInstance(assemblyType) as ICodec;
        }
    }
}
