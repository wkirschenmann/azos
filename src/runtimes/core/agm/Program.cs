﻿namespace agm
{
  class Program
  {
    static void Main(string[] args)
    {
      new Azos.Platform.Abstraction.NetCore.NetCore20Runtime();
      Azos.Sky.Tools.agm.ProgramBody.Main(args);
    }
  }
}
