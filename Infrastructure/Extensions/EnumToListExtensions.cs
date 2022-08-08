using Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Extensions
{
    public static class EnumToListExtension
    {
        public static List<string> ConvertCompressedExtensions()
        {
            return Enum.GetNames(typeof(ECompressedExtensions)).ToList();
        }
    }
}
