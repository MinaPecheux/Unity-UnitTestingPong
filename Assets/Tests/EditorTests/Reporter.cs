using System.Linq;
using System.Xml;
using UnityEditor.TestTools.TestRunner.Api;

public static class Reporter
{

    public static void ReportJUnitXML(string path, ITestResultAdaptor result)
    {
        XmlDocument xmlDoc = new XmlDocument();
        XmlDeclaration xmlDecl = xmlDoc.CreateXmlDeclaration("1.0", "UTF-8", null);
        xmlDoc.AppendChild(xmlDecl);

        XmlNode rootNode = xmlDoc.CreateElement("testsuites");
        XmlAttribute rootAttrTests = xmlDoc.CreateAttribute("tests");
        rootAttrTests.Value = result.PassCount.ToString();
        rootNode.Attributes.Append(rootAttrTests);
        XmlAttribute rootAttrSkipped = xmlDoc.CreateAttribute("disabled");
        rootAttrSkipped.Value = result.SkipCount.ToString();
        rootNode.Attributes.Append(rootAttrSkipped);
        XmlAttribute rootAttrFailed = xmlDoc.CreateAttribute("failures");
        rootAttrFailed.Value = result.FailCount.ToString();
        rootNode.Attributes.Append(rootAttrFailed);
        XmlAttribute rootAttrName = xmlDoc.CreateAttribute("name");
        rootAttrName.Value = result.Name;
        rootNode.Attributes.Append(rootAttrName);
        XmlAttribute rootAttrTime = xmlDoc.CreateAttribute("time");
        rootAttrTime.Value = result.Duration.ToString();
        rootNode.Attributes.Append(rootAttrTime);
        xmlDoc.AppendChild(rootNode);

        foreach (ITestResultAdaptor testSuite in result.Children.First().Children)
        {
            XmlNode testSuiteNode = xmlDoc.CreateElement("testsuite");
            XmlAttribute testSuiteAttrName = xmlDoc.CreateAttribute("name");
            testSuiteAttrName.Value = testSuite.Name;
            testSuiteNode.Attributes.Append(testSuiteAttrName);
            XmlAttribute testSuiteAttrTests = xmlDoc.CreateAttribute("tests");
            testSuiteAttrTests.Value = testSuite.Test.TestCaseCount.ToString();
            testSuiteNode.Attributes.Append(testSuiteAttrTests);
            XmlAttribute testSuiteAttrSkipped = xmlDoc.CreateAttribute("disabled");
            testSuiteAttrSkipped.Value = testSuite.SkipCount.ToString();
            testSuiteNode.Attributes.Append(testSuiteAttrSkipped);
            XmlAttribute testSuiteAttrFailed = xmlDoc.CreateAttribute("failures");
            testSuiteAttrFailed.Value = testSuite.FailCount.ToString();
            testSuiteNode.Attributes.Append(testSuiteAttrFailed);
            XmlAttribute testSuiteAttrTime = xmlDoc.CreateAttribute("time");
            testSuiteAttrTime.Value = testSuite.Duration.ToString();
            testSuiteNode.Attributes.Append(testSuiteAttrTime);
            foreach (ITestResultAdaptor testFixture in testSuite.Children)
            {
                foreach (ITestResultAdaptor testCase in testFixture.Children)
                {
                    XmlNode testCaseNode = xmlDoc.CreateElement("testcase");
                    XmlAttribute testAttrName = xmlDoc.CreateAttribute("name");
                    testAttrName.Value = testCase.Name;
                    testCaseNode.Attributes.Append(testAttrName);
                    XmlAttribute testAttrAssertions = xmlDoc.CreateAttribute("assertions");
                    testAttrAssertions.Value = testCase.AssertCount.ToString();
                    testCaseNode.Attributes.Append(testAttrAssertions);
                    XmlAttribute testAttrTime = xmlDoc.CreateAttribute("time");
                    testAttrTime.Value = testCase.Duration.ToString();
                    testCaseNode.Attributes.Append(testAttrTime);
                    XmlAttribute testAttrStatus = xmlDoc.CreateAttribute("status");
                    testAttrStatus.Value = testCase.TestStatus.ToString();
                    testCaseNode.Attributes.Append(testAttrStatus);
                    testSuiteNode.AppendChild(testCaseNode);
                }
            }
            rootNode.AppendChild(testSuiteNode);
        }

        xmlDoc.Save(path);
    }

}
