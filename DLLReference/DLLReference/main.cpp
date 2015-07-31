
//Note: No properties need to be added to be able to compile and link this program

// LOADING DLLS THE HARD(ER) WAY (Dynamic Loading)

#include <iostream>
// Need Windows.h for HINSTANCE and DLL Loading and Releasing functions.
#include <windows.h>
#include <process.h>
#include <assert.h>

#include "com_struct.h"
#include "id_defs.h"
#include "thread_man.h"
#define THREADCOUNT	5

// Function pointers that will be used for the DLL functions.
typedef int(*AddFunc)(int, int);
typedef void(*FunctionFunc)(void);

THREAD_INFO_t thread_info[] =
{
	{ THREAD1_TID, (StartThread)StartThread1Proc, EndThread1Proc, 0, 0, 0 },
	{ THREAD2_TID, (StartThread)StartThread2Proc, EndThread2Proc, 0, 0, 0 },
	{0,		NULL,			NULL,		0, 0, 0}
};

void ThreadProc(void *param);

DWORD dwTlsIndex;

// Function declarations and definitions...
VOID ErrorExit(LPTSTR lpszMessage)
{
	fprintf(stderr, "%s\n", lpszMessage);
	ExitProcess(0);
}

void MyCommonFunction(void)
{
	LPVOID lpvData;

	// Retrieve a data pointer for the current thread...
	lpvData = TlsGetValue(dwTlsIndex);

	if ((lpvData == 0) && (GetLastError() != 0))
		ErrorExit(L"TlsGetValue() error!\n");
	else
		printf("TlsGetValue() is OK.\n");

	// Use the data stored for the current thread...
	printf("Common: thread %u: lpvData = %p\n\n", GetCurrentThreadId(), lpvData);
}

DWORD WINAPI MyThreadFunc(void)
{
	LPVOID lpvData;

	// Initialize the TLS index for this thread.
	lpvData = (LPVOID)LocalAlloc(LPTR, 256);

	if (!TlsSetValue(dwTlsIndex, lpvData))
		ErrorExit(L"TlsSetValue() error!\n");
	else
		printf("TlsSetValue() is OK.\n");

	printf("Thread %u: lpvData = %p\n", GetCurrentThreadId(), lpvData);
	MyCommonFunction();

	// Release the dynamic memory before the thread returns...
	lpvData = TlsGetValue(dwTlsIndex);
	if (lpvData != 0)
		LocalFree((HLOCAL)lpvData);
	return 0;
}

void TlsExperiment() {
	DWORD index1 = ::TlsAlloc();
	assert(index1 != TLS_OUT_OF_INDEXES);
	LPVOID value1 = ::TlsGetValue(index1);
	assert(value1 == 0);  // Nobody else has used this slot yet.
	value1 = reinterpret_cast<LPVOID>(0x1234ABCD);
	::TlsSetValue(index1, value1);
	assert(value1 == ::TlsGetValue(index1));
	::TlsFree(index1);

	DWORD index2 = ::TlsAlloc();
	// There's nothing that requires TlsAlloc to give us back the recently freed slot,
	// but it just so happens that it does, which is convenient for our experiment.
	assert(index2 == index1); // If this assertion fails, the experiment is invalid.

	LPVOID value2 = ::TlsGetValue(index2);
	assert(value2 == 0);  // If the TlsAlloc documentation is right, value2 == 0.
	// If it's wrong, you'd expect value2 == 0x1234ABCD.
}

// Must have a main function
int main()
{
	DWORD IDThread;
	HANDLE hThread[THREADCOUNT];
	int i;

	printf("Thread count is: %u\n", THREADCOUNT);

	TlsExperiment();

	// Allocate a TLS index...
	if ((dwTlsIndex = TlsAlloc()) == -1)
		ErrorExit(L"TlsAlloc() failed");
	else
		printf("\nTlsAlloc() is OK.\n\n");

	// Create multiple threads...
	for (i = 0; i < THREADCOUNT; i++)
	{
		hThread[i] = CreateThread(NULL, // no security attributes
			0, // use default stack size
			(LPTHREAD_START_ROUTINE)MyThreadFunc, // thread function
			NULL, // no thread function argument
			0, // use default creation flags
			&IDThread); // returns thread identifier

		// Check the return value for success...
		if (hThread[i] == NULL)
			ErrorExit(L"CreateThread() error.\n");
		else
			printf("hThread[%u] is OK.\n", i);
	}

	for (i = 0; i < THREADCOUNT; i++)
		WaitForSingleObject(hThread[i], INFINITE);

	if (TlsFree(dwTlsIndex) == 0)
		printf("TlsFree() failed!\n");
	else
		printf("TlsFree() is OK!\n");

	std::cin.get();
	return 0;

	//int val = 0;

	/*for (int i = 0; i < 2; i++)
	{
		HANDLE handle = (HANDLE)_beginthread(thread_info[i].StartProc, 0, &val);
		WaitForSingleObject(handle, INFINITE);
	}*/

	//HANDLE threadHandle = (HANDLE)_beginthread(ThreadProc, 0, &val);

	// Wait for the user to press enter to exit
	//std::cin.get();
	//return 0;
}

void ThreadProc(void* param)
{
	// Typedef functions to hold what is in the DLL
	AddFunc _AddFunc;
	FunctionFunc _FunctionFunc;

	// The Instance of the DLL.
	// LoadLibrary used to load a DLL

	HINSTANCE hInstLibrary = LoadLibrary(TEXT("OtherDLL.dll"));
	if (hInstLibrary)
	{
		// Our DLL is loaded and ready to go.
		// Set up our function pointers.
		_AddFunc = (AddFunc)GetProcAddress(hInstLibrary, "Func1");
		_FunctionFunc = (FunctionFunc)GetProcAddress(hInstLibrary, "Func2");
		// Check if _AddFunc is currently holding a function, if not don't run it.
		if (_AddFunc)
		{
			std::cout << "23 + 43 = " << _AddFunc(23, 43) << std::endl;
		}

		// Check if _FunctionFunc is currently holding a function, if not don't run it.
		if (_FunctionFunc)
		{
			_FunctionFunc();
		}
		// We're done with the DLL so we need to release it from memory.
		FreeLibrary(hInstLibrary);
	}
	else
	{
		// Our DLL failed to load!
		std::cout << "DLL Failed To Load!" << std::endl;
	}

}
