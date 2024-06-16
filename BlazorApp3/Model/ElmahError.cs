using System;
using System.Collections.Generic;

namespace BlazorApp3.Model;

public partial class ElmahError
{
    public Guid ErrorId { get; set; }

    public string Application { get; set; } = null!;

    public string Host { get; set; } = null!;

    public string Type { get; set; } = null!;

    public string Source { get; set; } = null!;

    public string Message { get; set; } = null!;

    public string User { get; set; } = null!;

    public int StatusCode { get; set; }

    public DateTime TimeUtc { get; set; }

    public int Sequence { get; set; }

    public string AllXml { get; set; } = null!;
}
