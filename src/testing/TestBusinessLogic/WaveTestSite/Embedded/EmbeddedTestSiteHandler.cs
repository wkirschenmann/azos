
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using NFX;
using NFX.Web;
using NFX.Wave;
using NFX.Wave.Handlers;
using NFX.Environment;

namespace WaveTestSite.Embedded
{
  /// <summary>
  /// This handler serves the particular embedded site
  /// </summary>
  public class EmbeddedTestSiteHandler : EmbeddedSiteHandler
  {
    public EmbeddedTestSiteHandler(WorkDispatcher dispatcher, string name, int order, WorkMatch match)
                          : base(dispatcher, name, order, match){}


    public EmbeddedTestSiteHandler(WorkDispatcher dispatcher, IConfigSectionNode confNode)
                          : base(dispatcher, confNode) {}


    public override string RootResourcePath
    {
      get { return "WaveTestSite.Embedded"; }
    }

    protected override IEnumerable<EmbeddedSiteHandler.IAction> GetActions()
    {
      yield return new CountAction();
    }
  }

  /// <summary>
  /// Counts from one int to another
  /// </summary>
  public class CountAction : EmbeddedSiteHandler.IAction
  {
    public string Name{ get { return "Count"; } }

    public void Perform(WorkContext context)
    {
      var from = context.Request.QueryString["from"].AsInt(1);
      var to   = context.Request.QueryString["to"].AsInt(10);

      if (to-from>1000) to = from + 1000;//limit so no infinite loop possible

      context.Response.ContentType = ContentType.TEXT;
      for(var i=from;i<=to;i++)
        context.Response.WriteLine("{0} times and counting".Args(i));
    }
  }


}
