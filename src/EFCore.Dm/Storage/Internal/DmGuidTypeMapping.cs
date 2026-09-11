using System;
using System.Data;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Microsoft.EntityFrameworkCore.Dm.Storage.Internal
{
    /// <summary>
    ///     Guid mapping that binds Guid values as strings.
    ///     DmProvider's DmSetValue.SetObject silently skips binding a raw Guid value
    ///     when the server parameter's typeFlag == 1 and the column precision is not
    ///     36/8188 (string) or 16 (binary), which surfaces as
    ///     DmException 6054 "Unbinded parameter". Converting Guid to string before
    ///     binding routes the value through the plain SetString path, which always binds.
    /// </summary>
    public class DmGuidTypeMapping : GuidTypeMapping
    {
        public DmGuidTypeMapping()
            : base(new RelationalTypeMappingParameters(
                new CoreTypeMappingParameters(
                    typeof(Guid),
                    new GuidToStringConverter()),
                "CHAR(36)",
                StoreTypePostfix.None,
                System.Data.DbType.String))
        {
        }
    }
}
