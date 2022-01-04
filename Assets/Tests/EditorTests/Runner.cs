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
