#ifndef _THREAD_MAN_
#define _THREAD_MAN_

#if __cplusplus
extern "C" {
#endif

#ifndef MYAPI
#define MYAPI __declspec(dllexport)
#endif

	void StartThread1Proc(void);
	void StartThread2Proc(void);
	void EndThread1Proc();
	void EndThread2Proc();

#if __cplusplus
}
#endif // end of __cplusplus

#endif // !_THREAD_MAN_
