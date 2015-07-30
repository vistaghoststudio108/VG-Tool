#include <windows.h>
#include <iostream>
int addmore = 5;

// Define our 2 functions
// Add will return the sum of two numbers
int MyAdd(int a, int b)
{
	return(a + b + addmore);
}

// Function will print out a text string
void MyFunction(void)
{
	std::cout << "cout from the LouDLLMain MyFunction!" << std::endl;
}

BOOL WINAPI DllMain(HINSTANCE hinstDLL, /* Handle to dll module */
					DWORD fdwReason,	/* reason for calling function */
					LPVOID lpReserved)	/* reserved */
{
	int a = 1;
	// Perform actions based on the reason for calling.
	switch (fdwReason)
	{
	case DLL_PROCESS_ATTACH:
		// Initialize once for each new process.
		// Return FALSE to fail DLL load.
		a = 222;
		break;

	case DLL_THREAD_ATTACH:
		// Do thread-specific initialization.
		break;

	case DLL_THREAD_DETACH:
		// Do thread-specific cleanup.
		a = 12;
		break;

	case DLL_PROCESS_DETACH:
		// Perform any necessary cleanup.
		a = 100;
		break;
	}

	return TRUE; // Successful DLL_PROCESS_ATTACH.

}