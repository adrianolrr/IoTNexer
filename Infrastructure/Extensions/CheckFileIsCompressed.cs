using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class CheckIfISCompressed
    {
        public static bool ValidateExtensionFile(string filename)
        {
            return EnumToListExtension.ConvertCompressedExtensions().Any(x => x.Equals(filename.Substring(filename.Length - 3, 3), StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
