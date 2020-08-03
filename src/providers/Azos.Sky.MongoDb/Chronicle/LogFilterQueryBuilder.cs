﻿/*<FILE_LICENSE>
 * Azos (A to Z Application Operating System) Framework
 * The A to Z Foundation (a.k.a. Azist) licenses this file to you under the MIT license.
 * See the LICENSE file in the project root for more information.
</FILE_LICENSE>*/

using System;

using Azos.Data.AST;
using Azos.Data.Access.MongoDb.Connector;
using Azos.Serialization.BSON;
using System.Collections.Generic;

namespace Azos.Sky.Chronicle.Server
{
  internal static class LogFilterQueryBuilder
  {
    private static MongoDbXlat m_LogXlat = new MongoDbXlat();

    public static Query BuildLogFilterQuery(LogChronicleFilter filter)
    {
      var query = new Query();

      var andNodes = new List<BSONElement>();

      if (filter.Id.HasValue)
      {
        andNodes.Add(DataDocConverter.GUID_CLRtoBSON(BsonConvert.FLD_GUID, filter.Id.Value));
      }

      if (filter.RelId.HasValue)
      {
        andNodes.Add(DataDocConverter.GUID_CLRtoBSON(BsonConvert.FLD_RELATED_TO, filter.RelId.Value));
      }

      if (filter.Channel.HasValue && !filter.Channel.Value.IsZero)
      {
        andNodes.Add(new BSONInt64Element(BsonConvert.FLD_CHANNEL, (long)filter.Channel.Value.ID));
      }

      if (filter.Application.HasValue && !filter.Application.Value.IsZero)
      {
        andNodes.Add(new BSONInt64Element(BsonConvert.FLD_APP, (long)filter.Application.Value.ID));
      }

      if (filter.TimeRange.HasValue && filter.TimeRange.Value.Start.HasValue)
      {
        andNodes.Add(new BSONDocumentElement(BsonConvert.FLD_TIMESTAMP, new BSONDocument().Set(new BSONDateTimeElement("$gte", filter.TimeRange.Value.Start.Value)) ));
      }

      if (filter.TimeRange.HasValue && filter.TimeRange.Value.End.HasValue)
      {
        andNodes.Add(new BSONDocumentElement(BsonConvert.FLD_TIMESTAMP, new BSONDocument().Set(new BSONDateTimeElement("$lte", filter.TimeRange.Value.End.Value))));
      }


      if (filter.MinType.HasValue)
      {
        andNodes.Add(new BSONDocumentElement(BsonConvert.FLD_TYPE, new BSONDocument().Set(new BSONInt32Element("$gte", (int)filter.MinType.Value))));
      }

      if (filter.MaxType.HasValue)
      {
        andNodes.Add(new BSONDocumentElement(BsonConvert.FLD_TYPE, new BSONDocument().Set(new BSONInt32Element("$lte", (int)filter.MaxType.Value))));
      }


      query.Set(new BSONArrayElement("$and", andNodes.ToArray()));

      //todo: Finish the Advanced filter
      //var ctx = m_LogXlat.TranslateInContext(filter.AdvancedFilter);
      // var query = query;// ctx.Query;

      return query;
    }
  }
}
