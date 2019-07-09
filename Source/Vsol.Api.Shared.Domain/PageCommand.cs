using System;
using System.Collections.Generic;
using System.Text;

namespace Vsol.Api.Shared.Domain
{
    public class PageCommand : InputCommand
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public string OrderBy { get; set; }
        public int SkipNumber { get; }
    }
}
