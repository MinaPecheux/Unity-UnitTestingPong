import os
import platform
import sys

# Check if the app type argument has been provided, e.g python3 export_unity.py windows | ios | android
if len(sys.argv) >= 2:
    APP_TYPE = sys.argv[1].lower()
    print(f'APP_TYPE set to: {APP_TYPE}')
else:
    print('APP_TYPE not set. End process.')
    sys.exit(1)

# Check that environment variables have been set
vars = {'UNITY_HOME' : None}
for key,value in vars.items():
    if key in os.environ:
        vars[key] = os.getenv(key)
    else:
        print(f'{key} is not set. End process.')
        sys.exit(1)   

# Set location of Unity binary according to machine type
platform = platform.system()
if platform == 'Darwin':
    UNITY_BIN = vars['UNITY_HOME'] + '/Contents/MacOS/Unity' 
    LOG_DIR = '/Logs/'
elif platform == 'Windows':
    UNITY_BIN = vars['UNITY_HOME'] + '\\Unity.exe'
    LOG_DIR = '\\Logs\\'
elif platform == "Linux":
    print("Build for Linux on Mac or Windows. End Process.")
    sys.exit(1)

# Check that UNITY_BIN has been set
if UNITY_BIN is not None:
    print('UNITY_BIN set to: ' + UNITY_BIN)
else:
    print('UNITY_BIN does not exist. End process.')
    sys.exit(1)

# Set log file locations
UNITY_PROJECT_PATH = os.getcwd()
LOG_PATH = f'{UNITY_PROJECT_PATH}{LOG_DIR}'
UNITY_LOG_FILE_PLATFORM = f'{LOG_PATH}unity_unit_test_{APP_TYPE}.log'
UNIT_TEST_FILE_PATH = f'{UNITY_PROJECT_PATH}/tests.xml'

def runTests(appType, unityBin, projectPath, testFilePath, logPath):
    print(f'UNITY START UNIT TESTS {appType}')
    exitCode = os.system(f'"{unityBin}" -batchmode -projectPath "{projectPath}" -nographics -logFile "{logPath}" -executeMethod Runner.RunUnitTests -testsOutput "{testFilePath}"')
    print(f'UNITY END UNIT TESTS {appType} (exit code: {exitCode})')
    return 0 if exitCode == 0 else 1

testsExitCode = runTests(APP_TYPE.upper(), UNITY_BIN, UNITY_PROJECT_PATH, UNIT_TEST_FILE_PATH, UNITY_LOG_FILE_PLATFORM)

print('UNIT TESTS DONE')
sys.exit(testsExitCode)
