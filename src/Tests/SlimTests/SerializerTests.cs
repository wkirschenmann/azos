using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Threading.Tasks;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using TestData.DataModel;
using Assert = NUnit.Framework.Assert;

namespace SlimTests
{
  public class SerializerTests
  {
    private static readonly Slim.SlimSerializer Serializer = new Slim.SlimSerializer() {SerializeForFramework = true};
    private static readonly AllTypesData AllTypesDataInstance = new AllTypesData(10);

    [Test]
#if net452    
    public void MyDataSetFrameworkSerializationTest()
#endif //net452
#if NET50
    public void MyDataSetCoreSerializationTest()
#endif //NET50
    {
      byte[] serialized;
      using (var stream = new MemoryStream())
      {
        Serializer.Serialize(stream, ReferenceData.DataSet1);
        serialized = stream.ToArray();
      }

      object res;
      using (var stream = new MemoryStream(serialized))
      {
        res = Serializer.Deserialize(stream);
      }
      Assert.AreEqual(ReferenceData.DataSet1, res);
    }

    [Test]
    [DeploymentItem("MyDataSetFromFramework.slim")] 
#if net452    
    public void MyDataSetFrameworkDeserializationFromFrameworkTest()
#endif //net452
#if NET50
    public void MyDataSetCoreDeserializationFromFrameworkTest()
#endif //NET50
    {
      object res;
      using (var stream = File.OpenRead(SerializedDataGenerator.Filenames.MyDataSetFromFramework))
      {
        res = Serializer.Deserialize(stream);
      }

      Assert.AreEqual(ReferenceData.DataSet1, res);
    }

    [Test]
    [DeploymentItem("MyDataSetFromCore.slim")] 
#if net452    
    public void MyDataSetFrameworkDeserializationFromCoreTest()
#endif //net452
#if NET50
    public void MyDataSetCoreDeserializationFromCoreTest()
#endif //NET50
    {
      object res;
      using (var stream = File.OpenRead(SerializedDataGenerator.Filenames.MyDataSetFromCore))
      {
        res = Serializer.Deserialize(stream);
      }

      Assert.AreEqual(ReferenceData.DataSet1, res);
    }

    [Test]
#if net452    
    public void AllTypesDataFrameworkSerializationTest()
#endif //net452
#if NET50
    public void AllTypesDataCoreSerializationTest()
#endif //NET50
    {
      byte[] serialized;
      using (var stream = new MemoryStream())
      {
        Serializer.Serialize(stream, AllTypesDataInstance);
        serialized = stream.ToArray();
      }

      object res;
      using (var stream = new MemoryStream(serialized))
      {
        res = Serializer.Deserialize(stream);
      }
      AllTypesDataInstance.AssertEqual(res);
    }

    [Test]
    [DeploymentItem("AllTypesDataFromFramework.slim")]
#if net452    
    public void AllTypesDataFrameworkDeserializationFromFrameworkTest()
#endif //net452
#if NET50
    public void AllTypesDataCoreDeserializationFromFrameworkTest()
#endif //NET50
    {
      object res;
      using (var stream = File.OpenRead(SerializedDataGenerator.Filenames.AllTypesDataFromFramework))
      {
        res = Serializer.Deserialize(stream);
      }
      
      AllTypesDataInstance.AssertEqual(res);
    }

    [Test]
    [DeploymentItem("AllTypesDataFromCore.slim")] 
#if net452    
    public void AllTypesDataFrameworkDeserializationFromCoreTest()
#endif //net452
#if NET50
    public void AllTypesDataCoreDeserializationFromCoreTest()
#endif //NET50
    {
      object res;
      using (var stream = File.OpenRead(SerializedDataGenerator.Filenames.AllTypesDataFromCore))
      {
        res = Serializer.Deserialize(stream);
      }
      
      AllTypesDataInstance.AssertEqual(res);
    }


  }
}