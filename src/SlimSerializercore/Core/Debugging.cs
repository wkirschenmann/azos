/*<FILE_LICENSE>
 * Azos (A to Z Application Operating System) Framework
 * The A to Z Foundation (a.k.a. Azist) licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
</FILE_LICENSE>*/

using System.IO;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace SlimSerializer.Core
{
  /// <summary>
  /// Facilitates debugging tasks enabled by DEBUG conditional define
  /// </summary>
  public static class Debug
  {
    [Conditional("DEBUG")]
    public static void Assert(bool condition, string text)
    {
      if (condition) return;

      var frame = new StackFrame(1, true);
      var m = frame.GetMethod();
      var from = string.Format("{0}.{1} at [{2}:{3}]",
                        m.DeclaringType.FullName, m.Name,
                        Path.GetFileName(frame.GetFileName()), frame.GetFileLineNumber());

      if (string.IsNullOrWhiteSpace(text))
        text = "Assertion failure";
      throw new SlimException(text + ":  " + from, from);
    }
  }
}
