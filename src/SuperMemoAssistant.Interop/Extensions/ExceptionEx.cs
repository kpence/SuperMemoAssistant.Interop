#region License & Metadata

// The MIT License (MIT)
// 
// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
// 
// 
// Modified On:  2020/04/04 18:03
// Modified By:  Alexis

#endregion




namespace SuperMemoAssistant.Extensions
{
  using System;
  using System.IO;

  /// <summary>
  /// Extensions for <see cref="Exception"/>
  /// </summary>
  [System.Diagnostics.CodeAnalysis.SuppressMessage("Naming", "CA1720:Identifier contains type name", Justification = "<Pending>")]
  public static class ExceptionEx
  {
    #region Methods

    /// <summary>
    /// Throws a <see cref="ArgumentNullException"/> if <paramref name="obj"/> is null
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="errMsg"></param>
    public static void ThrowIfArgumentNull(this object obj, string errMsg)
    {
      if (obj == null)
        throw new ArgumentNullException(errMsg);
    }
    
    /// <summary>
    /// Throws a <see cref="NullReferenceException"/> if <paramref name="obj"/> is null
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="errMsg"></param>
    public static void ThrowIfNull(this object obj, string errMsg)
    {
      if (obj == null)
        throw new NullReferenceException(errMsg);
    }
    
    /// <summary>
    /// Throws a <see cref="InvalidOperationException"/> if <paramref name="str"/> is null or white space (blank, tab, line, ..)
    /// </summary>
    /// <param name="str"></param>
    /// <param name="errMsg"></param>
    public static void ThrowIfNullOrWhitespace(this string str, string errMsg)
    {
      if (string.IsNullOrWhiteSpace(str))
        throw new InvalidOperationException(errMsg);
    }
    
    /// <summary>
    /// Throws a <see cref="InvalidOperationException"/> if <paramref name="obj"/> is not null
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="errMsg"></param>
    public static void ThrowIfNotNull(this object obj, string errMsg)
    {
      if (obj != null)
        throw new InvalidOperationException(errMsg);
    }
    
    /// <summary>
    /// Throws a <see cref="InvalidOperationException"/> if <paramref name="dir"/> doesn't exist
    /// </summary>
    /// <param name="dir"></param>
    /// <param name="errMsg"></param>
    public static void ThrowIfMissing(this DirectoryInfo dir, string errMsg)
    {
      if (dir.Exists == false)
        throw new InvalidOperationException(errMsg);
    }
    
    /// <summary>
    /// Throws a <see cref="InvalidOperationException"/> if <paramref name="file"/> doesn't exist
    /// </summary>
    /// <param name="file"></param>
    /// <param name="errMsg"></param>
    public static void ThrowIfMissing(this FileInfo file, string errMsg)
    {
      if (file.Exists == false)
        throw new InvalidOperationException(errMsg);
    }

    #endregion
  }
}
