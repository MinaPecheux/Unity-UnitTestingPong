import os
import platform
import sys
import xml.etree.ElementTree as ET

# Check if the app type argument has been provided, e.g python3 export_unity.py windows | ios | android
if len(sys.argv) >= 2:
    APP_TYPE = sys.argv[1].lower()
    print(f'APP_TYPE set to: {APP_TYPE}')
else:
    print('APP_TYPE not set. End process.')
    sys.exit()

# Check that environment variables have been set
vars = {'UNITY_HOME' : None}
for key,value in vars.items():
    if key in os.environ:
        vars[key] = os.getenv(key)
    else:
        print(f'{key} is not set. End process.')
        sys.exit()   

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
    sys.exit()

# Check that UNITY_BIN has been set
if UNITY_BIN is not None:
    print('UNITY_BIN set to: ' + UNITY_BIN)
else:
    print('UNITY_BIN does not exist. End process.')
    sys.exit()

# Set log file locations
UNITY_PROJECT_PATH = os.getcwd()
LOG_PATH = f'{UNITY_PROJECT_PATH}{LOG_DIR}'
UNITY_LOG_FILE_PLATFORM = f'{LOG_PATH}unity_unit_test_{APP_TYPE}.log'
UNIT_TEST_FILE_PATH = f'{UNITY_PROJECT_PATH}/tests.xml'

def runTests(appType, unityBin, projectPath, testFilePath, logPath):
    print(f'UNITY START UNIT TESTS {appType}')
    os.system(f'"{unityBin}" -batchmode -projectPath {projectPath} -nographics -runTests -testResults {testFilePath} -logFile {logPath}')
    print(f'UNITY END UNIT TESTS {appType}')

def checkTests(testFilePath):
    if not os.path.exists(testFilePath):
        print(f'Could not find unit tests results!')
        return 1

    tree = ET.parse(testFilePath)
    root = tree.getroot()
    hasPassed = root.attrib['result'] == 'Passed'
    if hasPassed:
        print(f'Tests succeeded :(')
        return 0
    else:
        print(f'Tests failed :(')
        return 1

runTests(APP_TYPE.upper(), UNITY_BIN, UNITY_PROJECT_PATH, UNIT_TEST_FILE_PATH, UNITY_LOG_FILE_PLATFORM)
testsExitCode = checkTests(UNIT_TEST_FILE_PATH)

print('UNIT TESTS DONE')
os.remove(UNIT_TEST_FILE_PATH)
sys.exit(testsExitCode)
