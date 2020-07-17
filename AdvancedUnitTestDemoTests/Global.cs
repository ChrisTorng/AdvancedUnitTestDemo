using System.Diagnostics.CodeAnalysis;
using Microsoft.VisualStudio.TestTools.UnitTesting;

[assembly: Parallelize(Workers = 0, Scope = ExecutionScope.MethodLevel)]
[assembly: SuppressMessage("Naming", "CA1707:Identifiers should not contain underscores",
    Justification = "標準測試函式命名需要用到底線。", Scope = "module")]
