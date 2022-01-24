using UnityEditor;
using UnityEditor.TestTools.TestRunner.Api;
using UnityEngine;

public static class Runner
{
    private static TestRunnerApi _runner = null;

    private class MyCallbacks : ICallbacks
    {

        public void RunStarted(ITestAdaptor testsToRun)
        {}

        public void RunFinished(ITestResultAdaptor result)
        {
            // export test results to JUnit XML format if
            // using -testsOutput argument
            string reportPath = null;
            string[] args = System.Environment.GetCommandLineArgs();
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "-testsOutput")
                {
                    reportPath = args[i + 1];
                    break;
                }
            }

            if (reportPath != null) {
                Reporter.ReportJUnitXML(reportPath, result);
            }

            _runner.UnregisterCallbacks(this);
            if (result.ResultState != "Passed")
            {
                Debug.Log("Tests failed :(");
                if (Application.isBatchMode)
                    EditorApplication.Exit(1);
            }
            else
            {
                Debug.Log("Tests passed :)");
                if (Application.isBatchMode)
                    EditorApplication.Exit(0);
            }
        }

        public void TestStarted(ITestAdaptor test)
        {}

        public void TestFinished(ITestResultAdaptor result)
        {}
    }

    public static void RunUnitTests()
    {
        _runner = ScriptableObject.CreateInstance<TestRunnerApi>();
        Filter filter = new Filter()
        {
            testMode = TestMode.EditMode
        };
        _runner.RegisterCallbacks(new MyCallbacks());
        _runner.Execute(new ExecutionSettings(filter));
    }
}
