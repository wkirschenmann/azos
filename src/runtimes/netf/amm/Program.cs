﻿namespace amm
{
  class Program
  {
    static void Main(string[] args)
    {
      new Azos.Platform.Abstraction.NetFramework.DotNetFrameworkRuntime();
      Azos.Sky.Tools.amm.ProgramBody.Main(args);
    }
  }
}
